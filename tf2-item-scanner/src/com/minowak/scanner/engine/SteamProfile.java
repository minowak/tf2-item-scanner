package com.minowak.scanner.engine;

import java.util.HashSet;
import java.util.Set;

import org.json.simple.parser.ParseException;

import com.minowak.scanner.schema.TF2Item;

public class SteamProfile {
	private String name;
	private String id;
	private Double value;
	private boolean visited;
	private boolean ftp = false;
	private Set<TF2Item> searchedFor = new HashSet<TF2Item>();

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
		this(name, id, false, false);
	}

	public void putSearchedFor(Set<TF2Item> map) {
		searchedFor.addAll(map);
	}

	public Set<TF2Item> getSearchedFor() {
		return searchedFor;
	}

	public SteamProfile(String name, String id, boolean visited, boolean f2p) {
		this.name = name;
		this.id = id;
		this.visited = visited;
		ftp = f2p;

		try {
			value = Backpack.getValue(id);
		} catch(ParseException e) {
			value = 0.0;
		}
	}

	@Override
	public String toString() {
		return name + " (" + id + ") - " + String.format("%.2f", value) + "$";
	}

	public String serialize() {
		return name+";"+id+";"+visited;
	}

	public static SteamProfile deserialize(String str) {
		String[] ss = str.split(";");
		return new SteamProfile(ss[0], ss[1], Boolean.parseBoolean(ss[2]), false);
	}

	public boolean isF2P() {
		return ftp;
	}

	public boolean isVisited() {
		return visited;
	}
}
