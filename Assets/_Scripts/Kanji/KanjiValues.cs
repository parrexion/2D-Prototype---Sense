using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KanjiValues {

	public enum KanjiType {
		CLICK,SLASH,RISE,HOLD,DOWN,OTHER
	}

	[Header("GUI graphics")]
	public string kanjiName = "";
	public Sprite icon;
	public KanjiType kanjiType;

	[Space(10)]

	[Header("Projectile")]
	public Transform projectile;
	public Transform effect;
	public Vector2 projectileSpeed;
	public float projectileLifetime;

	[Space(10)]

	[Header("Kanji values")]
	public float startCooldownTime = 0f;
	public int maxCharges = 10;
	public float delay = 0.1f;
	public float cooldown = 5.0f;
	public int damage = 10;
	public float baseDamageScale = 1.0f;

	[Space(10)]

	[Header("Activation requirements")]
	public float area = 1.0f; 
	public float holdMin = -1f;
	public float holdMax = 0.5f;
}
