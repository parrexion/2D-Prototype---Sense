using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine.Events;

public class DialogueParser : MonoBehaviour {

	public StringVariable currentLocation;
	public BoolVariable equipMenuAvailable;
	public DialogueCollection dialogues;
	public UnityEvent nextFrameEvent;


	// Use this for initialization
	void Start () {
		string file = "Assets/Resources/test.json";
		dialogues = new DialogueCollection(1);

		currentLocation.value = "Location: Dialogue";
		equipMenuAvailable.value = false;
//		TemporaryParse();
//		SaveJson(file);

		LoadDialogue(file);
	}

	void LoadDialogue(string filename) {

		if (File.Exists(filename)) {
			string dataAsJson = File.ReadAllText(filename);
			dialogues = JsonUtility.FromJson<DialogueCollection>(dataAsJson);

			Debug.Log("Number of dialogues: "+dialogues.dialogues.Length);
			nextFrameEvent.Invoke();
		}
		else {
			Debug.LogError("Could not open file: "+filename);
		}
	}

	private void SaveJson(string filename){
		string str = JsonUtility.ToJson(dialogues,true);
		using (FileStream fs = new FileStream(filename,FileMode.Create)) {
			using (StreamWriter writer = new StreamWriter(fs)) {
				writer.Write(str);
				Debug.Log("Saved");
			}
		}
	}
}
