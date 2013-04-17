package com.minowak.scanner.gui;

import java.awt.BorderLayout;
import java.awt.Desktop;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.Image;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.InputEvent;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.URI;
import java.net.URISyntaxException;
import java.net.URL;
import java.net.URLConnection;
import java.util.LinkedList;
import java.util.List;
import java.util.logging.FileHandler;
import java.util.logging.Level;
import java.util.logging.Logger;
import java.util.logging.SimpleFormatter;
import java.util.prefs.BackingStoreException;
import java.util.prefs.InvalidPreferencesFormatException;
import java.util.prefs.Preferences;

import javax.imageio.ImageIO;
import javax.swing.BoxLayout;
import javax.swing.DefaultListModel;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JFileChooser;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JList;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JPopupMenu;
import javax.swing.JProgressBar;
import javax.swing.JScrollPane;
import javax.swing.JTextField;
import javax.swing.ListSelectionModel;
import javax.swing.SwingConstants;
import javax.swing.SwingUtilities;
import javax.swing.ToolTipManager;
import javax.swing.UIManager;
import javax.swing.border.BevelBorder;
import javax.swing.event.DocumentEvent;
import javax.swing.event.DocumentListener;

import com.minowak.scanner.engine.SteamProfile;
import com.minowak.scanner.schema.ItemQuality;
import com.minowak.scanner.schema.SchemaParser;
import com.minowak.scanner.schema.TF2Item;
import com.minowak.scanner.utils.Configuration;
import com.minowak.scanner.utils.QualityCellRenderer;
import com.minowak.scanner.utils.SteamIdConverter;
import com.minowak.scanner.utils.VisitedCellRenderer;

/**
 * Main class for GUI. Singleton.
 *
 * @author minowak
 *
 */
public class MainWindow extends JFrame {
	/**
	 * Logger.
	 */
	public final static Logger LOGGER = Logger.getLogger(MainWindow.class .getName());
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
	private JTextField valueTextField;
	private JList<SteamProfile> resultsArea;

	private JLabel idLabel;
	private JLabel itemListLabel;
	private JLabel filterLabel;
	private JLabel timeLabel;
	private JLabel profilesToScan;
	private JLabel valueLabel;
	private JLabel status;
	private JLabel[] wasOnlineLabels;

	private JList<TF2Item> selected;

	private JButton selectBtn;
	private JButton removeBtn;
	private JButton searchBtn;
	private JButton stopBtn;
	private JButton cleanBtn;
	private JButton saveBtn;
	private JButton loadBtn;

	private JPanel statusPanel;
	private JPanel panel;

	private JList<TF2Item> itemList;

	private JProgressBar progressBar;

	private DefaultListModel<TF2Item> listModel = new DefaultListModel<>();
	private DefaultListModel<SteamProfile> resultModel = new DefaultListModel<>();
	private DefaultListModel<TF2Item> selectedListModel = new DefaultListModel<>();

	/** Menu */
	private JMenuBar menuBar;
	private JMenu fileMenu;
	private JMenu helpMenu;
	private JMenuItem webApiItem;
	private JMenuItem aboutItem;

	private static MainWindow instance = null;

	/**
	 * Returns instance of this class.
	 * @return instance
	 */
	public static MainWindow getInstance() {
		if(instance == null) {
			instance = new MainWindow();
		}
		return instance;
	}

	private MainWindow() {
		FileHandler fileTxt = null;
		try {
			fileTxt = new FileHandler("scanner.log");
		} catch (Exception e) {
			LOGGER.severe(e.getMessage());
		}
		fileTxt.setFormatter(new SimpleFormatter());
		LOGGER.addHandler(fileTxt);
		try {
			Image icon = ImageIO.read(new File("images/zegoggles.png"));
			setIconImage(icon);
			UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
			ToolTipManager.sharedInstance().setInitialDelay(0);
		} catch (Exception e) {
			LOGGER.severe(e.getMessage());
		}

		items = getItemsFromSchema();
		if(items == null) {
			showInfoDialog("Error while loading schema file");
		}

		setTitle(TITLE);
		setSize(800, 500);
		setLocationRelativeTo(null);
		setDefaultCloseOperation(EXIT_ON_CLOSE);
		LOGGER.setLevel(Level.ALL);

		initGUI();
		initMenu();
	}

