using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour {

	public bool active = true;
	public Kanji kanji = null;
	
	public List<Projectile> projectiles;
	public List<ProjectileEffect> projectileEffects;

	private float currentCharge = 0;
	// private float currentCooldown = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void reduceCharge(float amount = 1f) {
		currentCharge -= amount;

		if (currentCharge <= 0) {
			active = false;
			// currentCooldown = kanji.values.cooldown;
		}
	}
}
