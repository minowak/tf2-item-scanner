package com.minowak.scanner.engine;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLConnection;

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

	protected static int getResponseCode(String urlString) throws MalformedURLException, IOException {
	    URL u = new URL(urlString);
	    HttpURLConnection huc =  (HttpURLConnection)  u.openConnection();
	    huc.setRequestMethod("GET");
	    huc.connect();
	    return huc.getResponseCode();
	}

}