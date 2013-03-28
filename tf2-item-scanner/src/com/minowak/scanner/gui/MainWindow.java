package com.minowak.scanner.gui;

import java.awt.BorderLayout;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.File;
import java.io.IOException;
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
import javax.swing.JTextArea;
import javax.swing.JTextField;
import javax.swing.ListSelectionModel;
import javax.swing.SwingUtilities;

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
	private JTextArea resultsArea;

	private JLabel idLabel;
	private JLabel itemListLabel;
	private JLabel filterLabel;
	private JLabel timeLabel;
	private JLabel resultsLabel;
	private JTextArea selected;

	private JButton filterBtn;
	private JButton selectBtn;
	private JButton searchBtn;
	private JButton stopBtn;

	private JList<TF2Item> itemList;

	DefaultListModel<TF2Item> listModel = new DefaultListModel<>();

	public MainWindow() {
		setTitle(TITLE);
		setSize(800, 400);
		setLocationRelativeTo(null);
		setDefaultCloseOperation(EXIT_ON_CLOSE);

		initGUI();
	}

	private void initGUI() {
		JPanel panel = new JPanel();
		getContentPane().add(panel);

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

		JPanel listPanel = new JPanel();
		listPanel.setLayout(new FlowLayout());

		JScrollPane listScroller = new JScrollPane(itemList);
		listScroller.setPreferredSize(new Dimension(200, 240));

		selected = new JTextArea(15,18);
		selected.setEditable(false);
		listPanel.add(listScroller);
		listPanel.add(selected);

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

		selectBtn = new JButton("Select");
		selectBtn.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				List<TF2Item> its = itemList.getSelectedValuesList();
				for(TF2Item it : its) {
					if(!selectedItems.contains(it)) {
						selectedItems.add(it);
						selected.append(it + "\n");
					}
				}
			}
		});

		searchBtn = new JButton("SEARCH!");
		searchBtn.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent arg0) {
				lUpdater = new ListUpdater(resultsArea, idTextField.getText().trim(),
						Long.parseLong(timeTextField.getText().trim()),
						selectedItems);
				lUpdater.start();
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
		leftPanel.add(selectBtn);
		leftPanel.add(searchBtn);
		leftPanel.add(stopBtn);
		leftPanel.add(timePanel);

		JPanel rightPanel = new JPanel();
		rightPanel.setLayout(new BoxLayout(rightPanel, BoxLayout.Y_AXIS));

		resultsLabel = new JLabel("Found STEAM_IDs");

		resultsArea = new JTextArea(2, 5);
		resultsArea.setEditable(false);
		JScrollPane resultScroll = new JScrollPane (resultsArea,
				   JScrollPane.VERTICAL_SCROLLBAR_ALWAYS, JScrollPane.HORIZONTAL_SCROLLBAR_ALWAYS);

		rightPanel.add(resultsLabel);
		rightPanel.add(resultScroll);

		panel.add(leftPanel, BorderLayout.WEST);
		panel.add(rightPanel, BorderLayout.CENTER);
	}

	private TF2Item[] getItemsFromSchema() {
		try {
			return new SchemaParser(new File("schema\\item_schema.txt").getCanonicalFile())
				.parse();
		} catch (IOException e) {
			e.printStackTrace();
		}
		return new TF2Item[1];
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
