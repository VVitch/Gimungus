using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputController : MonoBehaviour {
	public Unit playerUnit;
	public Camera mainCamera;
	public bool aim_enabled = true;
	bool dashLock = false;

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
		mainCamera.transform.position = new Vector3(playerUnit.transform.position.x, playerUnit.transform.position.y, mainCamera.transform.position.z);
			
		//Weapon aiming
		if (Input.GetAxisRaw ("Attack") != 0) {
			Attack ();
		}

		//playerUnit.weapon.Aim (Camera.main.ScreenToWorldPoint (Input.mousePosition));
		playerUnit.AimWeapon(Camera.main.ScreenToWorldPoint (Input.mousePosition));
		staminaBar.transform.localScale = new Vector3(playerUnit.stamina / 100f, 1, 1); 

		if (hpIndex > playerUnit.health - 1) {
			healthBar [hpIndex].transform.position = new Vector3(-1000,-1000,-1000);
			hpIndex--;
		}

		if(playerUnit.dead){
			  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
		
	void Attack(){
		playerUnit.AttackWithWeapon();
	}
}