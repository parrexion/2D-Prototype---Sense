using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueScene : MonoBehaviour {

	public int background = 0;
	public Character[] characters = new Character[4];
	public int[] positions = new int[4];
	public int[] currentPoses = new int[4];
	public string characterName = "";
	public string dialogue = "";
	public Character closeup;
	public int talkingCharacter = -1;
	public int talkingPose = -1;


	void Start() {
		for (int i = 0; i < positions.Length; i++) {
			positions[i] = -1;
		}
	}
}


public static class AppHelper {

	public static void Quit() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}
}
