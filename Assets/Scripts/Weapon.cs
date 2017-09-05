using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
	public BoxCollider2D bc;
	public float speed, staminaCost;
	protected bool rested;
	protected float swingTracker = 0;
	protected int direction = 1;
	protected float lethalbuffer;
	public float aimSpeed;
	protected Vector3 myTarget;
	// Use this for initialization
	protected void Start () {
		rested = true;
		if(bc!=null){bc.enabled = false;}
		if(aimSpeed == 0){
			aimSpeed = 5;
		}
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
			if (swingTracker > 90) {
				if(bc!=null){bc.enabled = true;}
			}
			if (swingTracker > 270) {
				if(bc!=null){bc.enabled = false;}
			}

			float swingTemp = Time.deltaTime * speed;
			swingTemp *= (.1f + Mathf.Pow(Mathf.Abs(Mathf.Sin (0.5f * (swingTracker*Mathf.PI / 180f))),1));

			transform.RotateAround (transform.position, new Vector3 (0, 0, 1), swingTemp*direction);
			swingTracker += swingTemp;
			if (Mathf.Abs (swingTracker) > 360) {
				rested = true;
				swingTracker = 0;
				direction = -direction;
			}
		} else {
			if(bc!=null){bc.enabled = false;}
		}
	}

	virtual public void Aim(Vector3 target){
		myTarget = target;
		if (IsRested()) {
			Quaternion nRotation = Quaternion.LookRotation (Vector3.forward, target - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, nRotation, Time.deltaTime * aimSpeed);
		}
	}

	virtual public void Strike(){

	}
}
	