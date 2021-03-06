﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputController : MonoBehaviour {
	public Unit playerUnit;
	public Camera mainCamera;
	public bool aim_enabled = true;
	bool dashLock, disabled, canAttack = false;

	public GameObject staminaBar;

	public List<GameObject> healthBar;
	int hpIndex;

	// Use this for initialization
	void Start () {
		hpIndex = 2;
	}
	
	// Update is called once per frame
	void Update () {
		//Dash
		if (!disabled) {
			if (Input.GetAxisRaw ("Dash") != 0 && !dashLock) {
				playerUnit.Dash ();
				dashLock = true;
			} else if (Input.GetAxisRaw ("Dash") == 0) {
				dashLock = false;
			}

			//Movement
			float hMovement = Input.GetAxis ("Horizontal");
			float vMovement = Input.GetAxis ("Vertical");
			playerUnit.Move (hMovement, vMovement);
			mainCamera.transform.position = new Vector3 (playerUnit.transform.position.x, playerUnit.transform.position.y, mainCamera.transform.position.z);
			
			//Weapon aiming
			if (Input.GetAxisRaw ("Attack") != 0 && canAttack) {
				Attack ();
				canAttack = false;
			} else if (Input.GetAxisRaw ("Attack") == 0) {
				canAttack = true;
			}

			//action
			if (Input.GetAxisRaw ("Action") != 0) {
				TalkToNPC ();
				Debug.Log ("wow");
			}

			//playerUnit.weapon.Aim (Camera.main.ScreenToWorldPoint (Input.mousePosition));
			playerUnit.AimWeapon (Camera.main.ScreenToWorldPoint (Input.mousePosition));
			staminaBar.transform.localScale = new Vector3 (playerUnit.stamina / 100f, 1, 1); 

			if (hpIndex > playerUnit.health - 1) {
				healthBar [hpIndex].transform.position = new Vector3 (-1000, -1000, -1000);
				hpIndex--;
			}

			if (playerUnit.dead) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
			}
		}
	}
		
	void Attack(){
		playerUnit.AttackWithWeapon();
	}

	void TalkToNPC(){
		if (playerUnit.GetNPC () != null) {
			playerUnit.GetNPC ().ActivateDialogue ();
			Debug.Log ("yes npc");
		} else {
			Debug.Log ("no npc");
		}
	}

	public void Disable(){
		disabled = true;
		playerUnit.SetVelocity (new Vector2 (0, 0));
	}
	public void Enable(){
		canAttack = false;
		disabled = false;
	}
}