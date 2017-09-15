using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : UnitController {
	public List<Unit> squad;
	public List<Vector3> positions;
	bool positionsAssigned, playerSurrounded, campEstablished = false;
	Vector3 playerOriginalPosition;
	public int surroundRange, campDistance, commanderDistance, strikeDistance;

	// Use this for initialization
	void Start () {
		base.Start ();
		positions = new List<Vector3> ();
	}
	
	// Update is called once per frame
	void Update () {
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

		if (!ntr && campEstablished) {
			MaintainCommanderDistance ();
		}
		AimWeapons ();

	}

	void MoveSquadWithCommander(){
		for (int i = 0; i < squad.Count; i++) {
			squad [i].SetVelocity(unit.GetVelocity());
		}
	}

	void StopSquad(){
		for (int i = 0; i < squad.Count; i++) {
			squad [i].Stop ();
		}
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
		return !unit_alive || unit.dead;
	}
	void MaintainCommanderDistance(){
		if (Vector3.Distance (playerUnit.transform.position, unit.transform.position) < commanderDistance) {
			unit.MoveAway (playerUnit.transform.position);
		} else if (Vector3.Distance (playerUnit.transform.position, unit.transform.position) > campDistance) {
			unit.MoveToward (playerUnit.transform.position);
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
			if (squad [i].dead) {
				positionCount++;
				squad.RemoveAt (i);
				i--;
			} else if (Vector3.Distance (squad [i].transform.position, goal) < 2 &&!squad[i].dead) {
				positionCount++;
				squad [i].SetVelocity (playerUnit.GetVelocity());
			} else {
				squad [i].MoveToward (goal);
			}
		}
		if (positionCount == squad.Count) {
			playerSurrounded = true;
			StopSquad ();
		}
	}
	void ChargePlayer(){
		for (int i = 0; i < squad.Count; i++) {
			if (Mathf.Abs(Vector3.Distance (squad [i].transform.position, playerUnit.transform.position) - strikeDistance) < .8f) {
				squad [i].AttackWithWeapon ();
				squad [i].SetVelocity (playerUnit.GetVelocity());
			} else{
				squad [i].MoveToward (playerUnit.transform.position);
			}
		}
	}

	void EstablishCamp(){
		unit.MoveToward (playerUnit.transform.position);
		MoveSquadWithCommander ();

		for (int i = 0; i < squad.Count; i++) {
			if (squad [i].dead) {
				squad [i].Stop ();
				squad [i].gameObject.transform.parent = null;
				squad.RemoveAt (i);
			}
		}

		if (Vector3.Distance (unit.transform.position, playerUnit.transform.position)<campDistance) {
			campEstablished = true;
			while (unit.transform.childCount > 0) {
				foreach (Transform child in unit.transform) {
					child.parent = this.gameObject.transform;
					unit.Stop ();
					StopSquad ();
				}
			}
		}
	}
	void Retreat(){
		unit.MoveAway (playerUnit.transform.position);
		for (int i = 0; i < squad.Count; i++) {
			squad [i].MoveAway (playerUnit.transform.position);
			if (Vector3.Distance (squad [i].transform.position, playerUnit.transform.position) > 100) {
				squad [i].Die ();
				squad.RemoveAt (i);
			}
		}
		if (Vector3.Distance (unit.transform.position, playerUnit.transform.position) > 100) {
			unit.Die ();
		}
	}
		
}