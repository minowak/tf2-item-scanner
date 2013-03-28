package com.minowak.scanner.gui;

import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.File;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.LinkedList;
import java.util.List;

import javax.swing.JTextArea;

import com.minowak.scanner.schema.TF2Item;

public class ListUpdater extends Thread {
	private JTextArea text;
	private String id;
	private long time;
	private List<Integer> ids;

	public ListUpdater(JTextArea list, String id, long time, List<TF2Item> items) {
		this.text = list;
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
			try {
				String line;
				while((line = br.readLine()) != null) {
					text.append("\n" + line);
				}
			} catch(Exception e) {
				e.printStackTrace();
			} finally {
				if(pr != null) {
					pr.destroy();
				}
				text.append("Finished!\n");
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

	public void stopMe() {
		super.stop();
		text.append("Stopped\n");
	}
}
