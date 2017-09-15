using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {
	protected Unit playerUnit;
	public Unit unit;
	public int strikingDistance;
	public int seeingDistance;
	protected bool playerSeen = false;

	// Use this for initialization
	public void Start () {
		playerUnit = GameObject.Find ("PlayerInputController").GetComponent<PlayerInputController> ().playerUnit;
	}
	
	// Update is called once per frame
	public void Update () {
		if(Vector3.Distance(playerUnit.transform.position, unit.transform.position) < seeingDistance){
			playerSeen = true;
		}
	}

	protected void CheckForPlayer(){
		if(Vector3.Distance(playerUnit.transform.position, unit.transform.position) < seeingDistance){
			playerSeen = true;
		}
	}
}
