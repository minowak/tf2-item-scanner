package com.minowak.scanner.schema;

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
}
