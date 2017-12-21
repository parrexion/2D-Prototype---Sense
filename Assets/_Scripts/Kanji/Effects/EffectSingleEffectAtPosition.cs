using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Kanji/Effect/Effect/SingleAtPosition")]
public class EffectSingleEffectAtPosition : KanjiEffect {

    public override bool Use(KanjiValues values, int attackValue, MouseInformation info) {
		
		var effectTransform = Instantiate(values.effect) as Transform;
		ProjectileEffect effect = effectTransform.GetComponent<ProjectileEffect>();
		MainControllerScript.instance.battleGUI.effectList.Add(effect);

		if (placeInMiddle)
			effectTransform.position = new Vector3(info.position1.x+info.distX*0.5f,info.position1.y+info.distY*0.5f,0);
		else
			effectTransform.position = info.position2;

		if (setRotation) {
			float rotation = info.rotationInternal*180/Mathf.PI;
			effectTransform.localRotation = Quaternion.AngleAxis(rotation,Vector3.forward);
		}

		effect.lifeTime = values.projectileLifetime;
		effect.SetMovement(values.projectileSpeed, info.rotationInternal);

		return true;
    }
}
