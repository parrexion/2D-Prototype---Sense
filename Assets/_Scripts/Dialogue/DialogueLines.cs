using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLines {

	public int pos = 0;
	private int filled = 0;
	
	public DialogueJsonScene[] dataList;
	public int size = 0;


	public DialogueLines(int size){
		this.size = size;
		dataList = new DialogueJsonScene[size];
	}


//	public void AddAction(DialogueAction action, DialogueJsonScene data){
//		dataList[filled] = data;
//		actions[filled] = action;
//		filled++;
//	}

	public void NextDialogue(DialogueScene scene){
		
		if (pos >= size) {
			Debug.Log("Reached the end");
			DialogueAction da = (DAEndDialogue)ScriptableObject.CreateInstance("DAEndDialogue");
			da.Act(scene,null);
		}
		else {
			CompareScenes(scene,dataList[pos]);
			pos++;
		}
	}

	public void CompareScenes(DialogueScene scene, DialogueJsonScene json) {
		DialogueAction da;
		DialogueJsonItem data;
		if (scene.background != json.background) {
			da = (DASetBackground)ScriptableObject.CreateInstance("DASetBackground");
			data = new DialogueJsonItem();
			data.character = json.background;
			da.Act(scene,data);
		}
		for (int i = 0; i < 4; i++) {
			if (scene.positions[i] != json.positions[i] || scene.currentPoses[i] != json.currentPoses[i]) {
				da = (DAAddCharacter)ScriptableObject.CreateInstance("DAAddCharacter");
				data = new DialogueJsonItem();
				data.position1 = i;
				data.character = json.positions[i];
				data.pose = json.currentPoses[i];
				da.Act(scene,data);
			}
		}

		if (scene.talkingCharacter != json.talkingCharacter || scene.talkingPose != json.talkingPose) {
			da = (DAChangeTalking)ScriptableObject.CreateInstance("DAChangeTalking");
			data = new DialogueJsonItem();
			data.character = json.talkingCharacter;
			data.pose = json.talkingPose;
			da.Act(scene,data);
		}

		if (scene.characterName != json.characterName) {
			da = (DASetName)ScriptableObject.CreateInstance("DASetName");
			data = new DialogueJsonItem();
			data.text = json.characterName;
			da.Act(scene,data);
		}

		if (scene.dialogue != json.dialogue) {
			da = (DASetText)ScriptableObject.CreateInstance("DASetText");
			data = new DialogueJsonItem();
			data.text = json.dialogue;
			da.Act(scene,data);
		}
	}

	public int getFilled(){
		return filled;
	}
}
