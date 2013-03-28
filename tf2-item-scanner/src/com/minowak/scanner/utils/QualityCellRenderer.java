package com.minowak.scanner.utils;

import java.awt.Color;
import java.awt.Component;

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
		switch(item.getQuality()) {
			case 1: setForeground(Color.decode("0x4D7455")); break;
			case 3: setForeground(Color.decode("0x476291")); break;
			case 5: setForeground(Color.decode("0x8650AC")); break;
			case 6: setForeground(Color.decode("0xFFD700")); break;
			case 11: setForeground(Color.decode("0xCF6A32")); break;
			case 13: setForeground(Color.decode("0x38F3AB")); break;
			case 7:
			case 9: setForeground(Color.decode("0x70B04A")); break;
			case 8: setForeground(Color.decode("0xA50F79")); break;
			default: setForeground(Color.decode("0xB2B2B2")); break;
		}
		return this;
	}

}
