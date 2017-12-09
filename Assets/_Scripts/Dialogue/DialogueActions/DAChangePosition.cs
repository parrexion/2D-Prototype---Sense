using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DAChangePosition : DialogueAction {

	public override bool Act(DialogueScene scene, DialogueJsonItem data) {

		CharacterEntry temp = scene.characters[data.position1];
		scene.characters[data.position1] = scene.characters[data.position2];
		scene.characters[data.position2] = temp;

		return true;
	}
}
