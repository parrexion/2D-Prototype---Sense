using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DAChangeTalking : DialogueAction {

	public override bool Act(DialogueScene scene, DialogueJsonItem data) {

		scene.talkingCharacter.value = data.position1;
		scene.talkingPose.value = data.position2;

		return true;
	}
}
