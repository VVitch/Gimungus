using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
	public bool dead = false;
	public Weapon weapon;
	public float speed, dashMultiplier, stamina, staminaRecharge, dashCost, attackCost;
	public int health;
	bool invincible, dashLocked = false;
	bool canTouchAttack = true;

	void Start(){

	}

	public void Move(float x, float y){
		if (!dead) {
			transform.Translate (x * speed * Time.deltaTime, y * speed * Time.deltaTime, 0);
		}
	}

	public void MoveToward(Vector3 position){
		if (!dead) {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, position, step);
		}
	}

	public void MoveAway(Vector3 position){
		if (!dead) {
			Vector3 direction = (transform.position + position) * 2;
			float step = speed * Time.deltaTime;

			transform.position = Vector3.MoveTowards (transform.position, direction, step);
		}
	}

	public bool Dash(){
		if (!dashLocked && stamina > dashCost) {
			stamina -= dashCost;
			dashLocked = true;
			speed = speed * 10;
			StartCoroutine (DashRoutine ());
			return true;
		}
		return false;
	}

	IEnumerator DashRoutine(){
		yield return new WaitForSeconds (.05f);
		speed = speed / 10;
		dashLocked = false;
	}

	void OnTriggerEnter2D(Collider2D other) 
	{ 
		Die ();
	}

	void Die(){
		GetComponent<SpriteRenderer> ().color = Color.red;
		dead = true;
	}

	public void AttackWithWeapon(){
		if (weapon.IsRested () && stamina > attackCost) {
			weapon.StartSwing ();
			stamina -= attackCost;
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
		yield return new WaitForSeconds(.5f);
		invincible = false;
	}

	void Update(){
		if(weapon != null && weapon.IsRested()){
			stamina += staminaRecharge * Time.deltaTime;
		}
		if (stamina > 100) {
			stamina = 100;
		}
	}

	public void TouchAttack(Unit u){
		if (canTouchAttack && !dead && Vector3.Distance(transform.position, u.transform.position) < 1) {
			u.TakeDamage ();
			Debug.Log ("dealt damage");

			StartCoroutine (TouchAttackCooldown ());
		}
	}

	IEnumerator TouchAttackCooldown(){
		canTouchAttack = false;
		yield return new WaitForSeconds(1);
		canTouchAttack = true;

	}
}