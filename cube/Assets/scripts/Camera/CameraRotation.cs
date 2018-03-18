using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

	float x;
	float y;
	private Vector3 rotateValue;
	public float idleSpeed;
	public float sensitivity;
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)) {
			y = Input.GetAxis("Mouse X");
			x = Input.GetAxis("Mouse Y");
			rotateValue = new Vector3(-x, -y, 0f) * sensitivity;
			transform.eulerAngles -= rotateValue;
		}
		transform.Rotate(Vector3.up * Time.deltaTime * idleSpeed);
		
	}
}
