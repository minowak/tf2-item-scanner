package moc.minowak.scanner.engine;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.URL;
import java.net.URLConnection;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.JSONValue;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import com.minowak.scanner.utils.Configuration;

public class SteamUser {
	private String id;
	private String apiUrl = String.format("http://api.steampowered.com/IPlayerService/GetRecentlyPlayedGames/v0001/?key=%s&format=json&SteamID=%s",
				Configuration.API_KEY, id);
	
	private Backpack backpack;
	private long timePlayed;

	public SteamUser(String id) {
		this.id = id;
	}
	
	public boolean isPremium() {
		return backpack.getSize() > 50;
	}
	
	public long played() {
		return timePlayed;
	}
	
	public void initUser() throws ParseException {
		JSONParser parser = new JSONParser();
		Object responseObj = parser.parse(getJson());
		JSONObject response = (JSONObject)((JSONObject) responseObj).get("response");
		JSONArray games = (JSONArray)response.get("games");
		
		for(int i = 0 ; i < games.size() ; i++) {
			JSONObject game = (JSONObject)games.get(i);
			if(game.get("appid").equals("440")) {
				timePlayed = (long) game.get("playtime_forever");
				break;
			}
		}
		
		// TODO init backpack
	}
	
	private String getJson() {
		StringBuilder sb = new StringBuilder();
		
		try {
			URL url = new URL(apiUrl);
			URLConnection connection = url.openConnection();
			connection.connect();
			BufferedReader br = new BufferedReader(new InputStreamReader(connection.getInputStream()));
			String line = null;
			while( (line = br.readLine()) != null) {
				sb.append(line);
			}
			br.close();
		} catch(Exception e) {
			e.printStackTrace();
		}
		
		return sb.toString();
	}
}
