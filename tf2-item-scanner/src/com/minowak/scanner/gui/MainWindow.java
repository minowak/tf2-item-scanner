package com.minowak.scanner.gui;

import java.awt.BorderLayout;
import java.awt.Desktop;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.io.PrintWriter;
import java.net.URI;
import java.net.URISyntaxException;
import java.util.LinkedList;
import java.util.List;

import javax.swing.BoxLayout;
import javax.swing.DefaultListModel;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JList;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JProgressBar;
import javax.swing.JScrollPane;
import javax.swing.JTextField;
import javax.swing.ListSelectionModel;
import javax.swing.SwingConstants;
import javax.swing.SwingUtilities;
import javax.swing.border.BevelBorder;

import com.minowak.scanner.schema.SchemaParser;
import com.minowak.scanner.schema.TF2Item;
import com.minowak.scanner.utils.Configuration;
import com.minowak.scanner.utils.QualityCellRenderer;

public class MainWindow extends JFrame {
	private static final long serialVersionUID = -3981477621230432928L;
	private final static String TITLE = "TF2 Item Scanner";

	private TF2Item [] items;
	private List<TF2Item> selectedItems = new LinkedList<TF2Item>();

	private ListUpdater lUpdater;

	/** Widgets */
	private JTextField idTextField;
	private JTextField filterTextField;
	private JTextField timeTextField;
	private JTextField wasOnlineText;
	private JTextField profilesCount;
	private JList<String> resultsArea;

	private JLabel idLabel;
	private JLabel itemListLabel;
	private JLabel filterLabel;
	private JLabel timeLabel;
	private JLabel resultsLabel;
	private JLabel profilesToScan;
	private JLabel[] wasOnlineLabels;

	private JList<TF2Item> selected;

	private JButton filterBtn;
	private JButton selectBtn;
	private JButton removeBtn;
	private JButton searchBtn;
	private JButton stopBtn;
	private JButton cleanBtn;

	private JPanel statusPanel;
	private JPanel panel;

	private JList<TF2Item> itemList;

	private JProgressBar progressBar;

	DefaultListModel<TF2Item> listModel = new DefaultListModel<>();
	DefaultListModel<String> resultModel = new DefaultListModel<>();
	DefaultListModel<TF2Item> selectedListModel = new DefaultListModel<>();

	/** Menu */
	private JMenuBar menuBar;
	private JMenu fileMenu;
	private JMenuItem webApiItem;

	public MainWindow() {
		setTitle(TITLE);
		setSize(800, 500);
		setLocationRelativeTo(null);
		setDefaultCloseOperation(EXIT_ON_CLOSE);

		initGUI();
	}

