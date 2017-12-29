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
		info.setPosition2(controller.nPlayer.position);
		if (setRotation) {
			float rotation = info.rotationInternal*180/Mathf.PI;
			shotTransform.localRotation = Quaternion.AngleAxis(rotation,Vector3.forward);
		}

		projectile.isEnemy = true;
		projectile.multiHit = attackScript.multihit;
		projectile.lifeTime = attackScript.lifeTime;
		projectile.SetDamage(attackScript.damage, 0, 1);
		projectile.SetMovement(attackScript.speed, info.rotationInternal);

		attackScript.bgui.effectList.Add(projectile);
	}
}
