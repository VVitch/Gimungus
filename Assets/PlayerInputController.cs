using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour {
	public Unit playerUnit;
	public Camera mainCamera;
	float speed = 0.1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		float hMovement = Input.GetAxis("Horizontal") * speed;
		float vMovement = Input.GetAxis ("Vertical") * speed;

		playerUnit.transform.Translate (hMovement, vMovement, 0);
		mainCamera.transform.Translate (hMovement, vMovement, 0);
	}
}
