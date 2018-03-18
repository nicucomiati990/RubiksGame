using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

	public GameObject helper;
	public CubeRotation cubeRotation;
	public Text scrambleText;

	public static bool isSolving;

	public void ToggleHelper() {
		if (helper.activeSelf == true) {
			helper.SetActive(false);
		} else {
			helper.SetActive(true);
		}
	}

	public static void ToggleButtons() {
		GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");
		bool shouldActivate = true;
		if (buttons[0].GetComponent<Button>().IsInteractable() == true) {
			shouldActivate = false;
		}
		// activate or deactivate
		foreach (GameObject button in buttons) {
			button.GetComponent<Button>().interactable = shouldActivate;
		}
	}

	public void GenerateScramble() {
		string scramble = ScrambleGenerator.Generate(20);
		scrambleText.text = "Scramble: " + scramble;
		

		cubeRotation.RotateSequence(scramble);
		//Debug.Log("solve now");
		isSolving = true;
	}

}
