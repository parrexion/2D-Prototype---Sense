using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KanjiValues {

	[Header("Projectile")]
	public Transform projectile;
	public Transform effect;

	[Space(10)]

	[Header("Kanji values")]
	public string kanjiName = "";
	public float startCooldownPercent = 0f;
	public float charge = 10f;
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
