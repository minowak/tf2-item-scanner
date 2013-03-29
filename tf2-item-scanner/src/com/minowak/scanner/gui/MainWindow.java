package com.minowak.scanner.gui;

import java.awt.BorderLayout;
import java.awt.Cursor;
import java.awt.Desktop;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.io.File;
import java.io.IOException;
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
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTextField;
import javax.swing.ListSelectionModel;
import javax.swing.SwingConstants;
import javax.swing.SwingUtilities;
import javax.swing.border.BevelBorder;

import com.minowak.scanner.schema.SchemaParser;
import com.minowak.scanner.schema.TF2Item;
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
	private JList<String> resultsArea;

	private JLabel idLabel;
	private JLabel itemListLabel;
	private JLabel filterLabel;
	private JLabel timeLabel;
	private JLabel resultsLabel;
	private JLabel statusLabel;

	private JList<TF2Item> selected;

	private JButton filterBtn;
	private JButton selectBtn;
	private JButton removeBtn;
	private JButton searchBtn;
	private JButton stopBtn;
	private JButton cleanBtn;

	private JPanel statusPanel;

	private JList<TF2Item> itemList;

	DefaultListModel<TF2Item> listModel = new DefaultListModel<>();
	DefaultListModel<String> resultModel = new DefaultListModel<>();
	DefaultListModel<TF2Item> selectedListModel = new DefaultListModel<>();

	public MainWindow() {
		setTitle(TITLE);
		setSize(800, 500);
		setLocationRelativeTo(null);
		setDefaultCloseOperation(EXIT_ON_CLOSE);

		initGUI();
	}

	private void initGUI() {
		JPanel panel = new JPanel();
		getContentPane().add(panel);

		statusLabel = new JLabel("Ready");
		statusLabel.setHorizontalAlignment(SwingConstants.LEFT);

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
		timeTextField = new JTextField(10);
		timeTextField.setText("0");

		timePanel.add(timeLabel);
		timePanel.add(timeTextField);

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
				}

				selected.validate();
			}
		});

		searchBtn = new JButton("SEARCH!");
		searchBtn.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent arg0) {
				lUpdater = new ListUpdater(resultModel, statusLabel, idTextField.getText().trim(),
						Long.parseLong(timeTextField.getText().trim()),
						selectedItems);
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

		controlPanel.add(searchBtn);
		controlPanel.add(stopBtn);

		leftPanel.add(controlPanel);
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

		statusPanel.add(statusLabel);

		panel.add(leftPanel, BorderLayout.WEST);
		panel.add(rightPanel, BorderLayout.CENTER);
		panel.add(statusPanel, BorderLayout.SOUTH);
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
