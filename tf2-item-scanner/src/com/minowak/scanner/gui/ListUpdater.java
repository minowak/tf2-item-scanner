package com.minowak.scanner.gui;

import java.util.HashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Set;

import javax.swing.DefaultListModel;
import javax.swing.JProgressBar;

import com.minowak.scanner.engine.IDGenerator;
import com.minowak.scanner.engine.SteamProfile;
import com.minowak.scanner.engine.SteamUser;
import com.minowak.scanner.schema.ItemQuality;
import com.minowak.scanner.schema.TF2Item;

public class ListUpdater extends Thread {
	private DefaultListModel<SteamProfile> list;
	private String id;
	private SteamUser user;
	private long time;
	private List<TF2Item> items;
	private JProgressBar statusBar;
	private int count;
	private long wasOnline;
	private double maxVal;
	private long itemsCount;
	private boolean medals = false;

	private static final long DAY = 86400000;

	private static List<String> steamIds = new LinkedList<String>();
	private static List<String> scanned = new LinkedList<String>();

	private String currId;

	/**
	 * Consructor.
	 *
	 * @param list
	 * 		model to update
	 * @param progressBar
	 * 		progress bar to update
	 * @param id
	 * 		id to start with
	 * @param time
	 * 		time spent playing
	 * @param items
	 * 		which items to search
	 * @param count
	 * 		how many profiles
	 * @param wasOnline
	 * 		when was last online
	 * @param maxVal
	 * 		maximum backpack value
	 * @param itemsCount
	 * 		maximum items count
	 */
	private ListUpdater(DefaultListModel<SteamProfile> list,
			JProgressBar progressBar,
			String id,
			long time,
			List<TF2Item> items,
			int count,
			long wasOnline,
			double maxVal,
			long itemsCount,
			boolean m) {
		this.list = list;
		this.statusBar = progressBar;
		this.id = id;
		this.time = time;
		this.count = count;
		this.wasOnline = wasOnline;
		this.items = items;
		this.maxVal = maxVal;
		this.itemsCount = itemsCount;
		this.medals = m;
	}

	public void run() {
		MainWindow.getInstance().changeStatus("Started");
		MainWindow.LOGGER.info("Starting id is " + id);
		currId = id;
		IDGenerator gen = new IDGenerator(id);
		boolean found = false;

		int max = count;

		while(count > 0) {
			double d1 = (double)(max - count);
			double d2 = d1/(double)max;

			statusBar.setValue((int)(d2 * 100.0));
			count--;
			user = new SteamUser(currId);
			MainWindow.getInstance().changeStatus("Checking user");

			try {
				if(!user.init()) {
					MainWindow.LOGGER.info("Cannot initialize user: " + currId + "(maybe private)");
					continue;
				} else {
					for(String s : user.getFriendsIds()) {
						if(!scanned.contains(s) && !steamIds.contains(s)) {
							steamIds.add(s);
						}
					}
					MainWindow.getInstance().changeStatus("Added his friends to queue");
					MainWindow.LOGGER.info("Adding friends (size=" + steamIds.size() + ")");
				}
			} catch(Exception e) {
				MainWindow.LOGGER.severe("Error getting new user: " + e.getMessage());
				MainWindow.getInstance().changeStatus("Cannot get user info");
				continue;
			} finally {
				boolean newFound = false;
				while(steamIds.size() > 0) {
					currId = steamIds.get(0);
					steamIds.remove(0);
					if(scanned.contains(currId)){
						continue;
					}
					scanned.add(currId);
					newFound = true;
					break;
				}
				if(!newFound && (steamIds.size()) == 0) {
					MainWindow.LOGGER.info("Friends tree ended early: size=" + steamIds.size() + " newFound=" + newFound);
					MainWindow.getInstance().showInfoDialog("Friends tree ended early");
					this.stopMe();
				}
				MainWindow.LOGGER.info("Getting new id: " + currId);
			}

			Set<TF2Item> searchedFor = new HashSet<TF2Item>();
			MainWindow.LOGGER.info("Checking " + currId);
			double bpValue = user.getValue();
			if(time == 0 || user.played() < time) {
				long ss = System.currentTimeMillis();
				if((ss - (ss - wasOnline*DAY) > ss-user.lastOnline()*1000)
						&& (maxVal == 0.0 || bpValue <= maxVal)
						&& (itemsCount == 0 || itemsCount >= user.bpSize())
						&& (!medals || !user.hasMedals())) {
					if(user.hasUnusual()) {
						found = true;
						searchedFor.add(new TF2Item("UNUSUAL", 0, ItemQuality.UNUSUAL));
					} else {
						for(TF2Item itemId : items) {
							if(user.hasItem(itemId.getDefinitionIndex(), itemId.getQuality())) {
								found = true;
								searchedFor.add(itemId);
								MainWindow.LOGGER.info("Found item");
								break;
							}
						}
					}
				}
			}


			if(found) {
				SteamProfile sp = new SteamProfile(user.getName(), user.getId(), false, !user.isPremium());
				sp.putSearchedFor(searchedFor);
				list.addElement(sp);
				found = false;
			}

			if(steamIds.size() > 0) {
				currId = steamIds.get(0);
				steamIds.remove(0);
				while(steamIds.size() > 0 && scanned.contains(currId)) {
					currId = steamIds.get(0);
					steamIds.remove(0);
				}
			} else {
				currId = gen.next();
			}
		}

		statusBar.setValue(100);
	}

	@SuppressWarnings("deprecation")
	public String stopMe() {
		super.stop();
		MainWindow.getInstance().changeStatus("Stopped");
		return currId;
	}

	public Builder builder() {
		return new Builder();
	}

	public static class Builder {
		private DefaultListModel<SteamProfile> list;
		private JProgressBar progressBar;
		private String id;
		private long time;
		private List<TF2Item> items;
		private int count;
		private long wasOnline;
		private double maxVal;
		private long itemsCount;
		private boolean medals;

		public Builder list(DefaultListModel<SteamProfile> list) {
			this.list = list;
			return this;
		}

		public Builder medals(boolean m) {
			this.medals = m;
			return this;
		}

		public Builder progressBar(JProgressBar pb) {
			this.progressBar = pb;
			return this;
		}

		public Builder id(String id) {
			this.id = id;
			return this;
		}

		public Builder time(long time) {
			this.time = time;
			return this;
		}

		public Builder items(List<TF2Item> items) {
			this.items = items;
			return this;
		}

		public Builder count(int count) {
			this.count = count;
			return this;
		}

		public Builder wasOnline(long online) {
			this.wasOnline = online;
			return this;
		}

		public Builder maxVal(double val) {
			this.maxVal = val;
			return this;
		}

		public Builder itemsCount(long itemsCount) {
			this.itemsCount = itemsCount;
			return this;
		}

		public ListUpdater build() {
			return new ListUpdater(list, progressBar, id, time, items, count, wasOnline, maxVal, itemsCount, medals);
		}
	}
}
