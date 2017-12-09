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
	public BackgroundEntry background;
	public CharacterEntry[] characters = new CharacterEntry[BattleConstants.DIALOGUE_PLAYERS_COUNT];
	public int[] poses = new int[BattleConstants.DIALOGUE_PLAYERS_COUNT];
	public string talkingName;
	public int talkingIndex;
	public string dialogueText;
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