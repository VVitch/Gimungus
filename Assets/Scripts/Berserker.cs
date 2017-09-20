using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserker : UnitController {
	bool charging = false;
	public float chargeRange;
	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!unit.dead) {
			unit.AimWeapon (playerUnit.transform.position);
			if (!playerSeen) {
				CheckForPlayer ();
			} else if (!charging) {
				Charge ();
			} else {
				if (Vector3.Distance (unit.transform.position, playerUnit.transform.position) < strikingDistance) {
					unit.AttackWithWeapon ();
				}
				if (Vector3.Distance (unit.transform.position, playerUnit.transform.position) > chargeRange) {
					playerSeen = false;
					charging = false;
				}
			}
		}

	}

	void Charge(){
		unit.MoveToward (playerUnit.transform.position);
		unit.SetVelocity (unit.GetVelocity ());
		charging = true;
	}
}
