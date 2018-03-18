using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotation : MonoBehaviour {

	//all the small cubes in the rubiks cube
	public CubeEntry[] cubes;
	public CubeSolver solver;
	//public for now
	public CubeEntry[][][] cubeMatrix = new CubeEntry[3][][];

	public KeyValuePair<CubeEntry, Vector3>[] initialPositions;

	public float sequenceDelay;

	public float rotationTime;

	public ParticleSystem winParticles;
	void Awake() {
		for (int i = 0; i < 3 /*aww*/; i++) {
			cubeMatrix[i] = new CubeEntry[3][];
			for (int j = 0; j < 3 /*aww*/; j++) {
				cubeMatrix[i][j] = new CubeEntry[3];
			}
		}
		// generate initial cube matrix
		GenerateMatrix();
		GenerateResetPositions();
		solver = gameObject.GetComponent<CubeSolver>();
	}

	private Dictionary<CubeName, PositionTriplet> GetMatrixPositions() {
		Dictionary<CubeName, PositionTriplet> matrixPositions = new Dictionary<CubeName, PositionTriplet>();
		// add white pieces
		matrixPositions.Add(CubeName.White, new PositionTriplet(2, 1, 1));
		matrixPositions.Add(CubeName.WhiteBlue, new PositionTriplet(2, 1, 0));
		matrixPositions.Add(CubeName.WhiteGreen, new PositionTriplet(2, 1, 2));
		matrixPositions.Add(CubeName.WhiteRed, new PositionTriplet(2, 0, 1));
		matrixPositions.Add(CubeName.WhiteOrange, new PositionTriplet(2, 2, 1));
		matrixPositions.Add(CubeName.WhiteRedBlue, new PositionTriplet(2, 0, 0));
		matrixPositions.Add(CubeName.WhiteRedGreen, new PositionTriplet(2, 0, 2));
		matrixPositions.Add(CubeName.WhiteOrangeBlue, new PositionTriplet(2, 2, 0));
		matrixPositions.Add(CubeName.WhiteOrangeGreen, new PositionTriplet(2, 2, 2));
		// add middle layer pieces
		matrixPositions.Add(CubeName.Red, new PositionTriplet(1, 0, 1));
		matrixPositions.Add(CubeName.Orange, new PositionTriplet(1, 2, 1));
		matrixPositions.Add(CubeName.Blue, new PositionTriplet(1, 1, 0));
		matrixPositions.Add(CubeName.Green, new PositionTriplet(1, 1, 2));

		matrixPositions.Add(CubeName.RedBlue, new PositionTriplet(1, 0, 0));
		matrixPositions.Add(CubeName.RedGreen, new PositionTriplet(1, 0, 2));
		matrixPositions.Add(CubeName.OrangeBlue, new PositionTriplet(1, 2, 0));
		matrixPositions.Add(CubeName.OrangeGreen, new PositionTriplet(1, 2, 2));

		// add top layer pieces
		matrixPositions.Add(CubeName.Yellow, new PositionTriplet(0, 1, 1));
		matrixPositions.Add(CubeName.YellowBlue, new PositionTriplet(0, 1, 0));
		matrixPositions.Add(CubeName.YellowGreen, new PositionTriplet(0, 1, 2));
		matrixPositions.Add(CubeName.YellowRed, new PositionTriplet(0, 0, 1));
		matrixPositions.Add(CubeName.YellowOrange, new PositionTriplet(0, 2, 1));
		matrixPositions.Add(CubeName.YellowRedBlue, new PositionTriplet(0, 0, 0));
		matrixPositions.Add(CubeName.YellowRedGreen, new PositionTriplet(0, 0, 2));
		matrixPositions.Add(CubeName.YellowOrangeBlue, new PositionTriplet(0, 2, 0));
		matrixPositions.Add(CubeName.YellowOrangeGreen, new PositionTriplet(0, 2, 2));
		return matrixPositions;
	}

	private void GenerateResetPositions() {
		initialPositions = new KeyValuePair<CubeEntry, Vector3>[cubes.Length];
		// populate initialPositions with pairs of cubeEntry and corresponding position
		for (int i = 0; i < cubes.Length; i++) {
			initialPositions[i] = new KeyValuePair<CubeEntry, Vector3>(cubes[i], cubes[i].cube.transform.position);
		}

	}

	public void ResetCube() {
		GenerateMatrix();
		foreach (var cubie in initialPositions) {
			cubie.Key.cube.position = cubie.Value;
			cubie.Key.cube.rotation = Quaternion.identity;
		}
	}

	private void GenerateMatrix() {
		// gonna be long
		// initial matrix will have Red in front, White down
		var matrixPositions = GetMatrixPositions();

		foreach (CubeEntry entry in cubes) {
			CubeName name = entry.name;
			// find the position
			PositionTriplet position = matrixPositions[name];
			// add to matrix;
			cubeMatrix[position.x][position.y][position.z] = new CubeEntry(entry);
			//Debug.Log(name + ": " + position.ToString());
		}
	}

	private CubeEntry[] GetFrontFace() {
		// returns the F face in order
		// 1 2 3
		// 4 5 6
		// 7 8 9
		return new CubeEntry[] {
			cubeMatrix[0][0][0],
			cubeMatrix[0][0][1],
			cubeMatrix[0][0][2],

			cubeMatrix[1][0][0],
			cubeMatrix[1][0][1],
			cubeMatrix[1][0][2],

			cubeMatrix[2][0][0],
			cubeMatrix[2][0][1],
			cubeMatrix[2][0][2],
		};
	}

	private CubeEntry[] GetRightFace() {
		// returns right face in order
		// check ReturnFrontFace method for details
		return new CubeEntry[] {
			cubeMatrix[0][0][2],
			cubeMatrix[0][1][2],
			cubeMatrix[0][2][2],

			cubeMatrix[1][0][2],
			cubeMatrix[1][1][2],
			cubeMatrix[1][2][2],

			cubeMatrix[2][0][2],
			cubeMatrix[2][1][2],
			cubeMatrix[2][2][2],
		};
	}

	private CubeEntry[] GetLeftFace() {
		// returns leftface in order
		// check ReturnFrontFace method for details
		return new CubeEntry[] {
			cubeMatrix[0][2][0],
			cubeMatrix[0][1][0],
			cubeMatrix[0][0][0],

			cubeMatrix[1][2][0],
			cubeMatrix[1][1][0],
			cubeMatrix[1][0][0],

			cubeMatrix[2][2][0],
			cubeMatrix[2][1][0],
			cubeMatrix[2][0][0],
		};
	}

	private CubeEntry[] GetUpFace() {
		return new CubeEntry[] {
			cubeMatrix[0][0][0],
			cubeMatrix[0][0][1],
			cubeMatrix[0][0][2],

			cubeMatrix[0][1][0],
			cubeMatrix[0][1][1],
			cubeMatrix[0][1][2],

			cubeMatrix[0][2][0],
			cubeMatrix[0][2][1],
			cubeMatrix[0][2][2]
		};
	}

	private CubeEntry[] GetBackFace() {
		return new CubeEntry[] {
			cubeMatrix[0][2][2],
			cubeMatrix[0][2][1],
			cubeMatrix[0][2][0],

			cubeMatrix[1][2][2],
			cubeMatrix[1][2][1],
			cubeMatrix[1][2][0],

			cubeMatrix[2][2][2],
			cubeMatrix[2][2][1],
			cubeMatrix[2][2][0]
		};
	}

	private CubeEntry[] GetDownFace() {
		return new CubeEntry[] {
			cubeMatrix[2][0][0],
			cubeMatrix[2][0][1],
			cubeMatrix[2][0][2],

			cubeMatrix[2][1][0],
			cubeMatrix[2][1][1],
			cubeMatrix[2][1][2],

			cubeMatrix[2][2][0],
			cubeMatrix[2][2][1],
			cubeMatrix[2][2][2]
		};
	}

	private CubeEntry[] GetMiddleFace() {
		return new CubeEntry[] {
			cubeMatrix[0][0][1],
			cubeMatrix[0][1][1],
			cubeMatrix[0][2][1],

			cubeMatrix[1][0][1],
			cubeMatrix[1][2][1],

			cubeMatrix[2][0][1],
			cubeMatrix[2][1][1],
			cubeMatrix[2][2][1]
		};
	}

	// Use this for initialization
	void Start() {
		
	}

	IEnumerator RotateCoroutine(CubeEntry cubeEntry, Vector3 target, float time) {
		float elapsed = 0f;
		
		Quaternion targetRot = Quaternion.Euler(target) * cubeEntry.cube.rotation;
		while (elapsed < time) {	
			cubeEntry.cube.localRotation = Quaternion.Lerp(
				cubeEntry.cube.rotation,
				targetRot,
				Time.deltaTime * 30
			);
			elapsed += Time.deltaTime;
			yield return null;
		}
	}

	private void RotateF() {
		CubeEntry[] face = GetFrontFace();
		foreach (CubeEntry cube in face) {
			//cube.cube.Rotate(new Vector3(0f, 0f, 90f), Space.World);
			StartCoroutine(RotateCoroutine(cube, new Vector3(0f, 0f, 90f), rotationTime));
		}
	
		// adjust main matrix values to correspond to new cube
		// tbw
		cubeMatrix[0][0][0] = face[6];
		cubeMatrix[0][0][1] = face[3];
		cubeMatrix[0][0][2] = face[0];

		cubeMatrix[1][0][0] = face[7];
		cubeMatrix[1][0][1] = face[4];
		cubeMatrix[1][0][2] = face[1];

		cubeMatrix[2][0][0] = face[8];
		cubeMatrix[2][0][1] = face[5];
		cubeMatrix[2][0][2] = face[2];
		
	}

	private void RotateR() {
		CubeEntry[] face = GetRightFace();
		foreach (CubeEntry cube in face) {
			//cube.cube.Rotate(new Vector3(-90f, 0f, 0f), Space.World);
			StartCoroutine(RotateCoroutine(cube, new Vector3(-90f, 0f, 0f), rotationTime));
		}
		// adjust main matrix values to correspond to new cube
		cubeMatrix[0][0][2] = face[6];
		cubeMatrix[0][1][2] = face[3];
		cubeMatrix[0][2][2] = face[0];

		cubeMatrix[1][0][2] = face[7];
		cubeMatrix[1][1][2] = face[4];
		cubeMatrix[1][2][2] = face[1];

		cubeMatrix[2][0][2] = face[8];
		cubeMatrix[2][1][2] = face[5];
		cubeMatrix[2][2][2] = face[2];
	}

	private void RotateL() {
		CubeEntry[] face = GetLeftFace();
		foreach (CubeEntry cube in face) {
			// cube.cube.Rotate(new Vector3(90f, 0f, 0f), Space.World);
			StartCoroutine(RotateCoroutine(cube, new Vector3(90f, 0f, 0f), rotationTime));
		}
		
		// adjust main matrix values to correspond to new cube
		cubeMatrix[0][0][0] = face[0];
		cubeMatrix[0][1][0] = face[3];
		cubeMatrix[0][2][0] = face[6];

		cubeMatrix[1][0][0] = face[1];
		cubeMatrix[1][1][0] = face[4];
		cubeMatrix[1][2][0] = face[7];

		cubeMatrix[2][0][0] = face[2];
		cubeMatrix[2][1][0] = face[5];
		cubeMatrix[2][2][0] = face[8];
	}

	private void RotateU() {
		CubeEntry[] face = GetUpFace();
		foreach (CubeEntry cube in face) {
			// cube.cube.Rotate(new Vector3(90f, 0f, 0f), Space.World);
			StartCoroutine(RotateCoroutine(cube, new Vector3(0f, 90f, 0f), rotationTime));
		}

		// adjust main matrix values to correspond to new cube
		cubeMatrix[0][0][0] = face[2];
		cubeMatrix[0][0][1] = face[5];
		cubeMatrix[0][0][2] = face[8];

		cubeMatrix[0][1][0] = face[1];
		cubeMatrix[0][1][1] = face[4];
		cubeMatrix[0][1][2] = face[7];

		cubeMatrix[0][2][0] = face[0];
		cubeMatrix[0][2][1] = face[3];
		cubeMatrix[0][2][2] = face[6];

	}

	private void RotateB() {
		CubeEntry[] face = GetBackFace();
		foreach (CubeEntry cube in face) {
			StartCoroutine(RotateCoroutine(cube, new Vector3(0f, 0f, -90f), rotationTime));
		}

		// adjust main matrix values to correspond to new cube
		cubeMatrix[0][2][2] = face[6];
		cubeMatrix[0][2][1] = face[3];
		cubeMatrix[0][2][0] = face[0];

		cubeMatrix[1][2][2] = face[7];
		cubeMatrix[1][2][1] = face[4];
		cubeMatrix[1][2][0] = face[1];

		cubeMatrix[2][2][2] = face[8];
		cubeMatrix[2][2][1] = face[5];
		cubeMatrix[2][2][0] = face[2];
	}

	private void RotateD() {
		CubeEntry[] face = GetDownFace();
		foreach (CubeEntry cube in face) {
			StartCoroutine(RotateCoroutine(cube, new Vector3(0f, -90f, 0f), rotationTime));
		}

		// adjust main matrix values to correspond to new cube
		cubeMatrix[2][0][0] = face[6];
		cubeMatrix[2][0][1] = face[3];
		cubeMatrix[2][0][2] = face[0];

		cubeMatrix[2][1][0] = face[7];
		cubeMatrix[2][1][1] = face[4];
		cubeMatrix[2][1][2] = face[1];

		cubeMatrix[2][2][0] = face[8];
		cubeMatrix[2][2][1] = face[5];
		cubeMatrix[2][2][2] = face[2];
	}

	// inverse moves
	private void RotateFi() {
		CubeEntry[] face = GetFrontFace();
		foreach (CubeEntry cube in face) {
			//cube.cube.Rotate(new Vector3(0f, 0f, 90f), Space.World);
			StartCoroutine(RotateCoroutine(cube, new Vector3(0f, 0f, -90f), rotationTime));
		}

		// adjust main matrix values to correspond to new cube
		// tbw
		cubeMatrix[0][0][0] = face[2];
		cubeMatrix[0][0][1] = face[5];
		cubeMatrix[0][0][2] = face[8];

		cubeMatrix[1][0][0] = face[1];
		cubeMatrix[1][0][1] = face[4];
		cubeMatrix[1][0][2] = face[7];

		cubeMatrix[2][0][0] = face[0];
		cubeMatrix[2][0][1] = face[3];
		cubeMatrix[2][0][2] = face[6];
	}

	private void RotateRi() {
		CubeEntry[] face = GetRightFace();
		foreach (CubeEntry cube in face) {
			//cube.cube.Rotate(new Vector3(-90f, 0f, 0f), Space.World);
			StartCoroutine(RotateCoroutine(cube, new Vector3(90f, 0f, 0f), rotationTime));
		}
		// adjust main matrix values to correspond to new cube
		cubeMatrix[0][0][2] = face[2];
		cubeMatrix[0][1][2] = face[5];
		cubeMatrix[0][2][2] = face[8];

		cubeMatrix[1][0][2] = face[1];
		cubeMatrix[1][1][2] = face[4];
		cubeMatrix[1][2][2] = face[7];

		cubeMatrix[2][0][2] = face[0];
		cubeMatrix[2][1][2] = face[3];
		cubeMatrix[2][2][2] = face[6];
		//foreach (CubeEntry e in GetRightFace()) {
		//	Debug.Log(e.name);
		//}
	}

	private void RotateLi() {
		CubeEntry[] face = GetLeftFace();
		foreach (CubeEntry cube in face) {
			// cube.cube.Rotate(new Vector3(90f, 0f, 0f), Space.World);
			StartCoroutine(RotateCoroutine(cube, new Vector3(-90f, 0f, 0f), rotationTime));
		}

		// adjust main matrix values to correspond to new cube
		cubeMatrix[0][0][0] = face[8];
		cubeMatrix[0][1][0] = face[5];
		cubeMatrix[0][2][0] = face[2];

		cubeMatrix[1][0][0] = face[7];
		cubeMatrix[1][1][0] = face[4];
		cubeMatrix[1][2][0] = face[1];

		cubeMatrix[2][0][0] = face[6];
		cubeMatrix[2][1][0] = face[3];
		cubeMatrix[2][2][0] = face[0];
	}

	private void RotateUi() {
		CubeEntry[] face = GetUpFace();
		foreach (CubeEntry cube in face) {
			// cube.cube.Rotate(new Vector3(90f, 0f, 0f), Space.World);
			StartCoroutine(RotateCoroutine(cube, new Vector3(0f, -90f, 0f), rotationTime));
		}

		// adjust main matrix values to correspond to new cube
		cubeMatrix[0][0][0] = face[6];
		cubeMatrix[0][0][1] = face[3];
		cubeMatrix[0][0][2] = face[0];

		cubeMatrix[0][1][0] = face[7];
		cubeMatrix[0][1][1] = face[4];
		cubeMatrix[0][1][2] = face[1];

		cubeMatrix[0][2][0] = face[8];
		cubeMatrix[0][2][1] = face[5];
		cubeMatrix[0][2][2] = face[2];

	}

	private void RotateBi() {
		CubeEntry[] face = GetBackFace();
		foreach (CubeEntry cube in face) {
			StartCoroutine(RotateCoroutine(cube, new Vector3(0f, 0f, 90f), rotationTime));
		}

		// adjust main matrix values to correspond to new cube
		cubeMatrix[0][2][2] = face[2];
		cubeMatrix[0][2][1] = face[5];
		cubeMatrix[0][2][0] = face[8];

		cubeMatrix[1][2][2] = face[1];
		cubeMatrix[1][2][1] = face[4];
		cubeMatrix[1][2][0] = face[7];

		cubeMatrix[2][2][2] = face[0];
		cubeMatrix[2][2][1] = face[3];
		cubeMatrix[2][2][0] = face[6];
	}

	private void RotateDi() {
		CubeEntry[] face = GetDownFace();
		foreach (CubeEntry cube in face) {
			StartCoroutine(RotateCoroutine(cube, new Vector3(0f, 90f, 0f), rotationTime));
		}

		// adjust main matrix values to correspond to new cube
		cubeMatrix[2][0][0] = face[2];
		cubeMatrix[2][0][1] = face[5];
		cubeMatrix[2][0][2] = face[8];

		cubeMatrix[2][1][0] = face[1];
		cubeMatrix[2][1][1] = face[4];
		cubeMatrix[2][1][2] = face[7];

		cubeMatrix[2][2][0] = face[0];
		cubeMatrix[2][2][1] = face[3];
		cubeMatrix[2][2][2] = face[6];
	}

	private void RotateF2() {
		CubeEntry[] face = GetFrontFace();
		foreach (CubeEntry cube in face) {
			//cube.cube.Rotate(new Vector3(0f, 0f, 90f), Space.World);
			StartCoroutine(RotateCoroutine(cube, new Vector3(0f, 0f, 180f), rotationTime));
		}

		// adjust main matrix values to correspond to new cube
		// tbw
		cubeMatrix[0][0][0] = face[8];
		cubeMatrix[0][0][1] = face[7];
		cubeMatrix[0][0][2] = face[6];

		cubeMatrix[1][0][0] = face[5];
		cubeMatrix[1][0][1] = face[4];
		cubeMatrix[1][0][2] = face[3];

		cubeMatrix[2][0][0] = face[2];
		cubeMatrix[2][0][1] = face[1];
		cubeMatrix[2][0][2] = face[0];

	}

	private void RotateR2() {
		CubeEntry[] face = GetRightFace();
		foreach (CubeEntry cube in face) {
			//cube.cube.Rotate(new Vector3(-90f, 0f, 0f), Space.World);
			StartCoroutine(RotateCoroutine(cube, new Vector3(180f, 0f, 0f), rotationTime));
		}
		// adjust main matrix values to correspond to new cube
		cubeMatrix[0][0][2] = face[8];
		cubeMatrix[0][1][2] = face[7];
		cubeMatrix[0][2][2] = face[6];

		cubeMatrix[1][0][2] = face[5];
		cubeMatrix[1][1][2] = face[4];
		cubeMatrix[1][2][2] = face[3];

		cubeMatrix[2][0][2] = face[2];
		cubeMatrix[2][1][2] = face[1];
		cubeMatrix[2][2][2] = face[0];
	}

	private void RotateL2() {
		CubeEntry[] face = GetLeftFace();
		foreach (CubeEntry cube in face) {
			// cube.cube.Rotate(new Vector3(90f, 0f, 0f), Space.World);
			StartCoroutine(RotateCoroutine(cube, new Vector3(180f, 0f, 0f), rotationTime));
		}

		// adjust main matrix values to correspond to new cube
		cubeMatrix[0][0][0] = face[6];
		cubeMatrix[0][1][0] = face[7];
		cubeMatrix[0][2][0] = face[8];

		cubeMatrix[1][0][0] = face[3];
		cubeMatrix[1][1][0] = face[4];
		cubeMatrix[1][2][0] = face[5];

		cubeMatrix[2][0][0] = face[0];
		cubeMatrix[2][1][0] = face[1];
		cubeMatrix[2][2][0] = face[2];
	}

	private void RotateU2() {
		CubeEntry[] face = GetUpFace();
		foreach (CubeEntry cube in face) {
			// cube.cube.Rotate(new Vector3(90f, 0f, 0f), Space.World);
			StartCoroutine(RotateCoroutine(cube, new Vector3(0f, 180f, 0f), rotationTime));
		}

		// adjust main matrix values to correspond to new cube
		cubeMatrix[0][0][0] = face[8];
		cubeMatrix[0][0][1] = face[7];
		cubeMatrix[0][0][2] = face[6];

		cubeMatrix[0][1][0] = face[5];
		cubeMatrix[0][1][1] = face[4];
		cubeMatrix[0][1][2] = face[3];

		cubeMatrix[0][2][0] = face[2];
		cubeMatrix[0][2][1] = face[1];
		cubeMatrix[0][2][2] = face[0];

	}

	private void RotateB2() {
		CubeEntry[] face = GetBackFace();
		foreach (CubeEntry cube in face) {
			StartCoroutine(RotateCoroutine(cube, new Vector3(0f, 0f, 180f), rotationTime));
		}

		// adjust main matrix values to correspond to new cube
		cubeMatrix[0][2][2] = face[8];
		cubeMatrix[0][2][1] = face[7];
		cubeMatrix[0][2][0] = face[6];

		cubeMatrix[1][2][2] = face[5];
		cubeMatrix[1][2][1] = face[4];
		cubeMatrix[1][2][0] = face[3];

		cubeMatrix[2][2][2] = face[2];
		cubeMatrix[2][2][1] = face[1];
		cubeMatrix[2][2][0] = face[0];
	}

	private void RotateD2() {
		CubeEntry[] face = GetDownFace();
		foreach (CubeEntry cube in face) {
			StartCoroutine(RotateCoroutine(cube, new Vector3(0f, 180f, 0f), rotationTime));
		}

		// adjust main matrix values to correspond to new cube
		cubeMatrix[2][0][0] = face[8];
		cubeMatrix[2][0][1] = face[7];
		cubeMatrix[2][0][2] = face[6];

		cubeMatrix[2][1][0] = face[5];
		cubeMatrix[2][1][1] = face[4];
		cubeMatrix[2][1][2] = face[3];

		cubeMatrix[2][2][0] = face[2];
		cubeMatrix[2][2][1] = face[1];
		cubeMatrix[2][2][2] = face[0];
	}

	private void RotateM() {
		CubeEntry[] face = GetMiddleFace();
		foreach (CubeEntry cube in face) {
			StartCoroutine(RotateCoroutine(cube, new Vector3(90f, 0f, 0f), rotationTime));
		}

		// adjust main matrix values to correspond to new cube
		cubeMatrix[0][0][1] = face[2];
		cubeMatrix[0][1][1] = face[4];
		cubeMatrix[0][2][1] = face[7];

		cubeMatrix[1][0][1] = face[1];
		cubeMatrix[1][2][1] = face[6];

		cubeMatrix[2][0][1] = face[0];
		cubeMatrix[2][1][1] = face[3];
		cubeMatrix[2][2][1] = face[5];
	}

	private void RotateM2() {
		CubeEntry[] face = GetMiddleFace();
		foreach (CubeEntry cube in face) {
			StartCoroutine(RotateCoroutine(cube, new Vector3(180f, 0f, 0f), rotationTime));
		}

		// adjust main matrix values to correspond to new cube
		cubeMatrix[0][0][1] = face[7];
		cubeMatrix[0][1][1] = face[6];
		cubeMatrix[0][2][1] = face[5];

		cubeMatrix[1][0][1] = face[4];
		cubeMatrix[1][2][1] = face[3];

		cubeMatrix[2][0][1] = face[2];
		cubeMatrix[2][1][1] = face[1];
		cubeMatrix[2][2][1] = face[0];
	}


	private void RotateMi() {
		CubeEntry[] face = GetMiddleFace();
		foreach (CubeEntry cube in face) {
			StartCoroutine(RotateCoroutine(cube, new Vector3(-90f, 0f, 0f), rotationTime));
		}

		// adjust main matrix values to correspond to new cube
		cubeMatrix[0][0][1] = face[5];
		cubeMatrix[0][1][1] = face[3];
		cubeMatrix[0][2][1] = face[0];

		cubeMatrix[1][0][1] = face[6];
		cubeMatrix[1][2][1] = face[1];

		cubeMatrix[2][0][1] = face[7];
		cubeMatrix[2][1][1] = face[4];
		cubeMatrix[2][2][1] = face[2];
	}

	public void DoMove(string move) {
		switch (move) {
			case "R":
			RotateR();
			solver.R();
			break;

			case "F":
			RotateF();
			solver.F();
			break;

			case "L":
			RotateL();
			solver.L();
			break;

			case "U":
			RotateU();
			solver.U();
			break;

			case "D":
			RotateD();
			solver.D();
			break;

			case "B":
			RotateB();
			solver.B();
			break;

			case "R'":
			RotateRi();
			solver.Ri();
			break;

			case "L'":
			RotateLi();
			solver.Li();
			break;

			case "U'":
			RotateUi();
			solver.Ui();
			break;

			case "B'":
			RotateBi();
			solver.Bi();
			break;

			case "D'":
			RotateDi();
			solver.Di();
			break;

			case "F'":
			RotateFi();
			solver.Fi();
			break;

			case "R2":
			RotateR2();
			solver.R2();
			break;

			case "F2":
			RotateF2();
			solver.F2();
			break;

			case "L2":
			RotateL2();
			solver.L2();
			break;

			case "U2":
			RotateU2();
			solver.U2();
			break;

			case "D2":
			RotateD2();
			solver.D2();
			break;

			case "B2":
			RotateB2();
			solver.B2();
			break;

			case "M":
			RotateM();
			solver.M();
			break;

			case "M'":
			RotateMi();
			solver.Mi();
			break;

			case "M2":
			RotateM2();
			solver.M2();
			break;
		}
		if (IsSolved()) {
			Debug.Log("YOU SOLVED IT WTF");
			winParticles.Play();
			
			
		}
		// solver.PrintCurrentCube();
		//Debug.Log(IsSolved());
		Debug.Log("solver says: "+solver.IsSolved());
	}

	public bool IsSolved() {
		Dictionary<CubeName, PositionTriplet> solverMatrix = GetMatrixPositions();

		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				for (int k = 0; k < 3; k++) {
					if (i != 1 || j != 1 || k != 1) {
						CubeName name = cubeMatrix[i][j][k].name;
						PositionTriplet triplet = solverMatrix[name];
						if (!(triplet.x == i && triplet.y == j && triplet.z == k)) {
							return false;
						}
					}
					
				}
			}
		}
		return true;
	}

	public IEnumerator SequenceCoroutine(string sequence) {
		string[] moves = sequence.Split(' ');
		Manager.ToggleButtons();
		
		foreach (string move in moves) {
			DoMove(move);
			//StartCoroutine(Wait());
			yield return new WaitForSeconds(sequenceDelay);
		}
		Debug.Log("solve now");
		// enable buttons here
		Manager.ToggleButtons();
		solver.PrintCurrentCube();
	}

	public void RotateSequence(string seq) {
		StartCoroutine(SequenceCoroutine(seq));
	}

	// Update is called once per frame
	void Update() {

	}
}



enum Face {
	F, B, U, D, R, L
}
