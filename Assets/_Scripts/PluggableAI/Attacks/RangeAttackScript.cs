using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackScript : AttackEffect {


	public override void Attack(StateController controller, AttackScript attackScript){

		Vector2 playerPos = new Vector2(0,0);

		if (controller is NStateController) {
			playerPos = controller.nPlayer.position;
		}
		else if (controller is SStateController) {
			playerPos = controller.sPlayer.position;
		}

		var shotTransform = Instantiate(attackScript.projectile) as Transform;
		shotTransform.position = controller.thisTransform.position;
		attackScript.bgui.effectList.Add(shotTransform.GetComponent<Projectile>());

		MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
		if (move != null) {
			float direction = Mathf.Atan2(
				playerPos.y-controller.thisTransform.position.y,
				playerPos.x-controller.thisTransform.position.x);
			move.setSpeed(new Vector2(0,0), direction);
		}
	}
}
