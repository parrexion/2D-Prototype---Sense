﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/FinishedDecision")]
public class FinishedFleeingDecision : Decision {

	public override bool Decide(StateController controller) {

		NStateController ncon = (NStateController)controller;

		if (controller.stateTimeElapsed >= ncon.values.fleeTimeLimit) {
			return true;
		}

		return false;
	}
}
