using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DASetText : DialogueAction {

	public override bool Act(DialogueScene scene, DialogueJsonItem data) {

		scene.dialogue = data.text;
//		Debug.Log("Text:  "+data.text);

		return true;
	}
}
