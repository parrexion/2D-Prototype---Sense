using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AiAttacks/Effect/NRangeAttack")]
public class NRangeAttackEffect : AttackEffect {


	public override void Attack(StateController controller, AttackScript attackScript){

		if (!(controller is NStateController)) {
			Debug.LogError("Wrong controller user!");
			return;
		}

		Vector2 playerPos = controller.sPlayer.position;
		var shotTransform = Instantiate(attackScript.projectile) as Transform;
		Projectile projectile = shotTransform.GetComponent<Projectile>();

		shotTransform.position = controller.thisTransform.position;
		MouseInformation info = new MouseInformation();
		info.position1 = controller.thisTransform.position;
		info.position2 = controller.nPlayer.position;
		if (setRotation) {
			float rotation = info.rotationInternal * 180 / Mathf.PI;
			shotTransform.localRotation = Quaternion.AngleAxis(rotation, Vector3.forward);
		}

		projectile.lifeTime = attackScript.lifeTime;
		projectile.SetDamage(attackScript.damage, 0, 1);
		projectile.SetMovement(attackScript.speed, info.rotationInternal);

		attackScript.bgui.effectList.Add(projectile);
	}
}
