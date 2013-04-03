package com.minowak.scanner.engine;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.URL;
import java.net.URLConnection;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

public class SteamEntity {
	public SteamEntity() {
		super();
	}

	protected static String getJson(String apiUrl) {
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
			return null;
		}

		return sb.toString();
	}

}