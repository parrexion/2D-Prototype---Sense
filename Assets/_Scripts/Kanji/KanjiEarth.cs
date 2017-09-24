using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanjiEarth : Kanji {

	public float holdDuration = 1.5f;
	public float area = 1.0f;
	public float tickDuration = 0.5f;

//	private bool running = false;
	private float tickCooldown;

	public override bool Activate (MouseInformation info){

		if (!active)
			return false;

		if (!CanActivate(info))
			return false;

//		running = true;
		tickCooldown += Time.deltaTime;

		if (tickCooldown >= tickDuration) {
			tickCooldown -= tickDuration;
			var dust = Instantiate(projectile) as Transform;
			dust.transform.position = info.position1;
			Projectile p = dust.GetComponent<Projectile>();
			p.SetDamage(PlayerStats.instance.attack.GetValue());
			weaponContainer.projectiles.Add(p);

			dust = Instantiate(effect) as Transform;
			dust.transform.position = info.position1;
			weaponContainer.projectileEffects.Add(dust.GetComponent<ProjectileEffect>());

			base.reduceCharge(1f);
		}

		return true;
	}


	private bool CanActivate(MouseInformation info){

		if (info.holdDuration < holdDuration || !info.holding)
			return false;

//		float dist = info.GetInternalDistance();
//		if (dist > area) {
//			if (running) {
//				running = false;
//				base.reduceCharge(1.0f);
//				info.holdDuration = 0;
//			}
//			return false;
//		}

		return true;
	}
}
