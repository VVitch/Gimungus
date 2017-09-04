using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interior : MonoBehaviour {
	public float fadeSpeed;
	public SpriteRenderer roof;
	public SpriteRenderer floor;
	public SpriteRenderer background;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) 
	{ 
		StartCoroutine (FadeInSprite (background));
		StartCoroutine (FadeOutSprite (roof));
	}

	void OnTriggerExit2D(Collider2D other) 
	{ 
		StartCoroutine (FadeOutSprite (background));
		StartCoroutine (FadeInSprite (roof));
	}

	IEnumerator FadeInSprite(SpriteRenderer sr){
		float t = sr.color.a * fadeSpeed;
		while (t < fadeSpeed) {
			t += Time.deltaTime;
			sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, t/fadeSpeed);
			yield return null;
		}
		sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, 1f);
	}

	IEnumerator FadeOutSprite(SpriteRenderer sr){
		float t = sr.color.a * fadeSpeed;
		while (t > 0) {
			t -= Time.deltaTime;
			sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, t/fadeSpeed);
			yield return null;
		}
		sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, 0f);
	}
}
