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
	public PlayerInputController pic;

	public float scrollDelay, buttonDelay;
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
		transform.localScale = new Vector3(1,1,1);
	}

	public void MakePanelDisappear(){
		transform.localScale = new Vector3(0,0,0);
	}

	public void MakeButtonsAppear(List<string> responses){
		for (int i = 0; i < responses.Count; i++) {
			if (responses [i] != "") {
				responseButtons [i].transform.localScale = new Vector3 (1, 1, 1);
				responseTexts [i].text = responses [i];
			}
		}
	}
	public void MakeButtonsDisappear ()
	{
		for (int i = 0; i < responseButtons.Count; i++) {
			responseButtons [i].transform.localScale = new Vector3 (0, 0, 0);
		}
	}

	public void Activate(string addon){
		MakeButtonsDisappear ();
		pic.Disable ();
		MakePanelAppear ();
		//chatText.text = 
		nameText.text = npc.name;
		StartCoroutine(ScrollText(addon + " " + npc.openers[npc.dialogueIndex]));

	}
	IEnumerator BringInButtons(){
		for (int i = npc.dialogueIndex*3; i < npc.dialogueIndex*3 + 3; i++) {
			Debug.Log (i);
			if (npc.responses [i] != "") {
				yield return new WaitForSeconds (buttonDelay);
				responseButtons [i % 3].transform.localScale = new Vector3 (1, 1, 1);
				responseTexts [i % 3].text = npc.responses [i];
			}
		}	
	}
	IEnumerator ScrollText(string line){
		chatText.text = "";
		float t = 0;
		for (int i = 0; i < line.Length; i++) {
			yield return new WaitForSeconds(scrollDelay);
			chatText.text += line[i];
		}
		StartCoroutine (BringInButtons ());
	}
	public void EnterResponse(int choice){
		Debug.Log ("chose" + choice);
		MakeButtonsDisappear ();
		if (npc.dialogueIndex + 1 < npc.openers.Count) {
			Debug.Log (npc.dialogueIndex * 3 + choice);
			Debug.Log(npc.closers.Count);
			string chosenResponse = npc.closers [npc.dialogueIndex * 3 + choice];
			npc.dialogueIndex += 1;
			Activate (chosenResponse);
		} else {
			pic.Enable ();
			MakePanelDisappear ();
		}
	}
}

