﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour {
	public Sprite cutSprite, bushelSprite;
	public GameObject bushel;
	bool isCut = false;
	public float flingTime, flingDistance;


	void OnTriggerEnter2D(Collider2D other) 
	{ 
		if (other.gameObject.tag == "damage source") {
			GetCut ();
		} 
	}

	void GetCut(){
		if(!isCut){
			isCut = true;
			//spawn a bushell
			//switch to cut sprite
			GetComponent<SpriteRenderer>().sprite = cutSprite;
				GameObject newBushel = GameObject.Instantiate(bushel, transform.position, Quaternion.identity);
				newBushel.GetComponent<SpriteRenderer> ().color = GetComponent<SpriteRenderer> ().color;
				StartCoroutine (FlingBushel(newBushel, flingTime));
		}
	}

	public IEnumerator FlingBushel(GameObject bushel, float timeToMove)
	{
		Vector3 position = bushel.transform.position + new Vector3 (Random.Range (-flingDistance, flingDistance+1), Random.Range (-flingDistance, flingDistance+1), 0);
		var currentPos = bushel.transform.position;
		var t = 0f;
		while (t < 1)
		{
			t += Time.deltaTime / timeToMove;
			bushel.transform.position = Vector3.Lerp(currentPos, position, t);
			yield return null;
		}
	}
}
