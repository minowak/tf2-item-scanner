package com.minowak.scanner.schema;

import java.io.Serializable;

public class TF2Item {
	private String name;
	private long definitionIndex;
	private ItemQuality quality = ItemQuality.NORMAL;
	private String imgUrl;

	public ItemQuality getQuality() {
		return quality;
	}

	public long getDefinitionIndex() {
		return definitionIndex;
	}

	public String getName() {
		return name;
	}

	public void setQuality(ItemQuality quality) {
		this.quality = quality;
	}

	public void setImgUrl(String imgUrl) {
		this.imgUrl = imgUrl;
	}

	public String getImageUrl() {
		return imgUrl;
	}

	public void setDefinitionIndex(int definitionIndex) {
		this.definitionIndex = definitionIndex;
	}

	public void setName(String name) {
		this.name = name;
	}

	public TF2Item(TF2Item copyOf) {
		this.name = copyOf.name;
		this.definitionIndex = copyOf.definitionIndex;
		this.quality = copyOf.quality;
		this.imgUrl = copyOf.imgUrl;
	}

	public TF2Item(String name, long definitionIndex, ItemQuality quality) {
		this.name = name;
		this.definitionIndex = definitionIndex;
		this.quality = quality;
	}

	@Override
	public String toString() {
		return name;
	}

	@Override
	public boolean equals(Object o2) {
		if(o2 instanceof TF2Item) {
			TF2Item it = (TF2Item) o2;
			return (definitionIndex == it.getDefinitionIndex()) &&
					(quality == it.getQuality());
		}
		return false;
	}

	public String serialize() {
		return name + ";" + quality + ";" + definitionIndex + ";" + imgUrl;
	}

	public static TF2Item deserialize(String serialized) {
		if(serialized.length() == 0)
			return null;
		String [] attrs = serialized.split(";");
		TF2Item item = new TF2Item(attrs[0], Long.parseLong(attrs[2]), ItemQuality.fromName(attrs[1]));
		item.setImgUrl(attrs[3]);
		return item;
	}
}