	private void initGUI() {
		try {
			BufferedReader br = new BufferedReader(new FileReader(new File("steamapi")));
			Configuration.API_KEY = br.readLine();
		} catch(Exception e) {
			e.printStackTrace();
		}

		panel = new JPanel();
		getContentPane().add(panel);

		progressBar = new JProgressBar(0, 100);
		progressBar.setValue(0);
		progressBar.setStringPainted(true);

		panel.setLayout(new BorderLayout());

		idTextField = new JTextField(20);
		idTextField.setText("76561197992203636");
		idLabel = new JLabel("Starting STEAM_ID64");
		itemListLabel = new JLabel("Items");

		items = getItemsFromSchema();

		for(TF2Item item : items) {
			listModel.addElement(item);
		}

		itemList = new JList<TF2Item>(listModel);
		itemList.setSelectionMode(ListSelectionModel.MULTIPLE_INTERVAL_SELECTION);
		itemList.setCellRenderer(new QualityCellRenderer());
		itemList.setVisibleRowCount(-1);
		itemList.addMouseListener(new MouseAdapter() {
		    public void mouseClicked(MouseEvent evt) {
		        JList list = (JList)evt.getSource();
		        if (evt.getClickCount() == 2) {
		            int index = list.locationToIndex(evt.getPoint());

		            itemList.setSelectedIndex(index);
		            if(!selectedListModel.contains(itemList.getSelectedValue())) {
		            	selectedListModel.addElement(itemList.getSelectedValue());
		            	selectedItems.add(itemList.getSelectedValue());
		            	selected.validate();
		            }
		        }
		    }
		});

		JPanel listPanel = new JPanel();
		listPanel.setLayout(new FlowLayout());

		JScrollPane listScroller = new JScrollPane(itemList);
		listScroller.setPreferredSize(new Dimension(200, 240));

		selected = new JList<TF2Item>(selectedListModel);
		selected.setSelectionMode(ListSelectionModel.MULTIPLE_INTERVAL_SELECTION);
		selected.setCellRenderer(new QualityCellRenderer());
		selected.setVisibleRowCount(-1);
		selected.addMouseListener(new MouseAdapter() {
		    public void mouseClicked(MouseEvent evt) {
		        JList list = (JList)evt.getSource();
		        if (evt.getClickCount() == 2) {
		            int index = list.locationToIndex(evt.getPoint());

		            selected.setSelectedIndex(index);
		            selectedListModel.removeElement(selected.getSelectedValue());
		            selectedItems.remove(itemList.getSelectedValue());
		            selected.validate();
		        }
		    }
		});

		JScrollPane selectedScroller = new JScrollPane(selected);
		selectedScroller.setPreferredSize(new Dimension(200, 240));

		listPanel.add(listScroller);
		listPanel.add(selectedScroller);

		JPanel idPanel = new JPanel();
		idPanel.setLayout(new FlowLayout(2));

		idPanel.add(idLabel);
		idPanel.add(idTextField);

		JPanel filterPanel = new JPanel();
		filterPanel.setLayout(new FlowLayout());

		filterLabel = new JLabel("Filter: ");
		filterTextField = new JTextField(20);

		filterBtn = new JButton("Filter");
		filterBtn.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent arg0) {
				String phrase = filterTextField.getText().trim();
				if(phrase.isEmpty()) {
					itemList.setListData(items);
				} else {
					LinkedList<TF2Item> filtered = new LinkedList<TF2Item>();
					for(TF2Item item : items) {
						if(item.getName().toUpperCase().contains(phrase.toUpperCase())) {
							filtered.add(item);
						}
					}
					listModel.removeAllElements();
					for(TF2Item item : filtered) {
						listModel.addElement(item);
					}
					filtered.toArray(new TF2Item[filtered.size()]);
				}

				itemList.validate();
			}
		});

		JPanel timePanel = new JPanel();
		timePanel.setLayout(new FlowLayout());

		timeLabel = new JLabel("Played no more than");
		timeTextField = new JTextField(3);
		timeTextField.setText("0");

		wasOnlineLabels = new JLabel[2];
		wasOnlineLabels[0] = new JLabel("Was online no more than");
		wasOnlineLabels[1] = new JLabel("days ago");
		wasOnlineText = new JTextField(3);
		wasOnlineText.setText("7");

		timePanel.add(timeLabel);
		timePanel.add(timeTextField);
		timePanel.add(wasOnlineLabels[0]);
		timePanel.add(wasOnlineText);
		timePanel.add(wasOnlineLabels[1]);

		filterPanel.add(filterLabel);
		filterPanel.add(filterTextField);
		filterPanel.add(filterBtn);

		selectBtn = new JButton(">>");
		selectBtn.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				List<TF2Item> its = itemList.getSelectedValuesList();
				for(TF2Item it : its) {
					if(!selectedListModel.contains(it)) {
						selectedListModel.addElement(it);
						selectedItems.add(it);
					}
				}

				selected.validate();
			}
		});

		removeBtn = new JButton("<<");
		removeBtn.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent arg0) {
				List<TF2Item> its = selected.getSelectedValuesList();
				for(TF2Item it : its) {
					selectedListModel.removeElement(it);
					selectedItems.remove(it);
				}

				selected.validate();
			}
		});

		searchBtn = new JButton("SEARCH!");
		searchBtn.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent arg0) {
				lUpdater = new ListUpdater(resultModel, progressBar, idTextField.getText().trim(),
						Long.parseLong(timeTextField.getText().trim()),
						selectedItems, Integer.parseInt(profilesCount.getText()), Long.parseLong(wasOnlineText.getText()));
				lUpdater.start();
				resultsArea.validate();
			}
		});

		stopBtn = new JButton("STOP");
		stopBtn.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent arg0) {
				if(lUpdater != null) {
					lUpdater.stopMe();
					lUpdater = null;

					try {
						Runtime.getRuntime().exec("Taskkill /F /IM python.exe");
						Runtime.getRuntime().exec("Taskkill /F /IM pythonw.exe");
					} catch (IOException e) {
						e.printStackTrace();
					}
				}
			}
		});

		JPanel leftPanel = new JPanel();
		leftPanel.setLayout(new BoxLayout(leftPanel, BoxLayout.Y_AXIS));
		leftPanel.add(idPanel);
		leftPanel.add(itemListLabel);
		leftPanel.add(filterPanel);
		leftPanel.add(listPanel);

		JPanel arrowsPanel = new JPanel();
		arrowsPanel.setLayout(new FlowLayout());
		arrowsPanel.add(removeBtn);
		arrowsPanel.add(selectBtn);

		leftPanel.add(arrowsPanel);

		JPanel controlPanel = new JPanel();
		controlPanel.setLayout(new FlowLayout());

		profilesToScan = new JLabel("Profiles to scan");
		profilesCount = new JTextField(3);
		profilesCount.setText("100");

		controlPanel.add(searchBtn);
		controlPanel.add(stopBtn);

		JPanel profilePanel = new JPanel();
		profilePanel.setLayout(new FlowLayout());

		profilePanel.add(profilesToScan);
		profilePanel.add(profilesCount);

		leftPanel.add(controlPanel);
		leftPanel.add(profilePanel);
		leftPanel.add(timePanel);

		JPanel rightPanel = new JPanel();
		rightPanel.setLayout(new BoxLayout(rightPanel, BoxLayout.Y_AXIS));

		resultsLabel = new JLabel("Found STEAM_IDs");
		resultsLabel.setHorizontalAlignment(SwingConstants.LEFT);

		resultsArea = new JList<String>(resultModel);
		JScrollPane resultScroll = new JScrollPane (resultsArea,
				   JScrollPane.VERTICAL_SCROLLBAR_ALWAYS, JScrollPane.HORIZONTAL_SCROLLBAR_ALWAYS);
		resultScroll.setPreferredSize(new Dimension(200, 380));
		resultsArea.addMouseListener(new MouseAdapter() {
		    public void mouseClicked(MouseEvent evt) {
		        JList<String> list = (JList<String>)evt.getSource();
		        if (evt.getClickCount() == 2) {
		            int index = list.locationToIndex(evt.getPoint());

		            resultsArea.setSelectedIndex(index);
		            goWebsite("http://steamcommunity.com/profiles/" + resultsArea.getSelectedValue());
		        }
		    }
		});

		cleanBtn = new JButton("Clean");
		cleanBtn.setHorizontalAlignment(SwingConstants.RIGHT);

		JPanel cleanPanel = new JPanel();
		cleanPanel.add(resultsLabel);
		cleanPanel.add(cleanBtn);
		cleanBtn.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				resultModel.removeAllElements();
				resultsArea.validate();
			}
		});

		rightPanel.add(cleanPanel);
		rightPanel.add(resultScroll);

		statusPanel = new JPanel();
		statusPanel.setBorder(new BevelBorder(BevelBorder.LOWERED));
		statusPanel.setLayout(new BoxLayout(statusPanel, BoxLayout.X_AXIS));

		statusPanel.add(progressBar);

		panel.add(leftPanel, BorderLayout.WEST);
		panel.add(rightPanel, BorderLayout.CENTER);
		panel.add(statusPanel, BorderLayout.SOUTH);

		initMenu();
	}

	private void initMenu() {
		menuBar = new JMenuBar();
		fileMenu = new JMenu("File");
		menuBar.add(fileMenu);
		webApiItem = new JMenuItem("Set API key");
		webApiItem.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent arg0) {
				JPanel panel = new JPanel();
				JLabel label = new JLabel("Enter key:");
				JTextField pass = new JTextField(15);
				panel.add(label);
				panel.add(pass);
				String[] options = new String[]{"OK", "Cancel"};
				int option = JOptionPane.showOptionDialog(null, panel, "Steam WEB API",
				                         JOptionPane.NO_OPTION, JOptionPane.PLAIN_MESSAGE,
				                         null, options, options[1]);
				if(option == 0) // pressing OK button
				{
				    String password = pass.getText();
				    System.out.println("inputed " + password);
				    PrintWriter pw;
					try {
						pw = new PrintWriter(new File("steamapi"));
						System.out.println("writingapi to file");
						pw.write(password);
						pw.close();
						Configuration.API_KEY = password;
					} catch (FileNotFoundException e) {
						e.printStackTrace();
					}
				}
			}
		});

		fileMenu.add(webApiItem);

		setJMenuBar(menuBar);
	}

	private TF2Item[] getItemsFromSchema() {
		try {
			return new SchemaParser(new File("schema" + File.separator + "item_schema.txt").getCanonicalFile())
				.parse();
		} catch (IOException e) {
			e.printStackTrace();
		}
		return new TF2Item[1];
	}

	private void goWebsite(final String url) {
        try {
                Desktop.getDesktop().browse(new URI(url));
        } catch (URISyntaxException | IOException ex) {
                //It looks like there's a problem
        }
    }

	public static void main(String[] args) {
		SwingUtilities.invokeLater(new Runnable() {
			public void run() {
				MainWindow mw = new MainWindow();
				mw.setVisible(true);
			}
		});
	}
}
