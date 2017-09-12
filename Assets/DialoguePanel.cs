using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialoguePanel : MonoBehaviour {
	Vector3 inPosition;
	Vector3 outPosition = new Vector3 (1000, 1000, 1000);
	public NPC npc;
	public Text nameText, chatText;
	List<Vector3> responsePositions;
	public List<Text> responseTexts;
	public List<GameObject> responseButtons;
	// Use this for initialization
	void Start () {
		responsePositions = new List<Vector3> ();
		inPosition = transform.position;
		responsePositions.Add(responseButtons[0].transform.position);
		responsePositions.Add(responseButtons[1].transform.position);
		responsePositions.Add(responseButtons[2].transform.position);
		MakePanelDisappear ();
		MakeButtonsDisappear ();
	}

	public void MakePanelAppear(){
		transform.position = inPosition;
	}

	public void MakePanelDisappear(){
		transform.position = outPosition;
	}

	public void MakeButtonsAppear(){
		for (int i = 0; i < responseButtons.Count; i++) {
			responseButtons[i].transform.position =	responsePositions[i];
		}
	}

	public void MakeButtonsDisappear ()
	{
		for (int i = 0; i < responseButtons.Count; i++) {
			responseButtons[i].transform.position =	outPosition;
		}
	}
	
	// Update is called once per frame
	/*
	void Update () {
		
	}*/
}