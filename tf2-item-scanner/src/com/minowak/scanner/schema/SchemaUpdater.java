package com.minowak.scanner.schema;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.net.URL;
import java.nio.channels.Channels;
import java.nio.channels.ReadableByteChannel;

import javax.swing.JProgressBar;

public class SchemaUpdater {
	private static final String SCHEMA_FILENAME = "schema" + File.separator + "item_schema.txt";
	private static final String SCHEMA_URL = "http://git.optf2.com/schema-tracking/plain/Team%20Fortress%202%20Schema?h=teamfortress2";

	private static class SingletonHolder {
		private static SchemaUpdater INSTANCE = new SchemaUpdater();
	}

	private SchemaUpdater() {};

	public static SchemaUpdater getInstance() {
		return SingletonHolder.INSTANCE;
	}

	public void updateSchema() throws IOException {
		URL schemaUrl = new URL(SCHEMA_URL);

	    ReadableByteChannel rbc = Channels.newChannel(schemaUrl.openStream());
	    FileOutputStream fos = new FileOutputStream(SCHEMA_FILENAME);
	    fos.getChannel().transferFrom(rbc, 0, 1 << 24);
	}
}
