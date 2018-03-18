using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrambleGenerator : MonoBehaviour {

	public static string Generate(int length) {
		string[] moves = {
			"R", "U", "F", "D", "B", "L",
			"R2", "U2", "F2", "D2", "B2", "L2",
			"R'", "U'", "F'", "D'", "B'", "L'"
		};

		System.Random random = new System.Random();
		string scramble = "";
		string lastMove = "";
		for (int i = 0; i < length; i++) {
			int index = random.Next(0, 18);
			string move = moves[index];
			if (i > 0) {

				// could implement this better but im lazy
				while (lastMove == moves[index] || lastMove.Contains(moves[index]) || moves[index].Contains(lastMove)) {
					index = random.Next(0, 18);
					move = moves[index];
				}
			}
			scramble += moves[index];
			if (i < length - 1) {
				scramble += " ";
			}
			lastMove = move;
		}
		Debug.Log(scramble);
		return scramble;
	}
}
