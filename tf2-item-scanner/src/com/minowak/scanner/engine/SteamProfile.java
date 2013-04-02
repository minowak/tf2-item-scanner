package com.minowak.scanner.engine;

public class SteamProfile {
	private String name;
	private String id;
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
	}

	@Override
	public String toString() {
		return name + " (" + id + ")";
	}

	public boolean isVisited() {
		return visited;
	}
}
