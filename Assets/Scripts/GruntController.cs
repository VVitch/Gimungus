using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntController : MonoBehaviour {
	public Unit unit;
	Unit playerUnit;
	bool canAttack;
	// Use this for initialization
	void Start () {
		playerUnit = GameObject.Find ("PlayerInputController").GetComponent<PlayerInputController> ().playerUnit;
		canAttack = true;
	}

	// Update is called once per frame
	void Update () {
		unit.MoveToward (playerUnit.transform.position);
		//check for attack 
		if (Mathf.Abs (unit.transform.position.x - playerUnit.transform.position.x) < 1) {
			if (Mathf.Abs (unit.transform.position.y - playerUnit.transform.position.y) < 1) {
				Attack ();
			}
		}
	}

	IEnumerator AttackCooldown(){
		canAttack = false;
		yield return new WaitForSeconds(1);
		canAttack = true;

	}

	void Attack(){
		if (canAttack && !unit.dead) {
			playerUnit.TakeDamage ();
		}
	}


}
