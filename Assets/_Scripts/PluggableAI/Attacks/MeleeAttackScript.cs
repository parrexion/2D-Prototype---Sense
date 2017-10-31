using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackScript : AttackScript {


	public override void Attack(StateController controller) {

		if (controller is NStateController) {

			NStateController ncon = (NStateController)controller;

			var shotTransform = Instantiate(projectile) as Transform;
			shotTransform.position = ncon.thisTransform.position;
			bgui.effectList.Add(shotTransform.GetComponent<Projectile>());

			shotTransform = Instantiate(effect) as Transform;
			shotTransform.position = ncon.thisTransform.position;
			bgui.effectList.Add(shotTransform.GetComponent<ProjectileEffect>());
		}
		else if (controller is SStateController) {

			SStateController scon = (SStateController)controller;

			var shotTransform = Instantiate(projectile) as Transform;
			shotTransform.position = scon.thisTransform.position;
			bgui.effectList.Add(shotTransform.GetComponent<Projectile>());

			shotTransform = Instantiate(effect) as Transform;
			shotTransform.position = scon.thisTransform.position;
			bgui.effectList.Add(shotTransform.GetComponent<ProjectileEffect>());
		}
	}
}
