using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Flee")]
public class FleeAction : Action {

	public override void Act (StateController controller) {
		Flee(controller);
	}

	private void Flee(StateController controller) {

		NStateController ncon = (NStateController)controller;

		Vector2 direction = ncon.thisTransform.position-ncon.nPlayer.position;
		ncon.movement = new Vector2(ncon.thisTransform.position.x,ncon.thisTransform.position.y) 
			+ (new Vector2(direction.normalized.x * ncon.values.speed.x,
							direction.normalized.y * ncon.values.speed.y)*Time.fixedDeltaTime);

		ncon.movement.Set(
			Mathf.Clamp(ncon.movement.x,Constants.NormalStartX-Constants.NormalBorderWidth,Constants.NormalStartX+Constants.NormalBorderWidth),
			Mathf.Clamp(ncon.movement.y,Constants.NormalStartY-Constants.NormalBorderWidth,Constants.NormalStartY+Constants.NormalBorderWidth));

		ncon.rigidBody.MovePosition(ncon.movement);

		if (ncon.thisTransform.position.x < ncon.nPlayer.position.x)
			ncon.moveDirection = -1;
		else
			ncon.moveDirection = 1;
	}
}
