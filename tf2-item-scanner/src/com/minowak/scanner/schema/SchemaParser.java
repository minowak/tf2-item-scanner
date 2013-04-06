package com.minowak.scanner.schema;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.util.LinkedList;
import java.util.List;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;

import com.minowak.scanner.gui.MainWindow;

public class SchemaParser {
	private File schemaFile;

	public SchemaParser(File file) {
		this.schemaFile = file;
	}

	public TF2Item[] parse() throws IOException {
		List<TF2Item> list = new LinkedList<TF2Item>();

		BufferedReader br = new BufferedReader(new FileReader(schemaFile));

		String line = null;

		StringBuilder sb = new StringBuilder();

		while((line = br.readLine()) != null) {
			sb.append(line);
		}

		JSONParser parser = new JSONParser();
		try {
			Object schema = parser.parse(sb.toString());
			JSONObject result = (JSONObject)((JSONObject)schema).get("result");
			JSONArray itemsArray = (JSONArray) result.get("items");

			for(int i = 0 ; i < itemsArray.size() ; i++) {
				JSONObject item = (JSONObject)itemsArray.get(i);
				long defindex = (long)item.get("defindex");
				String name = (String)item.get("item_name");
				String imgUrl = ((String)item.get("image_url")).replace("\\", "");

				TF2Item it = new TF2Item(name, defindex, ItemQuality.UNIQUE);
				it.setImgUrl(imgUrl);
				list.add(it);
			}
		} catch (Exception e) {
			MainWindow.LOGGER.severe("Error while parsing schema: " + e.getMessage());
		}

		return list.toArray(new TF2Item[list.size()]);
	}
}
