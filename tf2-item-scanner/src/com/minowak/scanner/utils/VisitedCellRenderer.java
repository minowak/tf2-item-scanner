package com.minowak.scanner.utils;

import java.awt.Color;
import java.awt.Component;

import javax.swing.DefaultListCellRenderer;
import javax.swing.JList;

import com.minowak.scanner.engine.SteamProfile;

public class VisitedCellRenderer extends DefaultListCellRenderer {
	private static final long serialVersionUID = 2908156440053016605L;

	@Override
	public Component getListCellRendererComponent(JList arg0, Object arg1,
			int arg2, boolean arg3, boolean arg4) {
		super.getListCellRendererComponent(arg0, arg1, arg2, arg3, arg4);
		SteamProfile sp = (SteamProfile)arg1;
		if(!sp.isVisited()) {
			setForeground(Color.BLUE);
		} else {
			setForeground(Color.BLACK);
		}
		return this;
	}
}
