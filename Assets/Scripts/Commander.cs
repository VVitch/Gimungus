using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : MonoBehaviour {
	public Unit commanderUnit;
	public List<Unit> squad;
	public List<Vector3> positions;
	Unit playerUnit;
	bool playerSeen, positionsAssigned, playerSurrounded, campEstablished, disabled = false;
	Vector3 playerOriginalPosition;
	public int surroundRange, viewDistance, campDistance, commanderDistance, strikeDistance;

	// Use this for initialization
	void Start () {
		positions = new List<Vector3> ();
		playerUnit = GameObject.Find ("PlayerInputController").GetComponent<PlayerInputController> ().playerUnit;
	}
	
	// Update is called once per frame
	void Update () {
		if (!disabled) {
			bool ntr = NeedToRetreat ();
			if (ntr) {
				Retreat ();
			} else if (!playerSeen) {
				CheckForPlayer ();
			} else if (!campEstablished) {
				EstablishCamp ();
			} else if (!positionsAssigned) {
				AssignPositions ();
			} else if (!playerSurrounded) {
				SurroundPlayer ();
			} else {
				ChargePlayer ();
			}
			if (!ntr) {
				KeepCommanderSafe ();
			}
		}
		AimWeapons ();

	}

	void AimWeapons(){
		for (int i = 0; i < squad.Count; i++) {
			if (!squad [i].dead) {
				squad [i].weapon.Aim (playerUnit.transform.position);
			}
		}
	}
	bool NeedToRetreat(){
		bool unit_alive = false;
		foreach(Unit u in squad){
			if (!u.dead) {
				unit_alive = true;
			}
		}
		return !unit_alive || commanderUnit.dead;
	}
	void KeepCommanderSafe(){
		if (Vector3.Distance (playerUnit.transform.position, commanderUnit.transform.position) < commanderDistance) {
			commanderUnit.MoveAway (playerUnit.transform.position);
		} else if (Vector3.Distance (playerUnit.transform.position, commanderUnit.transform.position) > campDistance) {
			commanderUnit.MoveToward (playerUnit.transform.position);
		}
	}

	void CheckForPlayer(){
		if(Vector3.Distance(playerUnit.transform.position, commanderUnit.transform.position) < viewDistance){
			playerSeen = true;
		}
	}

	void AssignPositions(){
		playerOriginalPosition = playerUnit.transform.position;
		float degreePortion = 360f/squad.Count;
		float offset = Random.Range (-360, 360);
		for (float a = 0; a <= 360; a += degreePortion) {
			float xComp = (surroundRange * Mathf.Cos (a + offset * Mathf.PI / 180f)) + playerUnit.transform.position.x;
			float yComp = (surroundRange * Mathf.Sin (a + offset * Mathf.PI / 180f)) + playerUnit.transform.position.y;
			positions.Add (new Vector3 (xComp, yComp, 0));
		}
		positionsAssigned = true;
	}
	void SurroundPlayer(){
		int positionCount = 0;
		for (int i = 0; i < squad.Count; i++) {
			Vector3 goal = positions [i] - (playerOriginalPosition - playerUnit.transform.position);
			squad [i].MoveToward (goal);
			if (squad [i].dead) {
				positionCount++;
				squad.RemoveAt (i);
				i--;
			}else if(Vector3.Distance (squad [i].transform.position, goal) < 2) {
				positionCount++;
			}
		}
		if (positionCount == squad.Count) {
			playerSurrounded = true;
		}
	}
	void ChargePlayer(){
		for (int i = 0; i < squad.Count; i++) {
			if (Vector3.Distance (squad [i].transform.position, playerUnit.transform.position) < strikeDistance) {
				squad [i].AttackWithWeapon ();
			} else{
				squad [i].MoveToward (playerUnit.transform.position);
			}
		}
	}

	void EstablishCamp(){

		commanderUnit.MoveToward (playerUnit.transform.position);
		for (int i = 0; i < squad.Count; i++) {
			if (squad [i].dead) {
				squad [i].gameObject.transform.parent = null;
				squad.RemoveAt (i);
			}
		}

		if (Vector3.Distance (commanderUnit.transform.position, playerUnit.transform.position)<campDistance) {
			campEstablished = true;
			while (commanderUnit.transform.childCount > 0) {
				foreach (Transform child in commanderUnit.transform) {
					child.parent = this.gameObject.transform;
				}
			}
		}
	}
	void Retreat(){
		commanderUnit.MoveAway (playerUnit.transform.position);
		for (int i = 0; i < squad.Count; i++) {
			squad [i].MoveAway (playerUnit.transform.position);
			if (Vector3.Distance (squad [i].transform.position, playerUnit.transform.position) > 100) {
				squad [i].Die ();
				squad.RemoveAt (i);
			}
		}
		if (Vector3.Distance (commanderUnit.transform.position, playerUnit.transform.position) > 100) {
			commanderUnit.Die ();
			disabled = true;
		}
	}
		
}