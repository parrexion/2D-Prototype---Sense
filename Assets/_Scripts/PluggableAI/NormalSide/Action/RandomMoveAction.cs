using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/RandomMove")]
public class RandomMoveAction : Action {

	public override void Act (StateController controller)
	{
		Move(controller);
	}

	private void Move(StateController controller) {

		NStateController ncon = (NStateController)controller;

		if (ncon.moveToPoint == new Vector2(-5*Constants.NormalBorderWidth,-5*Constants.NormalBorderHeight)) {
			float xpos = Random.Range(Constants.NormalStartX-Constants.NormalBorderWidth,Constants.NormalStartX+Constants.NormalBorderWidth);
			float ypos = Random.Range(Constants.NormalStartY-Constants.NormalBorderHeight,Constants.NormalStartY+Constants.NormalBorderHeight);
			ncon.moveToPoint = new Vector2(xpos,ypos);
		}

		ncon.movement = Vector2.MoveTowards(ncon.thisTransform.position,ncon.moveToPoint,ncon.values.speed.x*Time.fixedDeltaTime);

		ncon.movement.Set(
			Mathf.Clamp(ncon.movement.x,Constants.NormalStartX-Constants.NormalBorderWidth,Constants.NormalStartX+Constants.NormalBorderWidth),
			Mathf.Clamp(ncon.movement.y,Constants.NormalStartY-Constants.NormalBorderWidth,Constants.NormalStartY+Constants.NormalBorderWidth));

		ncon.rigidBody.MovePosition(ncon.movement);

		if (ncon.thisTransform.position.x < ncon.moveToPoint.x)
			ncon.moveDirection = 1;
		else
			ncon.moveDirection = -1;
	}
}
