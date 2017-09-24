using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/HitPlayer")]
public class HitPlayerDecision : Decision {

	public override bool Decide(StateController controller) {

		float dist = Vector2.Distance(controller.nPlayer.position,controller.thisTransform.position);
		if (dist <= 0.75f) {
//			Debug.Log("Hit the player");
			return true;
		}

		return false;
	}
}
