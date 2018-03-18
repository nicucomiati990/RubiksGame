using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CubeEntry {
	public CubeEntry(CubeEntry other) {
		this.name = other.name;
		this.cube = other.cube;
	}
	public CubeName name;
	public Transform cube;
}

