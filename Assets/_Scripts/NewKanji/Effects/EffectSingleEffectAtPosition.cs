using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Kanji/Effect/Effect/SingleAtPosition")]
public class EffectSingleEffectAtPosition : KanjiEffect {

	[SerializeField]
	private bool positionInMiddle = true;

    public override bool Use(KanjiValues values, MouseInformation info) {
		
		var effectTransform = Instantiate(values.effect) as Transform;
		effectTransform.position = info.playerPosition;
		ProjectileEffect effect = effectTransform.GetComponent<ProjectileEffect>();
		MainControllerScript.instance.battleGUI.effectList.Add(effect);

		if (positionInMiddle)
			effect.transform.position = new Vector3(info.position1.x+info.distX*0.5f,info.position1.y+info.distY*0.5f,0);
		else
			effect.transform.position = info.position2;

		float rotation = info.rotationInternal*180/Mathf.PI;
		effect.transform.localRotation = Quaternion.AngleAxis(rotation,Vector3.forward);

		effect.lifeTime = values.projectileLifetime;
		effect.SetMovement(values.projectileSpeed, info.rotationInternal);

		return true;
    }
}
