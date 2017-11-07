using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="AiAttacks/Effect/NMeleeAttack")]
public class NMeleeEffectEffect : AttackEffect {


	public override void Attack(StateController controller, AttackScript attackScript) {

		if (!(controller is NStateController)){
			Debug.LogError("Wrong controller user!");
			return;
		}

		NStateController ncon = (NStateController)controller;

		var effectTransform = Instantiate(attackScript.effect) as Transform;
		ProjectileEffect projectileEffect = effectTransform.GetComponent<ProjectileEffect>();

		effectTransform.position = ncon.thisTransform.position;
		MouseInformation info = new MouseInformation();
		info.position1 = controller.thisTransform.position;
		info.setPosition2(controller.nPlayer.position);
		if (setRotation) {
			float rotation = info.rotationInternal*180/Mathf.PI;
			effectTransform.localRotation = Quaternion.AngleAxis(rotation,Vector3.forward);
		}

		projectileEffect.lifeTime = attackScript.lifeTime;
		projectileEffect.SetMovement(attackScript.speed, info.rotationInternal);

		attackScript.bgui.effectList.Add(projectileEffect);
		

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
