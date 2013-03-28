package com.minowak.scanner.schema;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.LinkedList;
import java.util.List;

public class SchemaParser {
	private File schemaFile;

	public SchemaParser(File file) {
		this.schemaFile = file;
	}

	public TF2Item[] parse() throws IOException {
		List<TF2Item> list = new LinkedList<TF2Item>();

		BufferedReader br = new BufferedReader(new FileReader(schemaFile));

		String line = null;
		int defindex = 0;
		int quality = 0;
		String name = new String("");
		boolean added = false;
		while((line = br.readLine()) != null) {
			if(line.contains("defindex")) {
				String tmp = line.split(":")[1];
				defindex = Integer.parseInt(tmp.substring(0, tmp.length() - 1).trim());
			}
			if(line.contains("item_name")) {
				String[] tmp3 = line.split(":");
				String tmp = new String();
				for(int i = 1 ; i < tmp3.length ; i++) {
					tmp += tmp3[i];
				}
				String tmp2 = tmp.substring(0, tmp.length() - 1).trim();
				name = tmp2.substring(1, tmp2.length() - 1);
			}
			if(line.contains("item_quality")) {
				String tmp = line.split(":")[1];
				quality = Integer.parseInt(tmp.substring(0, tmp.length() - 1).trim());
				added = true;
			}
			if(added) {
				list.add(new TF2Item(name, defindex, quality));
				added = false;
			}
		}
		return list.toArray(new TF2Item[list.size()]);
	}
}
