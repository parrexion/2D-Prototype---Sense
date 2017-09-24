using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DARemoveCharacter : DialogueAction {

	public override bool Act (DialogueScene scene, DialogueJsonItem data)
	{
		scene.positions[data.position1] = -1;
		Debug.Log("hfhf");
		return true;
	}
}
