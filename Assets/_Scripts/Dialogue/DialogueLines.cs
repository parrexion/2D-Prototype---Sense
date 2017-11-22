using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLines {

	public int pos = 0;
	private int filled = 0;
	
	public Dialogue dialogue;

	public DialogueLines(Dialogue dialogue){
		this.dialogue = dialogue;
	}

	public void NextDialogue(DialogueScene scene){
		
		if (pos >= dialogue.size) {
			Debug.Log("Reached the end");
			DialogueAction da = (DAEndDialogue)ScriptableObject.CreateInstance("DAEndDialogue");
			da.Act(scene,null);
		}
		else {
			CompareScenes(scene,dialogue.frames[pos]);
			pos++;
		}
	}

	public void CompareScenes(DialogueScene scene, Frame frame) {
		DialogueAction da;
		DialogueJsonItem data;
		if (scene.background != frame.background) {
			da = (DASetBackground)ScriptableObject.CreateInstance("DASetBackground");
			data = new DialogueJsonItem();
			data.character = frame.background;
			da.Act(scene,data);
		}
		for (int i = 0; i < 4; i++) {
			if (scene.positions[i] != frame.currentCharacters[i] || scene.currentPoses[i] != frame.currentPoses[i]) {
				da = (DAAddCharacter)ScriptableObject.CreateInstance("DAAddCharacter");
				data = new DialogueJsonItem();
				data.position1 = i;
				data.character = frame.currentCharacters[i];
				data.pose = frame.currentPoses[i];
				da.Act(scene,data);
			}
		}

		if (scene.talkingCharacter != frame.talkingPosition || scene.talkingPose != frame.currentCharacters[frame.talkingPosition]) {
			da = (DAChangeTalking)ScriptableObject.CreateInstance("DAChangeTalking");
			data = new DialogueJsonItem();
			data.character = frame.talkingPosition;
			data.pose = frame.currentCharacters[frame.talkingPosition];
			da.Act(scene,data);
		}

		if (scene.characterName != frame.characterName) {
			da = (DASetName)ScriptableObject.CreateInstance("DASetName");
			data = new DialogueJsonItem();
			data.text = frame.characterName;
			da.Act(scene,data);
		}

		if (scene.dialogue != frame.dialogueText) {
			da = (DASetText)ScriptableObject.CreateInstance("DASetText");
			data = new DialogueJsonItem();
			data.text = frame.dialogueText;
			da.Act(scene,data);
		}
	}

	public int getFilled(){
		return filled;
	}
}
