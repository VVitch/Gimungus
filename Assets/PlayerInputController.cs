using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour {
	public Unit playerUnit;
	public Camera mainCamera;
	public bool aim_enabled = true;

	float speed = 0.1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Movement
		float hMovement = Input.GetAxis("Horizontal") * speed;
		float vMovement = Input.GetAxis ("Vertical") * speed;
		playerUnit.transform.Translate (hMovement, vMovement, 0);
		mainCamera.transform.Translate (hMovement, vMovement, 0);

		//Weapon aiming
		if (Input.GetAxisRaw ("Jump") != 0) {
			Attack ();
		}
		playerUnit.weapon.Aim (Camera.main.ScreenToWorldPoint (Input.mousePosition));

	}

	void Attack(){
		aim_enabled = false;
		playerUnit.weapon.StartSwing ();
	}
}
