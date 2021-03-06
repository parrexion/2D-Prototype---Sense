﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/GiveUpChase")]
public class GiveUpChaseDecision : Decision {


	public override bool Decide(StateController controller) {

		NStateController ncon = (NStateController)controller;

		if (controller.stateTimeElapsed >= ncon.values.chaseTimeLimit) {
			return true;
		}

		return false;
	}
}
