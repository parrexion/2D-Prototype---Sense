﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/MoveWithinBounds")]
public class MoveWithinBounds : Action {

	public override void Act (StateController controller)
	{
		Move(controller);
	}

	private void Move(StateController controller) {

		NStateController ncon = (NStateController)controller;

		ncon.movement.Set(
			Mathf.Clamp(ncon.thisTransform.position.x,Constants.NormalStartX-Constants.NormalBorderWidth,Constants.NormalStartX+Constants.NormalBorderWidth),
			Mathf.Clamp(ncon.thisTransform.position.y,Constants.NormalStartY-Constants.NormalBorderWidth,Constants.NormalStartY+Constants.NormalBorderWidth));

		ncon.rigidBody.MovePosition(ncon.movement);
	}
}
