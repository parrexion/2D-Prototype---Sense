using System.Collections;
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
			Mathf.Clamp(ncon.thisTransform.position.x,BattleConstants.NormalStartX-BattleConstants.NormalBorderWidth,BattleConstants.NormalStartX+BattleConstants.NormalBorderWidth),
			Mathf.Clamp(ncon.thisTransform.position.y,BattleConstants.NormalStartY-BattleConstants.NormalBorderWidth,BattleConstants.NormalStartY+BattleConstants.NormalBorderWidth));

		ncon.rigidBody.MovePosition(ncon.movement);
	}
}
