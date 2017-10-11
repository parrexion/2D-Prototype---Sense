using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;
using System.IO;
using System.Text.RegularExpressions;

public class DialogueParser : MonoBehaviour {

	public DialogueDialogues dialogues;


	// Use this for initialization
	void Start () {
		string file = "Assets/Resources/test.json";
		dialogues = new DialogueDialogues(1);
			
//		TemporaryParse();
//		SaveJson(file);

		LoadDialogue(file);
	}

	void LoadDialogue(string filename) {

		if (File.Exists(filename)) {
			string dataAsJson = File.ReadAllText(filename);
			dialogues = JsonUtility.FromJson<DialogueDialogues>(dataAsJson);

			Debug.Log("Number of dialogues: "+dialogues.lines.Length);
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

//	private void TemporaryParse(){
//
//		DialogueLines lineEntry = new DialogueLines(24);
//		dialogues.lines[0] = lineEntry;
//
//		ParseAction(lineEntry, "AddChar", 0, 0, 0, "");
//		ParseAction(lineEntry, "AddChar", 1, 1, 1, "");
//		ParseAction(lineEntry, "ChangeTalking", 1, 1, 0, "");
//		ParseAction(lineEntry, "SetName", 0, 0, 0, "Orien");
//		ParseAction(lineEntry, "SetText", 0, 0, 0, "Hello there!");
//		ParseAction(lineEntry, "EndText", 0, 0, 0, "");
//		ParseAction(lineEntry, "SetBackground", 1, 0, 0, "");
//		ParseAction(lineEntry, "ChangePos", 0, 2, 0, "");
//		ParseAction(lineEntry, "ChangePos", 1, 0, 0, "");
//		ParseAction(lineEntry, "ChangePos", 2, 0, 0, "");
//		ParseAction(lineEntry, "ChangeTalking", 3, 1, 0, "");
//		ParseAction(lineEntry, "SetName", 0, 0, 0, "Joe");
//		ParseAction(lineEntry, "SetText", 0, 0, 0, "Hello to you as well!");
//		ParseAction(lineEntry, "AddChar", 1, 2, 2, "");
//		ParseAction(lineEntry, "AddChar", 3, 3, 0, "");
//		ParseAction(lineEntry, "EndText", 0, 0, 0, "");
//		ParseAction(lineEntry, "SetBackground", 2, 0, 0, "");
//		ParseAction(lineEntry, "ChangeTalking", -1, 0, 0, "");
//		ParseAction(lineEntry, "SetName", 0, 0, 0, "");
//		ParseAction(lineEntry, "SetText", 0, 0, 0, "This is a long text.");
//		ParseAction(lineEntry, "RemoveChar", 0, 0, 0, "");
//		ParseAction(lineEntry, "RemoveChar", 2, 0, 0, "");
//		ParseAction(lineEntry, "EndText", 0, 0, 0, "");
//		ParseAction(lineEntry, "EndDialogue", 0, 0, 0, "");
//	}

//	private void ParseAction(DialogueLines line, string action, int val1, int val2, int val3, string text1) {
//
//		DialogueAction da = null;
//		DialogueJsonItem data = null;
//
//		switch (action) {
//		case "AddChar": 
//			da = (DAAddCharacter)ScriptableObject.CreateInstance("DAAddCharacter");
//			data = new DialogueJsonItem();
//			data.type = DialogueJsonItem.actionType.ADDCHAR;
//			data.position1 = val1;
//			data.character = val2;
//			data.pose = val3;
//			break;
//		case "RemoveChar": 
//			da = (DARemoveCharacter)ScriptableObject.CreateInstance("DARemoveCharacter");
//			data = new DialogueJsonItem();
//			data.type = DialogueJsonItem.actionType.REMOVECHAR;
//			data.position1 = val1;
//			break;
//		case "ChangePos": 
//			da = (DAChangePosition)ScriptableObject.CreateInstance("DAChangePosition");
//			data = new DialogueJsonItem();
//			data.type = DialogueJsonItem.actionType.CHANGEPOS;
//			data.position1 = val1;
//			data.position2 = val2;
//			break;
//
//		case "SetBackground": 
//			da = (DASetBackground)ScriptableObject.CreateInstance("DASetBackground");
//			data = new DialogueJsonItem();
//			data.type = DialogueJsonItem.actionType.SETBACKGROUND;
//			data.character = val1;
//			break;
//
//		case "ChangeTalking": 
//			da = (DAChangeTalking)ScriptableObject.CreateInstance("DAChangeTalking");
//			data = new DialogueJsonItem();
//			data.type = DialogueJsonItem.actionType.CHANGETALKING;
//			data.character = val1;
//			data.pose = val2;
//			break;
//
//		case "SetName": 
//			da = (DASetName)ScriptableObject.CreateInstance("DASetName");
//			data = new DialogueJsonItem();
//			data.type = DialogueJsonItem.actionType.SETNAME;
//			data.text = text1;
//			break;
//		case "SetText": 
//			da = (DASetText)ScriptableObject.CreateInstance("DASetText");
//			data = new DialogueJsonItem();
//			data.type = DialogueJsonItem.actionType.SETTEXT;
//			data.text = text1;
//			break;
//
//		case "EndText": 
//			da = (DAEndText)ScriptableObject.CreateInstance("DAEndText");
//			data = new DialogueJsonItem();
//			data.type = DialogueJsonItem.actionType.ENDTEXT;
//			break;
//		case "EndDialogue": 
//			da = (DAEndDialogue)ScriptableObject.CreateInstance("DAEndDialogue");
//			data = new DialogueJsonItem();
//			data.type = DialogueJsonItem.actionType.ENDDIALOGUE;
//			break;
//		}
//
//		line.AddAction(da, data);
//	}
}
