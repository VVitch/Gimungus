using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interior : MonoBehaviour {
	public float fadeSpeed;
	public SpriteRenderer roof;
	public SpriteRenderer roofFilling;
	public SpriteRenderer floor;
	public SpriteRenderer background0;
	public SpriteRenderer background1;
	public SpriteRenderer background2;
	public SpriteRenderer background3;

	Collider2D player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(player!=null){
			if(!this.GetComponent<Collider2D>().IsTouching(player) && background0.color.a == 1){
					ExitFade();
					Debug.Log("safteyFade");
					StartCoroutine(ResetSafteyFade());
				
			}
		}
	}

	void LateUpdate(){

	}

	void EnterFade(){
		StartCoroutine (FadeInSprite (background0));
		StartCoroutine (FadeInSprite (background1));
		StartCoroutine (FadeInSprite (background2));
		StartCoroutine (FadeInSprite (background3));
		StartCoroutine (FadeOutSprite (roof));
		StartCoroutine (FadeOutSprite (roofFilling));
	}
	

	void ExitFade(){
		StartCoroutine (FadeOutSprite (background0));
		StartCoroutine (FadeOutSprite (background1));
		StartCoroutine (FadeOutSprite (background2));
		StartCoroutine (FadeOutSprite (background3));
		StartCoroutine (FadeInSprite (roof));
		StartCoroutine (FadeInSprite (roofFilling));
	}

	void OnTriggerEnter2D(Collider2D other) 
	{ 
		if(other.transform.tag == "player unit"){
			EnterFade();
			player = other;
		}
	}

	void OnTriggerExit2D(Collider2D other) 
	{ 
		if(other.transform.tag == "player unit"){
			ExitFade();
		}
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

	IEnumerator ResetSafteyFade(){
		float t = 0;
		while (t < fadeSpeed*2) {
			t+=Time.deltaTime;
			yield return null;
		}
		player = null;
	}
}
