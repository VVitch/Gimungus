using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatter : MonoBehaviour {
	public float time;
	// Use this for initialization
	void Start () {
		StartCoroutine(FreezeBloodSplatter());
	}
	
	// Update is called once per frame
	void Update () {
					transform.RotateAround(transform.position, Vector3.forward, 200 * Time.deltaTime);

	}

	IEnumerator FreezeBloodSplatter(){
		float t = 0;
		gameObject.GetComponent<ParticleSystem>().playbackSpeed = 6f;

 		while(t < time){
			t+= Time.deltaTime;
			yield return null;
		}
		gameObject.GetComponent<ParticleSystem>().Pause(true);
	}
}
