using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {
	public GameObject speechBubble;
	public Unit unit;
	public float floatTimer, floatSpeed;
	float t = 0;

	List<GameObject> checkpoints;
	// Use this for initialization
	void Start () {
		MakeBubbleDisappear ();
		speechBubble.GetComponent<Rigidbody2D> ().velocity = new Vector2(0,1) * floatSpeed;

	}
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		if (t > floatTimer) {
			speechBubble.GetComponent<Rigidbody2D> ().velocity = -speechBubble.GetComponent<Rigidbody2D> ().velocity;
			t = 0;
		}
	}

	void OnTriggerEnter2D(Collider2D other){ 
		if (other.tag == "player unit") {
			MakeBubbleAppear ();
		}
	}

	void OnTriggerExit2D(Collider2D other){ 
		if (other.tag == "player unit") {
			MakeBubbleDisappear ();
		}
	}
	void MakeBubbleAppear(){
		speechBubble.GetComponent<SpriteRenderer> ().enabled = true;
	}
	void MakeBubbleDisappear(){
		speechBubble.GetComponent<SpriteRenderer> ().enabled = false;
	}
}
