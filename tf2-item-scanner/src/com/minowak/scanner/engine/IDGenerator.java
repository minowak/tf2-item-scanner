package com.minowak.scanner.engine;

import java.math.BigInteger;

public class IDGenerator {
	private BigInteger _v = new BigInteger("76561197960265728");
	private BigInteger currentId;

	public IDGenerator(String startId) {
		BigInteger bd = new BigInteger(startId);
		bd = bd.subtract(_v);
		bd = bd.divide(new BigInteger("2"));

		currentId = bd;
	}

	public String next() {
		BigInteger bd = currentId;
		bd = bd.multiply(bd);
		bd = bd.add(_v);

		currentId = currentId.add(new BigInteger("1"));
		return bd.toString();
	}
}
