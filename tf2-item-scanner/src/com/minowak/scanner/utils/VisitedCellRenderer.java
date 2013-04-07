package com.minowak.scanner.utils;

import java.awt.Color;
import java.awt.Component;

import javax.swing.DefaultListCellRenderer;
import javax.swing.JList;

import com.minowak.scanner.engine.SteamProfile;
import com.minowak.scanner.schema.TF2Item;

public class VisitedCellRenderer extends DefaultListCellRenderer {
	private static final long serialVersionUID = 2908156440053016605L;

	@Override
	public Component getListCellRendererComponent(JList arg0, Object arg1,
			int arg2, boolean arg3, boolean arg4) {
		super.getListCellRendererComponent(arg0, arg1, arg2, arg3, arg4);
		SteamProfile sp = (SteamProfile)arg1;
		StringBuilder sb = new StringBuilder();

		sb.append("<html><b>Found:</b><br>");
		for(TF2Item item : sp.getSearchedFor()) {
			sb.append("<i>" + item + "</i>");
			sb.append("<br>");
		}
		sb.append("</html>");

		setToolTipText(sb.toString());
		if(!sp.isVisited()) {
			setForeground(Color.BLUE);
		} else {
			setForeground(Color.BLACK);
		}

		if(sp.isF2P()) {
			setBackground(Color.GRAY);
		}
		return this;
	}
}
