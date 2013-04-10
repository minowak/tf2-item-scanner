package com.minowak.scanner.schema;

import java.awt.Color;

public enum ItemQuality {
	NORMAL("Stock", Color.decode("0xB2B2B2")),
	UNIQUE("Unique", Color.decode("0xFFD700")),
	VINTAGE("Vintage", Color.decode("0x476291")),
	STRANGE("Strange", Color.decode("0xCF6A32")),
	GENUINE("Genuine", Color.decode("0x4D7455")),
	HAUNTED("Haunted", Color.decode("0x38F3AB")),
	UNUSUAL("Unusual", Color.decode("0x8650AC")),
	SELFMADE("Selfmade", Color.decode("0x70B04A")),
	VALVE("Valve", Color.decode("0xA50F79"));

	private Color color;
	private String name;
	public Color getColor() {
		return color;
	}

	public String getName() {
		return name;
	}

	private ItemQuality(String n, Color c) {
		name = n;
		color = c;
	}

	@Override
	public String toString() {
		return name;
	}

	public static ItemQuality fromName(String name) {
		if(name.equals("Stock")) {
			return NORMAL;
		}
		if(name.equals("Unique")) {
			return UNIQUE;
		}
		if(name.equals("Vintage")) {
			return VINTAGE;
		}
		if(name.equals("Strange")) {
			return STRANGE;
		}
		if(name.equals("Genuine")) {
			return GENUINE;
		}
		if(name.equals("Haunted")) {
			return HAUNTED;
		}
		if(name.equals("Unusual")) {
			return UNUSUAL;
		}
		if(name.equals("Selfmade")) {
			return SELFMADE;
		}
		if(name.equals("Valve")) {
			return VALVE;
		} else return UNIQUE;
	}
}
