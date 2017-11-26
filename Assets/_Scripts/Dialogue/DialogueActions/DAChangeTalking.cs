using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DAChangeTalking : DialogueAction {

	public override bool Act(DialogueScene scene, DialogueJsonItem data) {

		scene.talkingCharacter.value = data.character;
		scene.talkingPose.value = data.pose;

		return true;
	}
}
