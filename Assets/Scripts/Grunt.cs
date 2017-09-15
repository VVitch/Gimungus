using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : UnitController {
	// Use this for initialization
	void Start () {
		base.Start ();
	}

	// Update is called once per frame
	void Update () {
		base.Update ();
		if (!unit.dead) {
			unit.weapon.Aim (playerUnit.transform.position);
			if (Vector3.Distance (unit.transform.position, playerUnit.transform.position) < strikingDistance) {
				unit.AttackWithWeapon ();
				unit.Stop ();
			}else if(playerSeen){
				unit.MoveToward (playerUnit.transform.position);
			}
		}
	}
		


}
