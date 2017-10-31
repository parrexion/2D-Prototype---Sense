using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Kanji/Effect/SingleProjectile")]
public class EffectSingleProjectile : KanjiEffect {

    public override bool Use(KanjiValues values, MouseInformation info) {
		
		var shotTransform = Instantiate(values.projectile) as Transform;
		shotTransform.position = info.playerPosition;
		Projectile projectile = shotTransform.GetComponent<Projectile>();
		projectile.SetDamage(PlayerStats.instance.attack.GetValue());
		MainControllerScript.instance.battleGUI.effectList.Add(projectile);

		MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
		if (move != null) {
			move.setSpeedFromRotation(info.rotationPlayer);
		}

		return true;
    }
}
