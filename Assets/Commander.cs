using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : MonoBehaviour {
	public Unit commanderUnit;
	public List<Unit> squad;
	public List<Vector3> positions;
	Unit playerUnit;
	bool playerSeen, positionsAssigned, playerSurrounded, campEstablished = false;
	Vector3 playerOriginalPosition;
	public int surroundRange;

	//Routine Order
	//Establish Camp
	//Surround Player (maintaining distance)
	//Charge Player

	// Use this for initialization
	void Start () {
		positions = new List<Vector3> ();
		playerUnit = GameObject.Find ("PlayerInputController").GetComponent<PlayerInputController> ().playerUnit;
	}
	
	// Update is called once per frame
	void Update () {
		KeepCommanderSafe ();

		if (!playerSeen) {
			CheckForPlayer ();
		} else if (!campEstablished) {
			EstablishCamp ();
		}else if (!positionsAssigned) {
			AssignPositions ();
		} else if (!playerSurrounded) {
			SurroundPlayer ();
		} else {
			ChargePlayer ();
		}
	}

	void KeepCommanderSafe(){
		if (Vector3.Distance (playerUnit.transform.position, commanderUnit.transform.position) < 15) {
			commanderUnit.MoveAway (playerUnit.transform.position);
		}
	}

	void CheckForPlayer(){
		if(Vector3.Distance(playerUnit.transform.position, commanderUnit.transform.position) < 50){
			Debug.Log ("Player Seen!");
			playerSeen = true;
		}
	}

	void AssignPositions(){
		playerOriginalPosition = playerUnit.transform.position;
		//20*Mathf(cos((degrees * 2*PI / 180f))
		float degreePortion = 360f/squad.Count;
		float offset = Random.Range (-360, 360);
		for (float a = 0; a <= 360; a += degreePortion) {
			float xComp = (surroundRange * Mathf.Cos (a + offset * Mathf.PI / 180f)) + playerUnit.transform.position.x;
			float yComp = (surroundRange * Mathf.Sin (a + offset * Mathf.PI / 180f)) + playerUnit.transform.position.y;
			positions.Add (new Vector3 (xComp, yComp, 0));
		}
		positionsAssigned = true;
		Debug.Log ("positions assigned");
	}
	void SurroundPlayer(){
		int positionCount = 0;
		for (int i = 0; i < squad.Count; i++) {
			Vector3 goal = positions [i] - (playerOriginalPosition - playerUnit.transform.position);
			squad [i].MoveToward (goal);
			if (squad[i].dead || Vector3.Distance (squad [i].transform.position, goal) < 2) {
				positionCount++;
			}
		}
		if (positionCount == squad.Count) {
			playerSurrounded = true;
		}
		Debug.Log ((positions [0] + (playerUnit.transform.position - playerOriginalPosition)));
	}
	void ChargePlayer(){
		for (int i = 0; i < squad.Count; i++) {
			squad [i].MoveToward (playerUnit.transform.position);
			squad [i].TouchAttack (playerUnit);
		}
	}

	void EstablishCamp(){
		for (int i = 0; i < squad.Count; i++) {
			//squad [i].MoveToward (playerUnit.transform.position);
		}
		commanderUnit.MoveToward (playerUnit.transform.position);

		if (Vector3.Distance (commanderUnit.transform.position, playerUnit.transform.position)<25) {
			campEstablished = true;
			while (commanderUnit.transform.childCount > 0) {
				foreach (Transform child in commanderUnit.transform) {
					child.parent = null;
				}
			}
		}
	}
		
}