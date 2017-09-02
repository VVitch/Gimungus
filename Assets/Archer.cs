using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour {
	public GameObject ArrowPrefab;
	public Unit unit;
	Unit playerUnit;
	bool choosePosition, inPosition, shooting = false;
	Vector3 position;
	// Use this for initialization
	void Start () {
		playerUnit = GameObject.Find ("PlayerInputController").GetComponent<PlayerInputController> ().playerUnit;
	}
	//choose random spot
	//shoot volley
	//repeat
	
	// Update is called once per frame
	void Update () {
		if (!choosePosition) {
			ChoosePosition ();
		}else if(!inPosition){
			MoveToPosition();
		}else if (!shooting){
			StartCoroutine(FireVolley());
		}
		//Shoot (playerUnit.transform.position);
	}

	IEnumerator FireVolley(){
		shooting = true;
		int shots = Random.Range (1, 5);
		for (int i = 0; i < shots; i++) {
			Shoot (playerUnit.transform.position);
			yield return new WaitForSeconds(0.2f);
		}
		choosePosition = false;
		inPosition = false;
		shooting = false;
	}

	void ChoosePosition(){
		int negX = Random.Range (-1, 1);
		int negY = Random.Range (-1, 1);
		if(negX < 0){negX = -1;}else {negX = 1;}
		if(negY < 0){negY = -1;}else {negY = 1;}

		float xComp = playerUnit.transform.position.x + Random.Range (15,20) * negX; 
		float yComp = playerUnit.transform.position.y + Random.Range(15,20) * negY;
		position = new Vector3 (xComp, yComp, 0);
		choosePosition = true;
	}
		
	void Shoot(Vector3 position){
		Arrow arrow = GameObject.Instantiate (ArrowPrefab, unit.transform.position, Quaternion.identity).GetComponent<Arrow>();
		Quaternion nRotation = Quaternion.LookRotation (Vector3.forward, position - unit.transform.position);
		arrow.myRotation = nRotation;
		Vector3 pos = (position - unit.transform.position);
		pos.Normalize ();
		arrow.SetVelocity (pos);
	}

	void MoveToPosition(){
		unit.MoveToward (position);
		if (Vector3.Distance (unit.transform.position, position) < 1) {
			inPosition = true;
		}
	}
}
