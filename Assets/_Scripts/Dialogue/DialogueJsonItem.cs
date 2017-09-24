using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



[System.Serializable]
public class DialogueDialogues {
	public DialogueLines[] lines;

	public DialogueDialogues(int size){
		lines = new DialogueLines[size];
	}
}



[System.Serializable]
public class DialogueJsonScene {

	public int background = 0;
	public int[] positions = new int[]{-1,-1,-1,-1,-1};
	public int[] currentPoses = new int[5];
	public string characterName = "";
	public string dialogue = "";
	public int talkingCharacter = -1;
	public int talkingPose = -1;
}