package com.minowak.scanner.engine;

import org.json.simple.parser.ParseException;

public class SteamProfile {
	private String name;
	private String id;
	private Double value;
	private boolean visited;

	public String getName() {
		return name;
	}

	public String getId() {
		return id;
	}

	public void visit() {
		visited = true;
	}

	public SteamProfile(String name, String id) {
		this(name, id, false);
	}

	public SteamProfile(String name, String id, boolean visited) {
		this.name = name;
		this.id = id;
		this.visited = visited;

		try {
			value = Backpack.getValue(id);
		} catch(ParseException e) {
			value = 0.0;
		}
	}

	@Override
	public String toString() {
		return name + " (" + id + ") - " + value + "$";
	}

	public String serialize() {
		return name+";"+id+";"+visited;
	}

	public static SteamProfile deserialize(String str) {
		String[] ss = str.split(";");
		return new SteamProfile(ss[0], ss[1], Boolean.parseBoolean(ss[2]));
	}

	public boolean isVisited() {
		return visited;
	}
}
