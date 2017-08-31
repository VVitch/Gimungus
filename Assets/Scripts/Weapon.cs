using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	protected float speed;
	protected bool rested;
	protected float swingTracker = 0;
	protected int direction = 1;
	// Use this for initialization
	protected void Start () {
		rested = true;
	}

	public bool IsRested(){
		return rested;
	}

	public void StartSwing(){
		rested = false;
	}

	// Update is called once per frame
	virtual protected void Update () {
		AttackCheck ();
	}

	virtual protected void AttackCheck(){
		if (!rested) {
			float swingTemp = Time.deltaTime * speed * direction;
			transform.RotateAround (transform.position, new Vector3 (0, 0, 1), swingTemp);
			swingTracker += swingTemp;
			if (Mathf.Abs(swingTracker) > 360) {
				rested = true;
				swingTracker = 0;
				direction = -direction;
			}
		}
	}

	public void Aim(Vector3 target){
		if (IsRested()) {
			transform.rotation = Quaternion.LookRotation (Vector3.forward, target - transform.position);
		}
	}


	virtual public void Strike(){

	}
}