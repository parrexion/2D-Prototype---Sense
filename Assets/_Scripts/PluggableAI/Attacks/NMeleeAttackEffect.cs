using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="AiAttacks/Projectile/NMeleeAttack")]
public class NMeleeAttackEffect : AttackEffect {


	public override void Attack(StateController controller, AttackScript attackScript) {

		if (!(controller is NStateController)){
			Debug.LogError("Wrong controller user!");
			return;
		}

		NStateController ncon = (NStateController)controller;

		var shotTransform = Instantiate(attackScript.projectile) as Transform;
		Projectile projectile = shotTransform.GetComponent<Projectile>();

		shotTransform.position = ncon.thisTransform.position;
		MouseInformation info = new MouseInformation();
		info.position1 = controller.thisTransform.position;
		info.position2 = controller.nPlayer.position;
		if (setRotation) {
			float rotation = info.rotationInternal*180/Mathf.PI;
			shotTransform.localRotation = Quaternion.AngleAxis(rotation,Vector3.forward);
		}

		projectile.lifeTime = attackScript.lifeTime;
		projectile.SetDamage(attackScript.damage, 0, 1);
		projectile.SetMovement(attackScript.speed, info.rotationInternal);

		attackScript.bgui.effectList.Add(projectile);

		// else if (controller is SStateController) {

		// 	SStateController scon = (SStateController)controller;

		// 	var shotTransform = Instantiate(projectile) as Transform;
		// 	shotTransform.position = scon.thisTransform.position;
		// 	bgui.effectList.Add(shotTransform.GetComponent<Projectile>());

		// 	shotTransform = Instantiate(effect) as Transform;
		// 	shotTransform.position = scon.thisTransform.position;
		// 	bgui.effectList.Add(shotTransform.GetComponent<ProjectileEffect>());
		// }
	}
}
