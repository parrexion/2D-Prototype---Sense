using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DAChangePosition : DialogueAction {

	public override bool Act(DialogueScene scene, DialogueJsonItem data) {

		int temp = scene.positions[data.position1];
		scene.positions[data.position1] = scene.positions[data.position2];
		scene.positions[data.position2] = temp;

		return true;
	}
}
