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
	public List<Frame> frames;
}


[System.Serializable]
public class Frame {
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
	public int character;
	public int pose;
	public int position1;
	public int position2;
	public string text;
}