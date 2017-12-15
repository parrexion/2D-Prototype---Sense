using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DialogueCollection {
	public Dialogue[] dialogues;

	public DialogueCollection(int size){
		dialogues = new Dialogue[size];
	}
}


[System.Serializable]
public class Dialogue {
	public string name;
	public int size;
	public List<OldFrame> frames;
}


[System.Serializable]
public class Frame {
	public BackgroundEntry background = null;
	public CharacterEntry[] characters = new CharacterEntry[Constants.DIALOGUE_PLAYERS_COUNT];
	public int[] poses = new int[Constants.DIALOGUE_PLAYERS_COUNT];
	public string talkingName = "";
	public int talkingIndex = -1;
	public string dialogueText = "";
	public int talkingPose { get {
			if (talkingIndex == -1 || talkingIndex == 4)
				return -1;
			return poses[talkingIndex];
		} }

	public void CopyValues(Frame other) {
		background = other.background;
		characters = other.characters;
		poses = other.poses;
		talkingIndex = other.talkingIndex;
		talkingName = other.talkingName;
		dialogueText = other.dialogueText;
	}
}

[System.Serializable]
public class OldFrame {
	public int background;
	public int[] currentCharacters;
	public int[] currentPoses;
	public string characterName;
	public string dialogueText;
	public int talkingPosition;
}


[System.Serializable]
public class DialogueJsonItem {

	public enum actionType {ADDCHAR,REMOVECHAR,CHANGEPOS,SETBACKGROUND,CHANGETALKING,SETNAME,SETTEXT,ENDTEXT,ENDDIALOGUE};

	public actionType type;
	public ScrObjLibraryEntry entry;
	public int value;
	public int position1;
	public int position2;
	public string text;
}