﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile {
	public Quaternion myRotation;
	Unit playerUnit;
	Rigidbody2D rb;
	int collisionTracker;

	public void Awake(){
		playerUnit = GameObject.Find ("PlayerInputController").GetComponent<PlayerInputController> ().playerUnit;
		StartCoroutine (Fire());
		rb = GetComponent<Rigidbody2D>();
	}
	void OnTriggerEnter2D(Collider2D other) 
	{ 

		if (other.gameObject.tag == "damage source") {
			transform.Rotate(0,0,Mathf.PI);
			rb.velocity = -rb.velocity;
			collisionTracker = collisionTracker + 1;
			Debug.Log (collisionTracker);
			if (collisionTracker > 10) {
				//rb.velocity = new Vector2 (Random.Range (-1, 2), Random.Range (-1, 2)) * speed;
				Destroy (this.gameObject);
			}
		}
	}

	IEnumerator Fire(){
		BoxCollider2D[] myColliders = gameObject.GetComponents<BoxCollider2D>();
		foreach(BoxCollider2D bc in myColliders) bc.enabled = false;
		speed = speed / 2;
		yield return new WaitForSeconds (.1f);
		speed = speed * 2;
		foreach(BoxCollider2D bc in myColliders) bc.enabled = true;
	}

	override public void SetVelocity(Vector3 vel){
		rb.velocity = vel * speed;
	}

	override public void Update(){
		transform.rotation = Quaternion.identity;
		//transform.Translate (velocity * Time.deltaTime);
		transform.rotation = myRotation;
		if (Vector3.Distance (playerUnit.transform.position, transform.position) > 100) {
			Destroy (this.gameObject);
		}
	}
}
