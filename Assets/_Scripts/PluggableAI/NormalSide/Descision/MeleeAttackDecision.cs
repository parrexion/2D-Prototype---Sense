using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/MeleeAttack")]
public class MeleeAttackDecision : Decision {

	public override bool Decide(StateController controller) {

		NStateController ncon = (NStateController)controller;
		float dist = Vector2.Distance(controller.nPlayer.position,controller.thisTransform.position);
		if (dist <= ncon.values.meleeRange) {
			ncon.hasAttacked = false;
			return true;
		}

		return false;
	}
}
