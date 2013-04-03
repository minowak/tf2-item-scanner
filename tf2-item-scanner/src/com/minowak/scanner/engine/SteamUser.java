package com.minowak.scanner.engine;



import java.util.LinkedList;
import java.util.List;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import com.minowak.scanner.schema.ItemQuality;
import com.minowak.scanner.utils.Configuration;

public class SteamUser extends SteamEntity {
	private String apiUrl;
	private String api2Url;
	private String api3Url;
	private String id;
	private Backpack backpack;
	private long timePlayed;
	private long online;
	private List<String> friends = new LinkedList<String>();
	private String name;

	public SteamUser(String id) {
		this.id = id;
		this.apiUrl = String.format("http://api.steampowered.com/IPlayerService/GetRecentlyPlayedGames/v0001/?key=%s&format=json&SteamID=%s",
				Configuration.API_KEY, id);
		this.api2Url = String.format("http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key=%s&steamids=%s",
				Configuration.API_KEY, id);
		this.api3Url = String.format("http://api.steampowered.com/ISteamUser/GetFriendList/v0001/?key=%s&steamid=%s&relationship=friend",
				Configuration.API_KEY, id);
	}

	public boolean isPremium() {
		return backpack.getSize() >= 300;
	}

	public long played() {
		return timePlayed;
	}

	public long lastOnline() {
		return online;
	}

	public String getName() {
		return name;
	}

	public boolean init() throws ParseException {
		JSONParser parser = new JSONParser();
		String jsonResponse = super.getJson(apiUrl);
		if(jsonResponse == null)
			return false;
		Object responseObj = parser.parse(jsonResponse);
		JSONObject response = (JSONObject)((JSONObject) responseObj).get("response");
		JSONArray games = (JSONArray)response.get("games");


		if(games != null) {
			for(int i = 0 ; i < games.size() ; i++) {
				JSONObject game = (JSONObject)games.get(i);
				if(game.get("appid").equals("440")) {
					timePlayed = (long) game.get("playtime_forever");
					break;
				}
			}
		}

		jsonResponse = super.getJson(api2Url);
		if(jsonResponse == null) {
			return false;
		}
		responseObj = parser.parse(jsonResponse);
		response = (JSONObject)((JSONObject) responseObj).get("response");
		JSONObject player = (JSONObject)((JSONArray) response.get("players")).get(0);
		online = (long)player.get("lastlogoff");
		name = (String)player.get("personaname");

		jsonResponse = super.getJson(api3Url);
		if(jsonResponse == null) {
			return false;
		}
		responseObj = parser.parse(jsonResponse);
		response = (JSONObject)((JSONObject) responseObj).get("friendslist");
		JSONArray friendsArray = (JSONArray) response.get("friends");

		for(int i = 0 ; i < friendsArray.size() ; i++) {
			JSONObject friend = (JSONObject)friendsArray.get(i);
			friends.add((String)friend.get("steamid"));
		}

		backpack = new Backpack(id);
		return backpack.init();
	}

	public List<String> getFriendsIds() {
		return friends;
	}

	public boolean hasItem(long itemId, ItemQuality quality) {
		return backpack.hasItem(itemId, quality);
	}

	public double getValue() {
		return backpack.getValue();
	}
}
