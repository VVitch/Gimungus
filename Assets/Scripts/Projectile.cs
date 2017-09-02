using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	public Vector3 velocity;
	public float speed;
	void Awake(){

	}
	virtual public void SetVelocity(Vector3 newVelocity){
		velocity = newVelocity;
	}
	public virtual void Update(){
		transform.Translate (velocity * Time.deltaTime);
	}
	public void SetSpeed(float s){
		speed = s;
	}
}
