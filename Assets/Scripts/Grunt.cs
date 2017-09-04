using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : MonoBehaviour {
	public Unit unit;
	public int strikingDistance;
	public int seeingDistance;
	Unit playerUnit;
	bool spottedPlayer = false;
	// Use this for initialization
	void Start () {
		playerUnit = GameObject.Find ("PlayerInputController").GetComponent<PlayerInputController> ().playerUnit;
	}

	// Update is called once per frame
	void Update () {
		if (!unit.dead) {
			unit.weapon.Aim (playerUnit.transform.position);
			if(Vector3.Distance(playerUnit.transform.position, unit.transform.position) < seeingDistance){
				spottedPlayer = true;
			}


			if (Vector3.Distance (unit.transform.position, playerUnit.transform.position) < strikingDistance) {
				unit.AttackWithWeapon ();
			} else if(spottedPlayer){
				unit.MoveToward (playerUnit.transform.position);
			}
		}
	}
		


}
