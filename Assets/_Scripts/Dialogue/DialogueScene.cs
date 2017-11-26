using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueScene : MonoBehaviour {

	public IntVariable background;
	[SerializeField] private IntVariable character0;
	[SerializeField] private IntVariable character1;
	[SerializeField] private IntVariable character2;
	[SerializeField] private IntVariable character3;
	[SerializeField] private IntVariable character4;
	[SerializeField] private IntVariable pose0;
	[SerializeField] private IntVariable pose1;
	[SerializeField] private IntVariable pose2;
	[SerializeField] private IntVariable pose3;
	[SerializeField] private IntVariable pose4;
	public StringVariable talkingName;
	public IntVariable talkingCharacter;
	public IntVariable talkingPose;
	public StringVariable dialogueText;

	[HideInInspector] public IntVariable[] characters;
	[HideInInspector] public IntVariable[] poses;


	void Start() {
		characters = new IntVariable[]{ character0, character1, character2, character3, character4 };
		poses = new IntVariable[]{pose0, pose1, pose2, pose3, pose4};

		background.value = 0;
		character0.value = -1;
		character1.value = -1;
		character2.value = -1;
		character3.value = -1;
		character4.value = -1;
		pose0.value = -1;
		pose1.value = -1;
		pose2.value = -1;
		pose3.value = -1;
		pose4.value = -1;
		talkingName.value = "";
		talkingCharacter.value = -1;
		talkingPose.value = -1;
		dialogueText.value = "";
		Debug.Log("Resettttt: " + character0.value);
	}

}


public static class AppHelper {

	public static void Quit() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}
}
