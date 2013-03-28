package com.minowak.scanner.schema;

public class TF2Item {
	private String name;
	private int definitionIndex;
	private int quality = 0;

	public int getQuality() {
		return quality;
	}

	public int getDefinitionIndex() {
		return definitionIndex;
	}

	public String getName() {
		return name;
	}

	public void setQuality(int quality) {
		this.quality = quality;
	}

	public void setDefinitionIndex(int definitionIndex) {
		this.definitionIndex = definitionIndex;
	}

	public void setName(String name) {
		this.name = name;
	}

	public TF2Item(String name, int definitionIndex, int quality) {
		this.name = name;
		this.definitionIndex = definitionIndex;
		this.quality = quality;
	}

	@Override
	public String toString() {
		return name;
	}
}
