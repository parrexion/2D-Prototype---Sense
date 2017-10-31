using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanjiWater : KanjiBaseClass {

	public float area = 1.0f;
	private float coneL = Mathf.PI / 4.0f;
	private float coneR = Mathf.PI*3.0f / 4.0f;

	public override bool Activate (MouseInformation info){

		if (!active)
			return false;

		if (!CanActivate(info))
			return false;

		var shotTransform = Instantiate(projectile) as Transform;
		shotTransform.position = new Vector3(info.position1.x,info.position1.y+shotTransform.localScale.y*0.25f,0);
		Projectile p = shotTransform.GetComponent<Projectile>();
		p.SetDamage(PlayerStats.instance.attack.GetValue());
		weaponContainer.projectiles.Add(p);

		shotTransform = Instantiate(effect) as Transform;
		shotTransform.position = new Vector3(info.position1.x,info.position1.y+shotTransform.localScale.y*0.25f,0);
		weaponContainer.projectileEffects.Add(shotTransform.GetComponent<ProjectileEffect>());

		base.reduceCharge(1.0f);

		return true;
	}


	private bool CanActivate(MouseInformation info){

		if (info.holding || info.holdDuration > 0.5f)
			return false;

		if (!info.clicked)
			return false;

		float dist = info.GetInternalDistance();
		if (dist < area) {
			return false;
		}

		float rotation = info.rotationInternal;

		if (coneL < rotation && rotation < coneR) {
			return true;
		}

		return false;
	}
}