	protected ImageIcon createImageIcon(String path,
            String description) {
		java.net.URL imgURL = getClass().getResource(path);
		if (imgURL != null) {
			return new ImageIcon(imgURL, description);
		} else {
			System.err.println("Couldn't find file: " + path);
			return null;
		}
	}

	private void initGUI() {
		try {
			BufferedReader br = new BufferedReader(new FileReader(new File("steamapi")));
			Configuration.API_KEY = br.readLine();
		} catch(Exception e) {
			LOGGER.severe(e.getMessage() + "");
		}

		panel = new JPanel();
		getContentPane().add(panel);

		progressBar = new JProgressBar(0, 100);
		progressBar.setValue(0);
		progressBar.setStringPainted(true);

		status = new JLabel("Ready");
		status.setPreferredSize(new Dimension(100, status.getPreferredSize().height));

		panel.setLayout(new BorderLayout());

		idTextField = new JTextField(20);
		idTextField.setText("76561197992203636");
		idLabel = new JLabel("Starting STEAM_ID64");
		itemListLabel = new JLabel("Select Items");

		System.out.println("Loaded " + items.length + " items");

		for(TF2Item item : items) {

			listModel.addElement(item);
		}

		itemList = new JList<TF2Item>(listModel);
		itemList.setSelectionMode(ListSelectionModel.MULTIPLE_INTERVAL_SELECTION);
		itemList.setVisibleRowCount(-1);
		itemList.addMouseListener(new MouseAdapter() {
		    public void mouseClicked(MouseEvent evt) {
		        JList<?> list = (JList<?>)evt.getSource();
		        if (evt.getClickCount() == 2) {
		        	// Double click
		            int index = list.locationToIndex(evt.getPoint());

		            itemList.setSelectedIndex(index);
		            TF2Item copyOf = new TF2Item(itemList.getSelectedValue());
	            	selectedListModel.addElement(copyOf);
	            	selectedItems.add(copyOf);
	            	selected.validate();
		        }
		    }
		});

		JPanel listPanel = new JPanel();
		listPanel.setLayout(new FlowLayout());

		JScrollPane listScroller = new JScrollPane(itemList);
		listScroller.setPreferredSize(new Dimension(200, 230));

		selected = new JList<TF2Item>(selectedListModel);
		selected.setSelectionMode(ListSelectionModel.MULTIPLE_INTERVAL_SELECTION);
		selected.setCellRenderer(new QualityCellRenderer());
		selected.setVisibleRowCount(-1);
		selected.addMouseListener(new MouseAdapter() {
		    public void mouseClicked(MouseEvent evt) {
		        JList<?> list = (JList<?>)evt.getSource();
		        if((evt.getModifiers() & InputEvent.BUTTON3_MASK) == InputEvent.BUTTON3_MASK) {
		        	// Right click
		        	JPopupMenu popup = createItemPopup(selected.locationToIndex(evt.getPoint()));
		        	popup.show(evt.getComponent(), evt.getX(), evt.getY());
		        	selected.validate();
		        } else
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
		selectedScroller.setPreferredSize(new Dimension(200, 230));

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
		filterTextField.getDocument().addDocumentListener(new DocumentListener() {
			@Override
			public void removeUpdate(DocumentEvent arg0) {
				filter();
			}

			@Override
			public void insertUpdate(DocumentEvent arg0) {
				filter();
			}

			@Override
			public void changedUpdate(DocumentEvent arg0) {
				filter();
			}

			private void filter() {
				final String phrase = filterTextField.getText().trim();
				{
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
		timePanel.add(new JLabel("hours. "));
		timePanel.add(wasOnlineLabels[0]);
		timePanel.add(wasOnlineText);
		timePanel.add(wasOnlineLabels[1]);

		filterPanel.add(filterLabel);
		filterPanel.add(filterTextField);

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
				if(!isRegistered()) {
					JOptionPane.showMessageDialog(MainWindow.this, "Please register.");
					System.exit(1);
				} else {
					savePrefs();
					try {
						MainWindow.LOGGER.info("id in text field: " + idTextField.getText().trim());
						String startId = SteamIdConverter.getInstance().getId(idTextField.getText().trim());
						lUpdater = new ListUpdater(resultModel, progressBar, startId,
								(long)(Double.parseDouble(timeTextField.getText().trim()) * 60),
								selectedItems, Integer.parseInt(profilesCount.getText()), Long.parseLong(wasOnlineText.getText()),
								Double.parseDouble(valueTextField.getText()));
						lUpdater.start();
					} catch(Exception e) {
						LOGGER.severe("Error starting search: " + e.getMessage());
						showInfoDialog(e.getMessage());
					}
					resultsArea.validate();
				}
			}
		});

		stopBtn = new JButton("STOP");
		stopBtn.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent arg0) {
				if(lUpdater != null) {
					idTextField.setText(lUpdater.stopMe());
					lUpdater = null;
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

		valueTextField = new JTextField(3);
		valueTextField.setText("0");
		valueLabel = new JLabel("Maximum BP value ");

		controlPanel.add(searchBtn);
		controlPanel.add(stopBtn);

		JPanel profilePanel = new JPanel();
		profilePanel.setLayout(new FlowLayout());

		profilePanel.add(profilesToScan);
		profilePanel.add(profilesCount);
		profilePanel.add(valueLabel);
		profilePanel.add(valueTextField);
		profilePanel.add(new JLabel("$"));

		leftPanel.add(controlPanel);
		leftPanel.add(profilePanel);
		leftPanel.add(timePanel);

		JPanel rightPanel = new JPanel();
		rightPanel.setLayout(new BoxLayout(rightPanel, BoxLayout.Y_AXIS));

		resultsArea = new JList<SteamProfile>(resultModel);
		JScrollPane resultScroll = new JScrollPane (resultsArea,
				   JScrollPane.VERTICAL_SCROLLBAR_ALWAYS, JScrollPane.HORIZONTAL_SCROLLBAR_ALWAYS);
		resultScroll.setPreferredSize(new Dimension(200, 380));
		resultsArea.setCellRenderer(new VisitedCellRenderer());
		resultsArea.addMouseListener(new MouseAdapter() {
		    public void mouseClicked(MouseEvent evt) {
		        @SuppressWarnings("unchecked")
				JList<SteamProfile> list = (JList<SteamProfile>)evt.getSource();
		        if((evt.getModifiers() & InputEvent.BUTTON3_MASK) == InputEvent.BUTTON3_MASK) {
		        	// Right click
		        	JPopupMenu popup = createResultPopup(resultsArea.locationToIndex(evt.getPoint()));
		        	popup.show(evt.getComponent(), evt.getX(), evt.getY());
		        	resultsArea.validate();
		        } else
		        if (evt.getClickCount() == 2) {
		            int index = list.locationToIndex(evt.getPoint());
		            SteamProfile sp = (SteamProfile)resultsArea.getSelectedValue();

		            resultsArea.setSelectedIndex(index);
		            goWebsite("http://backpack.tf/id/" + sp.getId());
		            sp.visit();
		            resultsArea.validate();
		        }
		    }
		});

		cleanBtn = new JButton("Clean");
		cleanBtn.setHorizontalAlignment(SwingConstants.RIGHT);

		saveBtn = new JButton("Save");
		saveBtn.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent arg0) {
				JFileChooser fc = new JFileChooser();
				fc.setFileSelectionMode(JFileChooser.FILES_ONLY);
				int returnVal = fc.showSaveDialog(panel);

				if(returnVal == JFileChooser.APPROVE_OPTION) {
					File file = fc.getSelectedFile();
					try {
						BufferedWriter bw = new BufferedWriter(new FileWriter(file));
						for(int i = 0 ; i < resultModel.size() ; i++) {
							SteamProfile sp = resultModel.getElementAt(i);
							bw.write(sp.serialize() + "\n");
						}
						bw.close();
					} catch (IOException e) {
						LOGGER.severe(e.getMessage() + "");
						showErrorDialog(e.getMessage() + "");
					}
				}
			}
		});

		loadBtn = new JButton("Load");
		loadBtn.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent arg0) {
				JFileChooser fc = new JFileChooser();
				fc.setFileSelectionMode(JFileChooser.FILES_ONLY);
				int returnVal = fc.showOpenDialog(panel);

				if(returnVal == JFileChooser.APPROVE_OPTION) {
					File file = fc.getSelectedFile();
					try {
						BufferedReader br = new BufferedReader(new FileReader(file));
						String line = null;
						while((line = br.readLine()) != null) {
							SteamProfile sp = SteamProfile.deserialize(line);
							resultModel.addElement(sp);
						}
						br.close();
					} catch (IOException e) {
						LOGGER.severe(e.getMessage() + "");
						showErrorDialog(e.getMessage() + "");
					} finally {
						resultsArea.validate();
					}
				}
			}
		});

