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

		if (ncon.moveToPoint == new Vector2(-5*BattleConstants.NormalBorderWidth,-5*BattleConstants.NormalBorderHeight)) {
			float xpos = Random.Range(BattleConstants.NormalStartX-BattleConstants.NormalBorderWidth,BattleConstants.NormalStartX+BattleConstants.NormalBorderWidth);
			float ypos = Random.Range(BattleConstants.NormalStartY-BattleConstants.NormalBorderHeight,BattleConstants.NormalStartY+BattleConstants.NormalBorderHeight);
			ncon.moveToPoint = new Vector2(xpos,ypos);
		}

		ncon.movement = Vector2.MoveTowards(ncon.thisTransform.position,ncon.moveToPoint,ncon.values.speed.x*Time.fixedDeltaTime);

		ncon.movement.Set(
			Mathf.Clamp(ncon.movement.x,BattleConstants.NormalStartX-BattleConstants.NormalBorderWidth,BattleConstants.NormalStartX+BattleConstants.NormalBorderWidth),
			Mathf.Clamp(ncon.movement.y,BattleConstants.NormalStartY-BattleConstants.NormalBorderWidth,BattleConstants.NormalStartY+BattleConstants.NormalBorderWidth));

		ncon.rigidBody.MovePosition(ncon.movement);

		if (ncon.thisTransform.position.x < ncon.moveToPoint.x)
			ncon.moveDirection = 1;
		else
			ncon.moveDirection = -1;
	}
}
