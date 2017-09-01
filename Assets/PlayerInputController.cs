using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour {
	public Unit playerUnit;
	public Camera mainCamera;
	public bool aim_enabled = true;
	bool dashLock = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Dash
		if (Input.GetAxisRaw ("Dash") != 0 && !dashLock) {
			playerUnit.Dash ();
			dashLock = true;
		} else if(Input.GetAxisRaw("Dash") == 0){
			dashLock = false;
		}

		//Movement
		float hMovement = Input.GetAxis ("Horizontal");
		float vMovement = Input.GetAxis ("Vertical");


		playerUnit.Move (hMovement, vMovement);
		mainCamera.transform.Translate (hMovement * playerUnit.speed * Time.deltaTime, vMovement * playerUnit.speed*Time.deltaTime, 0);

		//Weapon aiming
		if (Input.GetAxisRaw ("Jump") != 0) {
			Attack ();
		}
		playerUnit.weapon.Aim (Camera.main.ScreenToWorldPoint (Input.mousePosition));

	}


	void Attack(){
		playerUnit.weapon.StartSwing ();
	}
}
