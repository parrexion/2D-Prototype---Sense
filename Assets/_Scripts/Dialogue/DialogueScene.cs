using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueScene : MonoBehaviour {

	public BackgroundEntry background;
	[SerializeField] private CharacterEntry character0;
	[SerializeField] private CharacterEntry character1;
	[SerializeField] private CharacterEntry character2;
	[SerializeField] private CharacterEntry character3;
	[SerializeField] private CharacterEntry character4;
	[SerializeField] private IntVariable pose0;
	[SerializeField] private IntVariable pose1;
	[SerializeField] private IntVariable pose2;
	[SerializeField] private IntVariable pose3;
	[SerializeField] private IntVariable pose4;
	public StringVariable talkingName;
	public IntVariable talkingCharacter;
	public IntVariable talkingPose;
	public StringVariable dialogueText;

	[HideInInspector] public CharacterEntry[] characters;
	[HideInInspector] public IntVariable[] poses;


	void Start() {
		characters = new CharacterEntry[]{ character0, character1, character2, character3, character4 };
		poses = new IntVariable[]{pose0, pose1, pose2, pose3, pose4};

		background = null;
		character0 = null;
		character1 = null;
		character2 = null;
		character3 = null;
		character4 = null;
		pose0.value = -1;
		pose1.value = -1;
		pose2.value = -1;
		pose3.value = -1;
		pose4.value = -1;
		talkingName.value = "";
		talkingCharacter.value = -1;
		talkingPose.value = -1;
		dialogueText.value = "";
	}

}
