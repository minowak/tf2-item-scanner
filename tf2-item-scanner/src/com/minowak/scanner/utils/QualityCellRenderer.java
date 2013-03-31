package com.minowak.scanner.utils;

import java.awt.Color;
import java.awt.Component;
import java.awt.Dimension;

import javax.swing.DefaultListCellRenderer;
import javax.swing.JList;

import com.minowak.scanner.schema.TF2Item;

public class QualityCellRenderer extends DefaultListCellRenderer {

	public QualityCellRenderer() {
		setOpaque(true);
	}

	@Override
	public Component getListCellRendererComponent(JList arg0, Object arg1,
			int arg2, boolean arg3, boolean arg4) {
		super.getListCellRendererComponent(arg0, arg1, arg2, arg3, arg4);
		TF2Item item = (TF2Item)arg1;
		setForeground(item.getQuality().getColor());
		return this;
	}

}
