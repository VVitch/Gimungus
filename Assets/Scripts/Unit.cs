﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
	public GameObject blood;


	public bool dead = false;
	public float speed, dashMultiplier, stamina, staminaMax, staminaRecharge, dashCost;
	public int health;
	bool invincible, dashLocked = false;
	bool canTouchAttack = true;
	public Weapon weapon;
	Vector3 colliderPosition;
	Rigidbody2D rb;
		
	void Start(){
		rb = gameObject.GetComponent<Rigidbody2D> ();
		staminaMax = stamina;
	}

	public void Move(float x, float y){
		if (!dead) {
			rb.velocity = new Vector2(x*speed, y*speed);
		}
	}

	public void MoveToward(Vector3 position){
		if (!dead) {
			rb.velocity = UBP(position, transform.position) * speed;
		}
	}

	public void MoveAway(Vector3 position){
		if (!dead) {
			rb.velocity = UBP(transform.position, position) * speed;
		}
	}

	public bool Dash(bool free){
		if (free) {
			StartCoroutine (DashRoutine());
			StartCoroutine(InvisiTimer());
			return true;
		}
		else if (!dashLocked && stamina > dashCost) {
			stamina -= dashCost;
			dashLocked = true;
			StartCoroutine (DashRoutine ());
			return true;
		}
		return false;
	}

	IEnumerator DashRoutine(){
		speed = speed * 10;
		yield return new WaitForSeconds (.08f);
		speed = speed / 10;
		dashLocked = false;
	}

	void OnTriggerEnter2D(Collider2D other) 
	{ 
		if (other.gameObject.tag == "damage source") {
			colliderPosition = other.transform.position;
				
			TakeDamage ();
		} else if (other.gameObject.tag == "weapon") {
			other.transform.parent = this.transform;
		}
	}

	public void Die(){
		rb.velocity = new Vector2 (0, 0);
		GetComponent<SpriteRenderer> ().color = Color.red;
		dead = true;
		BoxCollider2D[] myColliders = gameObject.GetComponents<BoxCollider2D>();
		foreach(BoxCollider2D bc in myColliders) bc.enabled = false;
		if(blood!=null){
			Vector3 u = UBP (colliderPosition, transform.position);
			GameObject b = Instantiate (blood, transform.position, Quaternion.Euler (new Vector3 (Random.Range (0, 360), 90, 0)));
		}

	}

	public void AttackWithWeapon(){
		if (!dead && weapon != null && weapon.IsRested () && stamina > weapon.staminaCost) {
			weapon.StartSwing ();
			stamina -= weapon.staminaCost;
		}

	}
	public void TakeDamage(){
		if (!dead && !invincible) {
			health--;
			if (health < 1) {
				Die ();
			} else {
				StartCoroutine (InvisiTimer ());
			}
		}
	}
	IEnumerator InvisiTimer(){
		invincible = true;
		BoxCollider2D[] myColliders = gameObject.GetComponents<BoxCollider2D>();
		yield return new WaitForSeconds(.005f);
		invincible = false;
	}

	void Update(){
		if(!dead){
			transform.rotation = Quaternion.identity;
			if(weapon != null && weapon.IsRested()){
				stamina += staminaRecharge * Time.deltaTime;
			}
			if (stamina > staminaMax) {
				stamina = staminaMax;
			}
		}
	}
	public void TouchAttack(Unit u){
		if (canTouchAttack && !dead && Vector3.Distance(transform.position, u.transform.position) < 1) {
			u.TakeDamage ();
			StartCoroutine (TouchAttackCooldown ());
		}
	}

	IEnumerator TouchAttackCooldown(){
		canTouchAttack = false;
		yield return new WaitForSeconds(1);
		canTouchAttack = true;

	}

	public void AimWeapon(Vector3 position){
		if (weapon != null && !dead) {
			weapon.Aim (position);
		}
	}

	public void DropWeapon(){
		if (weapon != null) {
			weapon.tag = "Untagged";
			weapon.transform.parent = null;
			weapon = null;
		}
	}

	Vector3 UBP(Vector3 p1, Vector3 p2){
		return (1f / (p1 - p2).magnitude) * (p1 - p2);
	}

}