using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Kanji/Effect/CreateProjectile")]
public class EffectCreateProjectile : KanjiEffect {

    public override bool Use(WeaponSlot slot, KanjiValues values, MouseInformation info) {
		
		var shotTransform = Instantiate(values.projectile) as Transform;
		shotTransform.position = info.playerPosition;
		shotTransform.GetComponent<Projectile>().SetDamage(PlayerStats.instance.attack.GetValue());
		slot.projectiles.Add(shotTransform.GetComponent<Projectile>());

		MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
		if (move != null) {
			move.setSpeedFromRotation(info.rotationPlayer);
		}

		slot.reduceCharge();

		return true;
    }
}
