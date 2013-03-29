package com.minowak.scanner.gui;

import java.util.LinkedList;
import java.util.List;

import javax.swing.DefaultListModel;
import javax.swing.JLabel;

import com.minowak.scanner.engine.IDGenerator;
import com.minowak.scanner.engine.SteamUser;
import com.minowak.scanner.schema.TF2Item;

public class ListUpdater extends Thread {
	private DefaultListModel<String> list;
	private String id;
	private SteamUser user;
	private long time;
	private List<Integer> ids;
	private JLabel status;
	private int count;

	public ListUpdater(DefaultListModel<String> list, JLabel status, String id, long time, List<TF2Item> items, int count) {
		this.list = list;
		this.status = status;
		this.id = id;
		this.time = time;
		this.count = count;
		this.ids = new LinkedList<Integer>();
		for(TF2Item it : items) {
			this.ids.add(it.getDefinitionIndex());
		}
		System.out.println("ids=" + ids);
	}

	public void run() {
		String currId = id;
		IDGenerator gen = new IDGenerator(id);
		boolean found = false;

		while(count-- > 0) {
			user = new SteamUser(currId);
			System.out.println("Checking id: " + currId);
			try {
				if(!user.init()) {
					continue;
				}
			} catch(Exception e) {
				e.printStackTrace();
			} finally {
				currId = gen.next();
			}
			if(user.isPremium()) {
				System.out.println("is premium");
				if(time == 0 || user.played() < time) {
					System.out.println("time under");
					System.out.println("ids=" + ids);
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

		status.setText("Finished");
	}

	public void stopMe() {
		super.stop();
		status.setText("Stopped by user");
	}
}
