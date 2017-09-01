using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
	bool dead = false;
	public Weapon weapon;
	public float speed;
	public float dashMultiplier;
	public float stamina;
	public int health;


	bool dashLocked;
	void Start(){
		dashLocked = false;
	}

	public void Move(float x, float y){
		if (!dead) {
			Debug.Log (x * speed);
			transform.Translate (x * speed * Time.deltaTime, y * speed * Time.deltaTime, 0);
		}
	}

	public void MoveToward(Vector3 position){
		if (!dead) {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, position, step);
		}
	}


	public bool Dash(){
		if (!dashLocked) {
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
}