using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTriplet {
	public PositionTriplet(int x, int y, int z) {
		this.x = x;
		this.y = y;
		this.z = z;
	}
	public int x, y, z;

	public override string ToString() {
		return "(" + x + ", " + y + ", " + z + ")";
	}
}
