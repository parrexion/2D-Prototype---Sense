using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanjiWind : KanjiBaseClass {

	public float area = 1.0f;
	public float cone = Mathf.PI / 4.0f;

	public override bool Activate (MouseInformation info){

		if (!active)
			return false;

		if (!CanActivate(info))
			return false;

		float rotation = info.rotationInternal*180/Mathf.PI;

		var shotTransform = Instantiate(projectile) as Transform;
		shotTransform.position = new Vector3(info.position1.x+info.distX*0.5f,info.position1.y+info.distY*0.5f,0);
		shotTransform.rotation = Quaternion.AngleAxis(rotation,Vector3.forward);
		Projectile p = shotTransform.GetComponent<Projectile>();
		p.SetDamage(PlayerStats.instance.attack.GetValue());
		weaponContainer.projectiles.Add(p);

		shotTransform = Instantiate(effect) as Transform;
		shotTransform.position = new Vector3(info.position1.x+info.distX*0.5f,info.position1.y+info.distY*0.5f,0);
		shotTransform.rotation = Quaternion.AngleAxis(rotation,Vector3.forward);
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

		float rotation = Mathf.Abs(info.rotationInternal);
		if (rotation < cone)
			return true;

		if (Mathf.PI-cone < rotation && rotation < Mathf.PI+cone) {
			return true;
		}

		return false;
	}
}
