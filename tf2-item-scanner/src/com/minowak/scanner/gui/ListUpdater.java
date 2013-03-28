package com.minowak.scanner.gui;

import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.File;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.util.LinkedList;
import java.util.List;

import javax.swing.DefaultListModel;
import javax.swing.JLabel;

import com.minowak.scanner.schema.TF2Item;

public class ListUpdater extends Thread {
	private DefaultListModel<String> list;
	private String id;
	private long time;
	private List<Integer> ids;
	private JLabel status;

	public ListUpdater(DefaultListModel<String> list, JLabel status, String id, long time, List<TF2Item> items) {
		this.list = list;
		this.status = status;
		this.id = id;
		this.time = time;
		ids = new LinkedList<Integer>();
		for(TF2Item it : items) {
			ids.add(it.getDefinitionIndex());
		}
	}

	public void run() {
		Runtime rt = Runtime.getRuntime();
		String s = new String();
		for(Integer i : ids) {
			s += i + ",";
		}
		if(s.isEmpty()) {
			s = "0";
		}
		System.out.println("Trying to run engine with: " + id + " " + time + " " + s);
		Process pr = null;
		try {
			String cmd = new File("Python27\\python.exe").getCanonicalPath() + " " + new File("engine\\scanner.py").getCanonicalPath() + " " + id + " " + time + " " + s;
			System.out.println("cmd=" + cmd);
			pr = rt.exec(cmd);
			BufferedReader br = new BufferedReader(new InputStreamReader(new BufferedInputStream(pr.getInputStream())));
			BufferedReader br2 = new BufferedReader(new InputStreamReader(new BufferedInputStream(pr.getErrorStream())));
			status.setText("Running...");
			try {
				String line;
				while((line = br.readLine()) != null) {
					list.addElement(line);
					System.out.println(line);
				}
			} catch(Exception e) {
				e.printStackTrace();
			} finally {
				if(pr != null) {
					pr.destroy();
				}
				PrintWriter out = new PrintWriter("errors.txt");
				String line = new String();
				String all = new String();
				while((line = br2.readLine()) != null) {
					all = all + line + "\n";
				}
				out.write(all);
				out.close();
				status.setText("Finished!");
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

	public void stopMe() {
		super.stop();
		status.setText("Stopped by user");
	}
}
