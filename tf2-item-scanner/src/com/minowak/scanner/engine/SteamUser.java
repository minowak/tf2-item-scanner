package com.minowak.scanner.engine;


import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.JSONValue;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import com.minowak.scanner.utils.Configuration;

public class SteamUser extends SteamEntity {
	private String apiUrl;
	private String id;
	private Backpack backpack;
	private long timePlayed;

	public SteamUser(String id) {
		this.id = id;
		this.apiUrl = String.format("http://api.steampowered.com/IPlayerService/GetRecentlyPlayedGames/v0001/?key=%s&format=json&SteamID=%s",
				Configuration.API_KEY, id);
	}

	public boolean isPremium() {
		return backpack.getSize() > 50;
	}

	public long played() {
		return timePlayed;
	}

	public boolean init() throws ParseException {
		JSONParser parser = new JSONParser();
		String jsonResponse = super.getJson(apiUrl);
		if(jsonResponse == null)
			return false;
		Object responseObj = parser.parse(jsonResponse);
		JSONObject response = (JSONObject)((JSONObject) responseObj).get("response");
		JSONArray games = (JSONArray)response.get("games");

		for(int i = 0 ; i < games.size() ; i++) {
			JSONObject game = (JSONObject)games.get(i);
			if(game.get("appid").equals("440")) {
				timePlayed = (long) game.get("playtime_forever");
				break;
			}
		}

		backpack = new Backpack(id);
		return backpack.init();
	}

	public boolean hasItem(int itemId) {
		return backpack.hasItem(itemId);
	}
}