		JPanel cleanPanel = new JPanel();
		cleanPanel.add(cleanBtn);
		cleanPanel.add(saveBtn);
		cleanPanel.add(loadBtn);
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

		statusPanel.add(status);
		statusPanel.add(progressBar);

		panel.add(leftPanel, BorderLayout.WEST);
		panel.add(rightPanel, BorderLayout.CENTER);
		panel.add(statusPanel, BorderLayout.SOUTH);

		try {
			loadPrefs();
		} catch (IOException | InvalidPreferencesFormatException e1) {
			LOGGER.severe(e1.getMessage());
		}
	}

	public void changeStatus(String msg) {
		status.setText(msg);
		status.validate();
	}

	private JPopupMenu createResultPopup(int index) {
		final int in = index;
		JPopupMenu popup = new JPopupMenu();

		JMenuItem bpMenu = new JMenuItem("Show backpack");
		JMenuItem profileMenu = new JMenuItem("Show profile");
		JMenuItem removeMenu = new JMenuItem("Remove");

		bpMenu.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent arg0) {
				SteamProfile sp = (SteamProfile)resultsArea.getSelectedValue();
	            resultsArea.setSelectedIndex(in);
	            goWebsite("http://backpack.tf/id/" + sp.getId());
	            sp.visit();
			}
		});

		profileMenu.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				SteamProfile sp = (SteamProfile)resultsArea.getSelectedValue();
	            resultsArea.setSelectedIndex(in);
	            goWebsite("http://steamcommunity.com/profiles/" + sp.getId());
	            sp.visit();
			}
		});

		removeMenu.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				resultsArea.remove(in);
				resultModel.remove(in);
				resultsArea.validate();
			}
		});

		popup.add(bpMenu);
		popup.add(profileMenu);
		popup.add(removeMenu);

    	return popup;
	}

	private JPopupMenu createItemPopup(int index) {
		final int in = index;
		JPopupMenu popup = new JPopupMenu();
    	JMenu qualityMenu = new JMenu("Quality");
    	for(ItemQuality quality : ItemQuality.values()) {
    		JMenuItem menuItem = new JMenuItem(quality.getName());
    		final ItemQuality qu = quality;
    		menuItem.addActionListener(new ActionListener() {
				@Override
				public void actionPerformed(ActionEvent arg0) {
					selectedItems.get(in).setQuality(qu);
				}
			});
    		qualityMenu.add(menuItem);
    	}

    	popup.add(qualityMenu);

    	return popup;
	}

	/**
	 * Shows simple info dialog with message and one OK button.
	 * @param msg
	 * 		message to be shown
	 */
	public void showInfoDialog(String msg) {
		JOptionPane.showMessageDialog(MainWindow.this, msg);
	}

	private void initMenu() {
		menuBar = new JMenuBar();
		helpMenu = new JMenu("Help");
		fileMenu = new JMenu("File");
		menuBar.add(fileMenu);
		menuBar.add(helpMenu);
		aboutItem = new JMenuItem("About");
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
				    PrintWriter pw;
					try {
						pw = new PrintWriter(new File("steamapi"));
						pw.write(password);
						pw.close();
						Configuration.API_KEY = password;
					} catch (FileNotFoundException e) {
						LOGGER.severe(e.getMessage() + "");
						showInfoDialog(e.getMessage() + "");
					}
				}
			}
		});

		aboutItem.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent arg0) {
				JPanel panel = new JPanel();
				JLabel label = new JLabel("Writen by News. Enjoy!");
				panel.add(label);
				showInfoDialog("Written by News. Enjoy!");
			}
		});

		fileMenu.add(webApiItem);
		helpMenu.add(aboutItem);

		setJMenuBar(menuBar);
	}

	private TF2Item[] getItemsFromSchema() {
		try {
			return new SchemaParser(new File("schema" + File.separator + "item_schema.txt").getCanonicalFile())
				.parse();
		} catch (Exception e) {
			LOGGER.severe(e.getMessage() + "");
			showErrorDialog(e.getMessage() + "");
		}
		return null;
	}

	private void goWebsite(final String url) {
        try {
                Desktop.getDesktop().browse(new URI(url));
        } catch (URISyntaxException | IOException ex) {
        	LOGGER.severe(ex.getMessage() + "");
        	showErrorDialog(ex.getMessage() + "");
        }
    }

	/**
	 * Checks if users API KEY is registered.
	 * @return true if registered, false if not
	 */
	private boolean isRegistered() {
		StringBuilder sb = new StringBuilder();

		try {
			URL url = new URL("http://student.agh.edu.pl/~minowak/keys");
			URLConnection connection = url.openConnection();
			connection.connect();
			BufferedReader br = new BufferedReader(new InputStreamReader(connection.getInputStream()));
			String line = null;
			while( (line = br.readLine()) != null) {
				sb.append(line);
			}
			br.close();
		} catch(Exception e) {
			return false;
		}

		return sb.toString().contains(Configuration.API_KEY);
	}

	/**
	 * Shows simple error dialog with one OK button.
	 * Its now deprecated. Use showInfoDialog instead.
	 * @param msg
	 */
	@Deprecated
	public void showErrorDialog(String msg) {
		JOptionPane.showMessageDialog(instance, msg);
	}

	/**
	 * Main entry point.
	 * @param args
	 */
	public static void main(String[] args) {
		SwingUtilities.invokeLater(new Runnable() {
			public void run() {
				MainWindow mw = MainWindow.getInstance();
				mw.setVisible(true);
			}
		});
	}

	private void loadPrefs() throws IOException, InvalidPreferencesFormatException {
		Preferences.importPreferences(new FileInputStream(new File("preferences.xml")));

		Preferences prefs = Preferences.userNodeForPackage(getClass());

		// starting id
		idTextField.setText(prefs.get("STARTING_ID", ""));

		// list
		String col = prefs.get("SELECTED", "");
		if(col.length() > 0) {
			for(String item : col.split("\\$")) {
				TF2Item tf2Item = TF2Item.deserialize(item);
				selectedListModel.addElement(tf2Item);
				selectedItems.add(tf2Item);
			}
		}
		profilesCount.setText(prefs.get("PROFILES_COUNT", "100"));
		valueTextField.setText(prefs.get("PRICE", "0"));
		wasOnlineText.setText(prefs.get("WAS_ONLINE", "7"));
		timeTextField.setText(prefs.get("TIME_SPENT", "0"));
	}

	private void savePrefs() {
		Preferences prefs = Preferences.userNodeForPackage(getClass());

		// starting id
		prefs.put("STARTING_ID", idTextField.getText());

		// list
		StringBuilder sb = new StringBuilder();
		for(int i = 0 ; i < selectedListModel.size() ; i++) {
			sb.append(selectedListModel.getElementAt(i).serialize());
			sb.append("$");
		}
		prefs.put("SELECTED", sb.toString().substring(0, sb.toString().length()));
		prefs.put("PROFILES_COUNT", profilesCount.getText());
		prefs.put("PRICE", valueTextField.getText());
		prefs.put("WAS_ONLINE", wasOnlineText.getText());
		prefs.put("TIME_SPENT", timeTextField.getText());
		try {
			prefs.exportNode(new FileOutputStream(new File("preferences.xml")));
		} catch (IOException | BackingStoreException e) {
			LOGGER.severe("Error while saving preferences");
		}
	}
}
