using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Kanji/Effect/Projectile/SingleAtPosition")]
public class EffectSingleProjectileAtPosition : KanjiEffect {

	[SerializeField]
	private bool positionInMiddle = true;

    public override bool Use(KanjiValues values, MouseInformation info) {
		
		var shotTransform = Instantiate(values.effect) as Transform;
		shotTransform.position = info.playerPosition;
		Projectile projectile = shotTransform.GetComponent<Projectile>();
if (!projectile)
	Debug.Log("asdasdasdsa");
		if (positionInMiddle) {
			Vector3 pos = new Vector3(info.position1.x+info.distX*0.5f,info.position1.y+info.distY*0.5f,0);
			projectile.transform.position = pos;
		}
		else
			projectile.transform.position = info.position2;

		float rotation = info.rotationInternal*180/Mathf.PI;
		projectile.transform.localRotation = Quaternion.AngleAxis(rotation,Vector3.forward);

		projectile.lifeTime = values.projectileLifetime;
		projectile.SetDamage(values.damage);
		projectile.SetMovement(values.projectileSpeed, info.rotationInternal);

		MainControllerScript.instance.battleGUI.effectList.Add(projectile);

		return true;
    }
}
