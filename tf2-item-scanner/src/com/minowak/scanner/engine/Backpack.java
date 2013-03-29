package com.minowak.scanner.engine;

import java.util.HashSet;
import java.util.Set;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import com.minowak.scanner.utils.Configuration;

public class Backpack extends SteamEntity {
	private String id;
	private String apiUrl;
	private long bpSize = 0;
	private Set<Long> items;

	public Backpack(String id) {
		this.id = id;
		apiUrl = String.format("http://api.steampowered.com/IEconItems_440/GetPlayerItems/v0001/?key=%s&format=json&SteamID=%s",
				Configuration.API_KEY, id);
		items = new HashSet<Long>();
	}

	public boolean init() throws ParseException {
		JSONParser parser = new JSONParser();
		String jsonResponse = super.getJson(apiUrl);
		if(jsonResponse == null) {
			return false;
		}
		Object responseObj = parser.parse(jsonResponse);
		JSONObject result = (JSONObject)((JSONObject) responseObj).get("result");

		bpSize = (long)result.get("num_backpack_slots");

		JSONArray itemsArray = (JSONArray) result.get("items");

		for(int i = 0 ; i < itemsArray.size() ; i++) {
			JSONObject it = (JSONObject) itemsArray.get(i);
			items.add((long)it.get("defindex"));
		}

		return true;
	}

	public boolean hasItem(long itemId) {
		//System.out.println("Searching " + itemId + " in " + items);
		return items.contains(itemId);
	}

	public long getSize() {
		return bpSize;
	}

}
