package com.minowak.scanner.gui;

import java.util.Date;
import java.util.LinkedList;
import java.util.List;

import javax.swing.DefaultListModel;
import javax.swing.JProgressBar;

import com.minowak.scanner.engine.IDGenerator;
import com.minowak.scanner.engine.SteamUser;
import com.minowak.scanner.schema.TF2Item;
import com.minowak.scanner.utils.Configuration;

public class ListUpdater extends Thread {
	private DefaultListModel<String> list;
	private String id;
	private SteamUser user;
	private long time;
	private List<Integer> ids;
	private JProgressBar statusBar;
	private int count;
	private long wasOnline;

	public ListUpdater(DefaultListModel<String> list, JProgressBar progressBar, String id, long time, List<TF2Item> items, int count, long wasOnline) {
		this.list = list;
		this.statusBar = progressBar;
		this.id = id;
		this.time = time;
		this.count = count;
		this.wasOnline = wasOnline;
		this.ids = new LinkedList<Integer>();
		for(TF2Item it : items) {
			this.ids.add(it.getDefinitionIndex());
		}
		System.out.println("ids=" + ids);
		System.out.println("apikey=" + Configuration.API_KEY);
	}

	public void run() {
		String currId = id;
		IDGenerator gen = new IDGenerator(id);
		boolean found = false;

		int percent = 0;
		int max = count;

		while(count > 0) {
			double d1 = (double)(max - count);
			double d2 = d1/(double)max;

			statusBar.setValue((int)(d2 * 100.0));
			count--;
			user = new SteamUser(currId);
			System.out.println("Checking id: " + currId);
			try {
				if(!user.init()) {
					currId = gen.next();
					continue;
				}
			} catch(Exception e) {
				e.printStackTrace();
			}
			if(user.isPremium()) {
				System.out.println("ispremium");
				if(time == 0 || user.played() < time) {
					// TODO check date
					System.out.println("time is good");
					for(int itemId : ids) {
						if(user.hasItem(itemId)) {
							found = true;
							break;
						}
					}
				}
			}

			if(found) {
				list.addElement(currId);
			}

			System.out.println("Getting next id...");
			currId = gen.next();
			System.out.println("" + currId);
		}

		statusBar.setValue(100);
	}

	public void stopMe() {
		super.stop();
	}
}
