using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : MonoBehaviour {
	public Unit unit;
	Unit playerUnit;
	// Use this for initialization
	void Start () {
		playerUnit = GameObject.Find ("PlayerInputController").GetComponent<PlayerInputController> ().playerUnit;
	}

	// Update is called once per frame
	void Update () {
		unit.MoveToward (playerUnit.transform.position);
		unit.TouchAttack (playerUnit);
	}
		


}
