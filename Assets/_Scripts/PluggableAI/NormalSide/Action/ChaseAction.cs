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

		float speed = ncon.values.speed.x*Time.fixedDeltaTime;// * ncon.moveDirection;
		if (ncon.canBeSlowed.value && !ncon.slowLeftSide.value) {
			speed *= ncon.slowAmount.value;
		}
		
		ncon.movement = Vector2.MoveTowards(ncon.thisTransform.position,ncon.nPlayer.position,speed);

		ncon.movement.Set(
			Mathf.Clamp(ncon.movement.x,Constants.NormalStartX-Constants.NormalBorderWidth,Constants.NormalStartX+Constants.NormalBorderWidth),
			Mathf.Clamp(ncon.movement.y,Constants.NormalStartY-Constants.NormalBorderWidth,Constants.NormalStartY+Constants.NormalBorderWidth));

		ncon.rigidBody.MovePosition(ncon.movement);


		if (ncon.thisTransform.position.x < ncon.nPlayer.position.x)
			ncon.moveDirection = 1;
		else
			ncon.moveDirection = -1;
	}
}
