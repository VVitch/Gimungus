using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour {

	public float timeToFade, minValue, maxValue;
	SpriteRenderer sr;
	void Awake(){
		sr = gameObject.GetComponent<SpriteRenderer> ();
	}
	// Use this for initialization
	void Start () {
		StartCoroutine (FadeIn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator FadeIn(){
		float t = sr.color.a;
		while (t > minValue) {
			t -= Time.deltaTime / timeToFade;
			sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, t);
			yield return null;
		}
	}

	IEnumerator FadeOut(){
		yield return null;
	}
}
