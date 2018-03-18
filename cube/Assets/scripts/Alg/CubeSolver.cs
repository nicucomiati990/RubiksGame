using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSolver : MonoBehaviour {

	// THE 6 FACES OF THE CUBE
	string[,] white = { 
		{ "w", "w", "w" },
		{ "w", "w", "w" },
		{ "w", "w", "w" }
	};

	string[,] red = {
		{ "r", "r", "r" },
		{ "r", "r", "r" },
		{ "r", "r", "r" },
	};

	string[,] yellow = {
		{ "y", "y", "y" },
		{ "y", "y", "y" },
		{ "y", "y", "y" },
	};

	string[,] blue = {
		{ "b", "b", "b" },
		{ "b", "b", "b" },
		{ "b", "b", "b" },
	};

	string[,] green = {
		{ "g", "g", "g" },
		{ "g", "g", "g" },
		{ "g", "g", "g" },
	};

	string[,] orange = {
		{ "o", "o", "o" },
		{ "o", "o", "o" },
		{ "o", "o", "o" },
	};


	public void R() {
		// keep one strip to add later
		string[] whiteStrip = { white[0, 2], white[1, 2], white[2, 2] };
		white[0, 2] = orange[2, 0];
		white[1, 2] = orange[1, 0];
		white[2, 2] = orange[0, 0];

		orange[0, 0] = yellow[2, 2];
		orange[1, 0] = yellow[1, 2];
		orange[2, 0] = yellow[0, 2];

		yellow[0, 2] = red[0, 2];
		yellow[1, 2] = red[1, 2];
		yellow[2, 2] = red[2, 2];

		red[0, 2] = whiteStrip[0];
		red[1, 2] = whiteStrip[1];
		red[2, 2] = whiteStrip[2];

		// rotate stickers of face
		string temp = green[0, 0];
		// corners
		green[0, 0] = green[2, 0];
		green[2, 0] = green[2, 2];
		green[2, 2] = green[0, 2];
		green[0, 2] = temp;
		// edges
		temp = green[0, 1];
		green[0, 1] = green[1, 0];
		green[1, 0] = green[2, 1];
		green[2, 1] = green[1, 2];
		green[1, 2] = temp;

	}

	public void F() {
		// keep one strip to add later
		string[] whiteStrip = { white[0, 0], white[0, 1], white[0, 2] };
		white[0, 0] = green[2, 0];
		white[0, 1] = green[1, 0];
		white[0, 2] = green[0, 0];

		green[0, 0] = yellow[2, 0];
		green[1, 0] = yellow[2, 1];
		green[2, 0] = yellow[2, 2];

		yellow[2, 0] = blue[2, 2];
		yellow[2, 1] = blue[1, 2];
		yellow[2, 2] = blue[0, 2];

		blue[0, 2] = whiteStrip[0];
		blue[1, 2] = whiteStrip[1];
		blue[2, 2] = whiteStrip[2];

		// rotate stickers of face
		string temp = red[0, 0];
		// corners
		red[0, 0] = red[2, 0];
		red[2, 0] = red[2, 2];
		red[2, 2] = red[0, 2];
		red[0, 2] = temp;
		// edges
		temp = red[0, 1];
		red[0, 1] = red[1, 0];
		red[1, 0] = red[2, 1];
		red[2, 1] = red[1, 2];
		red[1, 2] = temp;

	}

	public void U() {
		// keep one strip to add later
		string[] redStrip = { red[0, 0], red[0, 1], red[0, 2] };

		red[0, 0] = green[0, 0];
		red[0, 1] = green[0, 1];
		red[0, 2] = green[0, 2];

		green[0, 0] = orange[0, 0];
		green[0, 1] = orange[0, 1];
		green[0, 2] = orange[0, 2];

		orange[0, 0] = blue[0, 0];
		orange[0, 1] = blue[0, 1];
		orange[0, 2] = blue[0, 2];

		blue[0, 0] = redStrip[0];
		blue[0, 1] = redStrip[1];
		blue[0, 2] = redStrip[2];

		// rotate stickers of yellow face
		string temp = yellow[0, 0];
		// corners
		yellow[0, 0] = yellow[2, 0];
		yellow[2, 0] = yellow[2, 2];
		yellow[2, 2] = yellow[0, 2];
		yellow[0, 2] = temp;
		// edges
		temp = yellow[0, 1];
		yellow[0, 1] = yellow[1, 0];
		yellow[1, 0] = yellow[2, 1];
		yellow[2, 1] = yellow[1, 2];
		yellow[1, 2] = temp;

	}

	public void L() {
		// keep one strip to add later
		string[] whiteStrip = { white[0, 0], white[1, 0], white[2, 0] };

		white[0, 0] = red[0, 0];
		white[1, 0] = red[1, 0];
		white[2, 0] = red[2, 0];

		red[0, 0] = yellow[0, 0];
		red[1, 0] = yellow[1, 0];
		red[2, 0] = yellow[2, 0];

		yellow[0, 0] = orange[2, 2];
		yellow[1, 0] = orange[1, 2];
		yellow[2, 0] = orange[0, 2];

		orange[0, 2] = whiteStrip[2];
		orange[1, 2] = whiteStrip[1];
		orange[2, 2] = whiteStrip[0];

		// rotate stickers of yellow face
		string temp = blue[0, 0];
		// corners
		blue[0, 0] = blue[2, 0];
		blue[2, 0] = blue[2, 2];
		blue[2, 2] = blue[0, 2];
		blue[0, 2] = temp;
		// edges
		temp = blue[0, 1];
		blue[0, 1] = blue[1, 0];
		blue[1, 0] = blue[2, 1];
		blue[2, 1] = blue[1, 2];
		blue[1, 2] = temp;

	}

	public void D() {
		// keep one strip to add later
		string[] redStrip = { red[2, 0], red[2, 1], red[2, 2] };

		red[2, 0] = blue[2, 0];
		red[2, 1] = blue[2, 1];
		red[2, 2] = blue[2, 2];

		blue[2, 0] = orange[2, 0];
		blue[2, 1] = orange[2, 1];
		blue[2, 2] = orange[2, 2];

		orange[2, 0] = green[2, 0];
		orange[2, 1] = green[2, 1];
		orange[2, 2] = green[2, 2];

		green[2, 0] = redStrip[0];
		green[2, 1] = redStrip[1];
		green[2, 2] = redStrip[2];

		// rotate stickers of yellow face
		string temp = white[0, 0];
		// corners
		white[0, 0] = white[2, 0];
		white[2, 0] = white[2, 2];
		white[2, 2] = white[0, 2];
		white[0, 2] = temp;
		// edges
		temp = white[0, 1];
		white[0, 1] = white[1, 0];
		white[1, 0] = white[2, 1];
		white[2, 1] = white[1, 2];
		white[1, 2] = temp;

	}

	public void B() {
		// keep one strip to add later
		string[] whiteStrip = { white[2, 0], white[2, 1], white[2, 2] };

		white[2, 2] = blue[2, 0];
		white[2, 1] = blue[1, 0];
		white[2, 0] = blue[0, 0];

		blue[0, 0] = yellow[0, 2];
		blue[1, 0] = yellow[0, 1];
		blue[2, 0] = yellow[0, 0];

		yellow[0, 0] = green[0, 2];
		yellow[0, 1] = green[1, 2];
		yellow[0, 2] = green[2, 2];

		green[0, 2] = whiteStrip[2];
		green[1, 2] = whiteStrip[1];
		green[2, 2] = whiteStrip[0];


		// rotate stickers of orange face
		string temp = orange[0, 0];
		// corners
		orange[0, 0] = orange[2, 0];
		orange[2, 0] = orange[2, 2];
		orange[2, 2] = orange[0, 2];
		orange[0, 2] = temp;
		// edges
		temp = orange[0, 1];
		orange[0, 1] = orange[1, 0];
		orange[1, 0] = orange[2, 1];
		orange[2, 1] = orange[1, 2];
		orange[1, 2] = temp;

	}

	public void M() {
		// do M move
		string[] whiteStrip = { white[0, 1], white[1, 1], white[2, 1] };

		white[0, 1] = red[0, 1];
		white[1, 1] = red[1, 1];
		white[2, 1] = red[2, 1];

		red[0, 1] = yellow[0, 1];
		red[1, 1] = yellow[1, 1];
		red[2, 1] = yellow[2, 1];

		yellow[0, 1] = orange[2, 1];
		yellow[1, 1] = orange[1, 1];
		yellow[2, 1] = orange[0, 1];

		orange[0, 1] = whiteStrip[2];
		orange[1, 1] = whiteStrip[1];
		orange[2, 1] = whiteStrip[0];
	}

	public void Mi() {
		M();M();M();
	}

	public void M2() {
		M();M();
	}

	public void Ri() {
		R();
		R();
		R();
	}

	public void Fi() {
		F();
		F();
		F();
	}

	public void Li() {
		L();
		L();
		L();
	}

	public void Ui() {
		U();
		U();
		U();
	}

	public void Di() {
		D();
		D();
		D();
	}

	public void Bi() {
		B();
		B();
		B();
	}


	public void R2() {
		R();
		R();
	}

	public void F2() {
		F();
		F();
	}

	public void L2() {
		L();
		L();
	}

	public void U2() {
		U();
		U();
	}

	public void D2() {
		D();
		D();
	}

	public void B2() {
		B();
		B();
	}



	public void PrintCurrentCube() {
		Debug.Log("=====CUBE=====");
		Debug.Log("RED");
		for (int i = 0; i < 3; i++) {
			Debug.Log(red[i, 0] + " " + red[i, 1] + " " + red[i, 2]);
		}

		Debug.Log("ORANGE");
		for (int i = 0; i < 3; i++) {
			Debug.Log(orange[i, 0] + " " + orange[i, 1] + " " + orange[i, 2]);
		}

		Debug.Log("BLUE");
		for (int i = 0; i < 3; i++) {
			Debug.Log(blue[i, 0] + " " + blue[i, 1] + " " + blue[i, 2]);
		}

		Debug.Log("GREEN");
		for (int i = 0; i < 3; i++) {
			Debug.Log(green[i, 0] + " " + green[i, 1] + " " + green[i, 2]);
		}

		Debug.Log("WHITE");
		for (int i = 0; i < 3; i++) {
			Debug.Log(white[i, 0] + " " + white[i, 1] + " " + white[i, 2]);
		}

		Debug.Log("YELLOW");
		for (int i = 0; i < 3; i++) {
			Debug.Log(yellow[i, 0] + " " + yellow[i, 1] + " " + yellow[i, 2]);
		}

	}

	private void Sequencer(string moves) {
		string[] moveArr = moves.Split(' ');
		foreach (string move in moveArr) {
			switch (move) {
				case "R":
				R();
				break;

				case "F":
				F();
				break;

				case "L":
				L();
				break;

				case "U":
				U();
				break;

				case "D":
				D();
				break;

				case "B":
				B();
				break;

				case "R'":
				Ri();
				break;

				case "L'":
				Li();
				break;

				case "U'":
				Ui();
				break;

				case "B'":
				Bi();
				break;

				case "D'":
				Di();
				break;

				case "F'":
				Fi();
				break;

				case "R2":
				R2();
				break;

				case "F2":
				F2();
				break;

				case "L2":
				L2();
				break;

				case "U2":
				U2();
				break;

				case "D2":
				D2();
				break;

				case "B2":
				B2();
				break;

				case "M":
				M();
				break;

				case "M'":
				Mi();
				break;

				case "M2":
				M2();
				break;
			}
		}
		
	}

	public bool IsSolved() {
		foreach (string col in red) {
			if (col != "r") {
				return false;
			}
		}

		foreach (string col in orange) {
			if (col != "o") {
				return false;
			}
		}

		foreach (string col in blue) {
			if (col != "b") {
				return false;
			}
		}

		foreach (string col in green) {
			if (col != "g") {
				return false;
			}
		}

		foreach (string col in yellow) {
			if (col != "y") {
				return false;
			}
		}

		foreach (string col in white) {
			if (col != "w") {
				return false;
			}
		}

		return true;
	}

	private int IsSuperflip() {
		int numOfD = 0;
		bool found = false;

		for (int i = 0; i < 4; i++) {
			if (red[2,1] == "w" && green[2,1] == "w" &&
				orange[2,1] == "w" && blue[2,1] == "w" && 
				white[0,1] == "r" && white[1,2] == "g" && 
				white[1,0] == "b" && white[2,1] == "o") {

				found = true;
			}
			D();
			if (!found) {
				numOfD += 1;
			}
		}
		if (numOfD == 4) {
			return -1;
		}
		return numOfD;
	}

	private int NumOfEdgesSolved() {
		int num = 0;
		if (red[2, 1] == "r" && white[0, 1] == "w")
			num++;
		if (green[2, 1] == "g" && white[1, 2] == "w")
			num++;
		if (orange[2, 1] == "o" && white[2, 1] == "w")
			num++;
		if (blue[2, 1] == "b" && white[1, 0] == "w")
			num++;
		return num;
	}

	private string CheckTrivialCross() {
		// checks if cross is solved but misaligned
		string sol = "";
		// check superflip
		if (IsSuperflip() >= 0) {
			for (int i = 0; i < IsSuperflip(); i++) {
				D();
				sol += " D";
			}
			Sequencer(" R F L B R D");
			sol += " R F L B R D";
			return sol;
		}

		// else
		int maxNum = 0;
		for (int i = 0; i < 4; i++) {
			int num = NumOfEdgesSolved();
		}
		return sol;
	}

	private string SolveCross() {
		string solution = CheckTrivialCross();

		return solution;
	}

	public string GetSolve() {
		string solve = "";

		return solve;
	}


}
