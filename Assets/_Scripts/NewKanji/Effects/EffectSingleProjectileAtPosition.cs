using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Kanji/Effect/Projectile/SingleAtPosition")]
public class EffectSingleProjectileAtPosition : KanjiEffect {

    public override bool Use(KanjiValues values, MouseInformation info) {
		
		var shotTransform = Instantiate(values.projectile) as Transform;
		Projectile projectile = shotTransform.GetComponent<Projectile>();

		if (placeInMiddle) {
			Vector3 pos = new Vector3(info.position1.x+info.distX*0.5f,info.position1.y+info.distY*0.5f,0);
			shotTransform.position = pos;
		}
		else
			shotTransform.position = info.position2;

		if (setRotation) {
			float rotation = info.rotationInternal*180/Mathf.PI;
			shotTransform.localRotation = Quaternion.AngleAxis(rotation,Vector3.forward);
		}

		projectile.lifeTime = values.projectileLifetime;
		projectile.SetDamage(values.damage, PlayerStats.instance.attack.GetValue(), values.baseDamageScale);
		projectile.SetMovement(values.projectileSpeed, info.rotationInternal);

		MainControllerScript.instance.battleGUI.effectList.Add(projectile);

		return true;
    }
}
