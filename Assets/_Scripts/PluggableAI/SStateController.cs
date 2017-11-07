using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SStateController : StateController {

	public bool leftSide = false;
	public int lastTime = 0;

	/// /////////////////////////////////////////////////////

	override protected void OnExitState() {

	}

	override protected void UpdateAnimation() {
		animInfo.attacking = false;
		animInfo.chasing = false;
		animInfo.blocking = false;
		animInfo.hurt = false;
		animInfo.jumping = false;
		if (leftSide)
			animInfo.mouseDirection = 1;
		else
			animInfo.mouseDirection = -1;

		switch (currentState.stateString.ToString()) 
		{
		case "Idle":
//			Debug.Log("IDLE");
			if (lastTime != animInfo.mouseDirection){
				lastTime = animInfo.mouseDirection;
			}
			else
				animInfo.mouseDirection = 0;
			break;
		case "WalkLeft":
//			Debug.Log("WLAK");
			break;
		case "Attack":
//			Debug.Log("Attack");
			animInfo.attacking = true;
			break;
		default:
			Debug.Log(currentState.stateString.ToString());
			break;
		}

		animScript.UpdateState(animInfo);
	}

	public override Vector3 GetRandomLocation(){
		leftSide = !leftSide;
		float xpos = 0f;
		float ypos = 0f;
		if (leftSide)
			xpos = Random.Range(BattleConstants.SpiritStartX-4f,BattleConstants.SpiritStartX-2f);
		else
			xpos = Random.Range(BattleConstants.SpiritStartX+2f,BattleConstants.SpiritStartX+4f);
		ypos = Random.Range(BattleConstants.SpiritStartY-2f,BattleConstants.SpiritStartY);
		return new Vector3(xpos,ypos,0);
	}
}
