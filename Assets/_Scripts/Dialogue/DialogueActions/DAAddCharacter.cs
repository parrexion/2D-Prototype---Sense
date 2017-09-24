using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DAAddCharacter : DialogueAction {

	public override bool Act (DialogueScene scene, DialogueJsonItem data)
	{

		scene.positions[data.position1] = data.character;
		scene.currentPoses[data.position1] = data.pose;

		return true;
	}
}
