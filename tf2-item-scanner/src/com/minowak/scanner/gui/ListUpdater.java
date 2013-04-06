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

	private static final long DAY = 86400000;

	private List<String> steamIds = new LinkedList<String>();
	private List<String> scanned = new LinkedList<String>();

	private String currId;

	public ListUpdater(DefaultListModel<SteamProfile> list, JProgressBar progressBar, String id, long time, List<TF2Item> items, int count, long wasOnline, double maxVal) {
		this.list = list;
		this.statusBar = progressBar;
		this.id = id;
		this.time = time;
		this.count = count;
		this.wasOnline = wasOnline;
		this.items = items;
		this.maxVal = maxVal;
	}

	public void run() {
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

			try {
				if(!user.init()) {
					if(steamIds.size() > 0) {
						currId = steamIds.get(0);
						steamIds.remove(0);
						if(scanned.contains(currId)){
							continue;
						}
						scanned.add(currId);
					} else {
						currId = gen.next();
					}
					continue;
				}
			} catch(Exception e) {
				if(e.getMessage() != null)
					MainWindow.getInstance().showErrorDialog(e.getMessage() + "");
			}

			for(String s : user.getFriendsIds()) {
				if(!scanned.contains(s) && !steamIds.contains(s)) {
					steamIds.add(s);
				}
			}

			Set<TF2Item> searchedFor = new HashSet<TF2Item>();
			if(user.isPremium()) {
				double bpValue = user.getValue();
				if(time == 0 || user.played() < time) {
					long ss = System.currentTimeMillis();
					System.out.println("today-flag  =" + (ss - (ss - wasOnline*DAY)));
					System.out.println("today-logoff=" + (ss-user.lastOnline()*1000));
					if((ss - (ss - wasOnline*DAY) > ss-user.lastOnline()*1000)
							&& (maxVal == 0.0 || bpValue <= maxVal)) {
						for(TF2Item itemId : items) {
							if(user.hasItem(itemId.getDefinitionIndex(), itemId.getQuality())) {
								found = true;
								searchedFor.add(itemId);
								//break;
							}
						}
					}
				}
			}

			if(found) {
				SteamProfile sp = new SteamProfile(user.getName(), currId);
				sp.putSearchedFor(searchedFor);
				list.addElement(sp);
				found = false;
			}

			scanned.add(currId);
			steamIds.remove(currId);

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

	public String stopMe() {
		super.stop();
		return currId;
	}
}
