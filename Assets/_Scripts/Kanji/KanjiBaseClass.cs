using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KanjiBaseClass : MonoBehaviour {

	protected WeaponContainer weaponContainer;
	public Transform projectile;
	public Transform effect;

	public Texture2D sprite;
	[HideInInspector] public Rect slotPos;
	[HideInInspector] public Rect slotFilled;
//	private Rect slotName;

	[HideInInspector] public string kanjiName = "";
	public bool active = true;
	public float startCooldownPercent = 0f;
	public float charge = 10f;
	public float delay = 0.1f;
	public float cooldown = 5.0f;
	public int damage = 10;

	private float currentCooldown;
	private float currentCharge;

	// Use this for initialization
	public void Initialize (WeaponContainer weapon, int slot) {

		weaponContainer = weapon;
		kanjiName = LocalizationManager.instance.GetLocalizedValue(kanjiName);

		currentCharge = charge * (1f - startCooldownPercent);
		currentCooldown = cooldown * startCooldownPercent;
		if (currentCharge == 0) {
			active = false;
		}


		//		slotName = new Rect(BattleConstants.kanjiXPos+slot*80,BattleConstants.kanjiYPos-16,400,100);
	}
	
	// Update is called once per frame
	public void Update () {
		if (!active) {
			currentCooldown -= Time.deltaTime;
			if (currentCooldown <= 0f) {
				active = true;
				currentCooldown = 0f;
				currentCharge = charge;
			}
		}
	}

	abstract public bool Activate (MouseInformation info);


	protected void reduceCharge(float amount) {
		currentCharge -= amount;

		if (currentCharge <= 0) {
			active = false;
			currentCooldown = cooldown;
		}
	}

	public float GetCharge() {
		if (active)
			return (float)currentCharge/(float)charge;
		else
			return (float)(cooldown-currentCooldown)/(float)cooldown;
	}
}
