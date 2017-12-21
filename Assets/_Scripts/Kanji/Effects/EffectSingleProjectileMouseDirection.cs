using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Kanji/Effect/Projectile/SingleMouseDirection")]
public class EffectSingleProjectileMouseDirection : KanjiEffect {

    public override bool Use(KanjiValues values, int attackValue, MouseInformation info) {
		
		var shotTransform = Instantiate(values.projectile) as Transform;
		Projectile projectile = shotTransform.GetComponent<Projectile>();
		MainControllerScript.instance.battleGUI.effectList.Add(projectile);

		shotTransform.position = info.playerPosition;
		if (setRotation) {
			float rotation = info.rotationPlayer*180/Mathf.PI;
			shotTransform.localRotation = Quaternion.AngleAxis(rotation,Vector3.forward);
		}

		projectile.lifeTime = values.projectileLifetime;
		projectile.SetDamage(values.damage, attackValue, values.baseDamageScale);
		projectile.SetMovement(values.projectileSpeed, info.rotationPlayer);

		return true;
    }
}
