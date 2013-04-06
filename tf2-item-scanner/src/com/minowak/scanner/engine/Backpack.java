package com.minowak.scanner.engine;

import java.util.LinkedList;
import java.util.List;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import com.minowak.scanner.schema.ItemQuality;
import com.minowak.scanner.schema.TF2Item;
import com.minowak.scanner.utils.Configuration;

public class Backpack extends SteamEntity {
	private String apiUrl;
	private long bpSize = 0;
	private List<TF2Item> items;
	private static final Double REFINED_PRICE = 0.35;

	public Backpack(String id) {
		apiUrl = String.format("http://api.steampowered.com/IEconItems_440/GetPlayerItems/v0001/?key=%s&format=json&SteamID=%s",
				Configuration.API_KEY, id);
		items = new LinkedList<TF2Item>();
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
			ItemQuality cQuality = ItemQuality.NORMAL;
			switch((int)(long)it.get("quality")) {
				case 0: cQuality = ItemQuality.NORMAL; break;
				case 1: cQuality = ItemQuality.GENUINE; break;
				case 3: cQuality = ItemQuality.VINTAGE; break;
				case 5: cQuality = ItemQuality.UNUSUAL; break;
				case 6: cQuality = ItemQuality.UNIQUE; break;
				case 8: cQuality = ItemQuality.VALVE; break;
				case 11: cQuality = ItemQuality.STRANGE; break;
				case 13: cQuality = ItemQuality.HAUNTED; break;
			}
			items.add(new TF2Item("N/A", (long)it.get("defindex"), cQuality));
		}

		return true;
	}

	public boolean hasItem(long itemId, ItemQuality quality) {
		return items.contains(new TF2Item("N/A", itemId, quality));
	}

	public boolean hasUnusual() {
		for(TF2Item item : items) {
			if(item.getQuality().equals(ItemQuality.UNUSUAL)) {
				return true;
			}
		}
		return false;
	}

	public long getSize() {
		return bpSize;
	}

	public static Double getValue(String id) throws ParseException {
		String api = String.format("http://backpack.tf/api/IGetUsers/v2/?steamids=%s&format=json", id);
		String jsonResponse = Backpack.getJson(api);
		if(jsonResponse == null) {
			return 0.0;
		}
		JSONParser parser = new JSONParser();
		Object responseObj = parser.parse(jsonResponse);
		JSONObject response = (JSONObject)((JSONObject) responseObj).get("response");
		JSONObject players = (JSONObject)response.get("players");
		JSONObject player = (JSONObject)players.get("0");

		if(player.get("backpack_value").getClass().equals(Long.class)) {
			return (((double)(long)player.get("backpack_value")) * REFINED_PRICE);
		} else {
			return (((double)player.get("backpack_value")) * REFINED_PRICE);
		}
	}
}
