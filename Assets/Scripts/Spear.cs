using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Weapon {
	public float timeToStab;
	public float range;
	Vector3 frozenTarget;
	bool hasft,stabComplete = false;


	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	override protected void AttackCheck(){
		if (!rested) {
			if (!hasft) {
				hasft = true;
				frozenTarget = myTarget;
			}
			swingTracker += Time.deltaTime/timeToStab;


		

			if (swingTracker < 1 && !stabComplete) {
				transform.position = Vector3.Lerp (transform.parent.transform.position, range * UBP (frozenTarget, transform.parent.transform.position) + transform.parent.transform.position, swingTracker);
				bc.enabled = true;
			} else if (!stabComplete) {
				swingTracker = 0;
				stabComplete = true;
				bc.enabled = false;
			} else if (swingTracker < 1 && stabComplete) {
				transform.position = Vector3.Lerp (range * UBP (frozenTarget, transform.parent.transform.position) + transform.parent.transform.position, transform.parent.transform.position, swingTracker);
			} else {
				transform.position = transform.parent.transform.position;
				rested = true;
				swingTracker = 0;
				hasft = false;
				stabComplete = false;
			}
				
		} else {
			if(bc!=null){bc.enabled = false;}
		}
}

	Vector3 UBP(Vector3 p1, Vector3 p2){
		return (1f / (p1 - p2).magnitude) * (p1 - p2);
	}
}	


	