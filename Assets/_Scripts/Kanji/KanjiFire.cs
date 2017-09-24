using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanjiFire : Kanji {

	public float area = 1.0f;
	public float reduction = 1.0f;

	public override bool Activate (MouseInformation info){

		if (!active)
			return false;

		if (!CanActivate(info))
			return false;
		
		var shotTransform = Instantiate(projectile) as Transform;
		shotTransform.position = info.playerPosition;
		shotTransform.GetComponent<Projectile>().SetDamage(PlayerStats.instance.attack.GetValue());
		weaponContainer.projectiles.Add(shotTransform.GetComponent<Projectile>());

		MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
		if (move != null) {
			move.setSpeedFromRotation(info.rotationPlayer);
		}

		base.reduceCharge(reduction);

		return true;
	}


	private bool CanActivate(MouseInformation info){

		if (info.holding || info.holdDuration > 0.5f)
			return false;

		if (!info.clicked)
			return false;
		
		float dist = info.GetInternalDistance();
		//Debug.Log("Dist:  "+dist);
		if (dist > area) {
			return false;
		}

		return true;
	}
}
