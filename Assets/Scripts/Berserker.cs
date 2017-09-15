using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserker : UnitController {
	bool charging = false;
	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!playerSeen) {
			CheckForPlayer ();
		} else if(!charging) {

		}

	}
}
