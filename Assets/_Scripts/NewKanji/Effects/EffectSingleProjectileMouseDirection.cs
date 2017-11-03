using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Kanji/Effect/Projectile/SingleMouseDirection")]
public class EffectSingleProjectileMouseDirection : KanjiEffect {

    public override bool Use(KanjiValues values, MouseInformation info) {
		
		var shotTransform = Instantiate(values.projectile) as Transform;
		shotTransform.position = info.playerPosition;
		Projectile projectile = shotTransform.GetComponent<Projectile>();
		MainControllerScript.instance.battleGUI.effectList.Add(projectile);

		projectile.lifeTime = values.projectileLifetime;
		projectile.SetDamage(PlayerStats.instance.attack.GetValue());
		projectile.SetMovement(values.projectileSpeed, info.rotationPlayer);

		return true;
    }
}
