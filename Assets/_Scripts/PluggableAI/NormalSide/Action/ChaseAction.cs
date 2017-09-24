using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : Action {

	public override void Act (StateController controller)
	{
		Chase(controller);
	}

	private void Chase(StateController controller) {

		NStateController ncon = (NStateController)controller;

		ncon.movement = Vector2.MoveTowards(ncon.thisTransform.position,ncon.nPlayer.position,ncon.values.speed.x*Time.fixedDeltaTime);

		ncon.movement.Set(
			Mathf.Clamp(ncon.movement.x,BattleConstants.NormalStartX-BattleConstants.NormalBorderWidth,BattleConstants.NormalStartX+BattleConstants.NormalBorderWidth),
			Mathf.Clamp(ncon.movement.y,BattleConstants.NormalStartY-BattleConstants.NormalBorderWidth,BattleConstants.NormalStartY+BattleConstants.NormalBorderWidth));

		ncon.rigidBody.MovePosition(ncon.movement);


		if (ncon.thisTransform.position.x < ncon.nPlayer.position.x)
			ncon.moveDirection = 1;
		else
			ncon.moveDirection = -1;
	}
}
