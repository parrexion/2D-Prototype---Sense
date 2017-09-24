using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NStateController : StateController {

	//Moving
	[HideInInspector] public int moveDirection;

	//Chasing
	[HideInInspector] public Vector2 movement;
	//

	//Move to point
	[HideInInspector] public Vector2 moveToPoint = new Vector2(-5*BattleConstants.NormalBorderWidth,-5*BattleConstants.NormalBorderHeight);
	//


	/// /////////////////////////////////////////////////////


	override protected void OnExitState() {
		moveToPoint = new Vector2(-5*BattleConstants.NormalBorderWidth,-5*BattleConstants.NormalBorderHeight);
	}

	override protected void UpdateAnimation() {

		animInfo.attacking = false;
		animInfo.chasing = false;
		animInfo.blocking = false;
		animInfo.hurt = false;
		animInfo.jumping = false;
		animInfo.mouseDirection = 0;

		switch (currentState.stateString.ToString()) 
		{
		case "Idle":
			break;
		case "WalkLeft":
			animInfo.mouseDirection = moveDirection;
			break;
		case "Chase":
			animInfo.chasing = true;
			animInfo.mouseDirection = moveDirection;
			break;
		case "Attack":
			animInfo.attacking = true;
			animInfo.mouseDirection = moveDirection;
			break;
		default:
			Debug.Log(currentState.stateString.ToString());
			break;
		}

		animScript.UpdateState(animInfo);
	}

	public override Vector3 GetRandomLocation() {
		Vector2 dist = Random.rotation * new Vector2(3,0);
		return new Vector3(BattleConstants.NormalStartX + dist.x, BattleConstants.NormalStartY + dist.y,0);
	}
}
