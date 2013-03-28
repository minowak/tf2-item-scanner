package com.minowak.scanner.gui;

import java.awt.BorderLayout;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;
import java.io.File;
import java.io.IOException;
import java.util.LinkedList;

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
import javax.swing.SwingUtilities;

import com.minowak.scanner.schema.SchemaParser;
import com.minowak.scanner.schema.TF2Item;
import com.minowak.scanner.utils.QualityCellRenderer;

public class MainWindow extends JFrame {
	private static final long serialVersionUID = -3981477621230432928L;
	private final static String TITLE = "TF2 Item Scanner";

	private TF2Item [] items;

	/** Widgets */
	private JTextField idTextField;
	private JTextField filterTextField;

	private JLabel idLabel;
	private JLabel itemListLabel;
	private JLabel filterLabel;

	private JButton searchBtn;

	private JList<TF2Item> itemList;

	DefaultListModel<TF2Item> listModel = new DefaultListModel<>();

	public MainWindow() {
		setTitle(TITLE);
		setSize(400, 400);
		setLocationRelativeTo(null);
		setDefaultCloseOperation(EXIT_ON_CLOSE);

		initGUI();
	}

	private void initGUI() {
		JPanel panel = new JPanel();
		getContentPane().add(panel);

		panel.setLayout(new BorderLayout());

		idTextField = new JTextField(20);
		idLabel = new JLabel("Starting STEAM_ID64");
		itemListLabel = new JLabel("Items");

		items = getItemsFromSchema();

		for(TF2Item item : items) {
			listModel.addElement(item);
		}

		itemList = new JList<TF2Item>(listModel);
		itemList.setSelectionMode(ListSelectionModel.SINGLE_INTERVAL_SELECTION);
		itemList.setCellRenderer(new QualityCellRenderer());
		itemList.setVisibleRowCount(-1);

		JScrollPane listScroller = new JScrollPane(itemList);
		listScroller.setPreferredSize(new Dimension(10, 250));

		JPanel idPanel = new JPanel();
		idPanel.setLayout(new FlowLayout(2));

		idPanel.add(idLabel);
		idPanel.add(idTextField);

		JPanel filterPanel = new JPanel();
		filterPanel.setLayout(new FlowLayout());

		filterLabel = new JLabel("Search: ");
		filterTextField = new JTextField(20);

		searchBtn = new JButton("Search");
		searchBtn.addActionListener(new ActionListener() {
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

		filterPanel.add(filterLabel);
		filterPanel.add(filterTextField);
		filterPanel.add(searchBtn);

		JPanel leftPanel = new JPanel();
		leftPanel.setLayout(new BoxLayout(leftPanel, BoxLayout.Y_AXIS));
		leftPanel.add(idPanel);
		leftPanel.add(itemListLabel);
		leftPanel.add(filterPanel);
		leftPanel.add(listScroller);

		panel.add(leftPanel, BorderLayout.WEST);
	}

	private TF2Item[] getItemsFromSchema() {
		try {
			return new SchemaParser(new File("C:\\Users\\News\\Documents\\GitHub\\tf2-item-scanner\\tf2-item-scanner\\schema\\item_schema.txt"))
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
