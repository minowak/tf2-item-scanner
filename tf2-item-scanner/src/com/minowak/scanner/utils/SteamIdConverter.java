package com.minowak.scanner.utils;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.math.BigInteger;
import java.net.URL;
import java.net.URLConnection;

import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import com.minowak.scanner.gui.MainWindow;

/**
 * Converts ids.
 *
 * @author nowak
 */
public class SteamIdConverter {
	private static class SingletonHolder {
		public static SteamIdConverter INSTANCE = new SteamIdConverter();
	}

	private SteamIdConverter() {};

	public static SteamIdConverter getInstance() {
		return SingletonHolder.INSTANCE;
	}

	/**
	 * Converts vanity id to steamid64.
	 *
	 * @param vanity
	 * 			vanity name
	 *
	 * @return steamid64
	 */
	public String getId(String id) {
		if(id.startsWith("STEAM")) {
			String[] tmp = id.split(":");
			long x = Long.parseLong(tmp[1]);
			long y = Long.parseLong(tmp[2]);
			y *= 2;
			y += x;
			BigInteger result = new BigInteger("76561197960265728");
			result = result.add(new BigInteger("" + y));
			return result.toString();
		} else {
			return fromVanity(id);
		}
	}

	public String fromVanity(String vanity) {
		String apiUrl = String.format("http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/?key=%s&vanityurl=%s",
				Configuration.API_KEY, vanity);

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
			MainWindow.LOGGER.info("Error while retrieving steamid64: " + vanity);
			return vanity;
		}

		try {
			JSONParser parser = new JSONParser();
			Object response = parser.parse(sb.toString());
			JSONObject result = (JSONObject) ((JSONObject) response).get("response");
			String getId = (String)result.get("steamid");
			if(getId == null || getId.length() == 0) {
				return vanity;
			}
			return getId;
		} catch(ParseException e) {
			MainWindow.LOGGER.info("Error while retrieving steamid64: " + vanity);
			return vanity;
		}
	}
}
