using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerKanji : MonoBehaviour {

	public bool active = true;
	public Kanji kanji;

	[HideInInspector] public Rect slotPos;
	[HideInInspector] public Rect slotFilled;

	private float currentCooldown, maxCooldown;
	private float currentCharge;
	private float tickCooldown;

	// Use this for initialization
	public void Initialize (int slot) {
		if (kanji == null){
			return;
		}
		
		currentCharge = kanji.values.maxCharges;
		currentCooldown = maxCooldown = kanji.values.cooldown;
		if (kanji.values.startCooldownTime > 0){
			currentCharge = 0;
			currentCooldown = maxCooldown = kanji.values.startCooldownTime;
		}

		if (currentCharge == 0) {
			active = false;
		}
	}
	
	public void LowerCooldown(float time) {
		if (kanji == null){
			return;
		}
		if (!active) {
			currentCooldown -= time;
			if (currentCooldown <= 0f) {
				active = true;
				currentCooldown = 0f;
				currentCharge = maxCooldown = kanji.values.maxCharges;
			}
		}
	}

	public bool CanActivate (MouseInformation info) {
		if (kanji == null){
			return false;
		}
		if (!active)
			return false;

		if (currentCharge <= 0)
			return false;

		return kanji.CanActivate(info);
	}

	public void CreateEffect(MouseInformation info) {
		kanji.CreateEffects(info);
	}

	public KanjiValues GetValues(){
		return kanji.values;
	}

	/// <summary>
	/// Reduces the amount of charges left in the kanji.
	/// If cooldown is negative then it means there is no rechages.
	/// If cooldown is 0 then no charges are removed.
	/// </summary>
	/// <param name="amount"></param>
	public void reduceCharge(float amount = 1) {
		if (kanji.values.cooldown == 0)
			return;

		currentCharge -= amount;

		if (currentCharge <= 0 && kanji.values.cooldown > 0) {
			active = false;
			currentCooldown = maxCooldown = kanji.values.cooldown;
		}
	}

	public float GetCharge() {
		if (kanji == null){
			return 0;
		}
		if (active)
			return (float)currentCharge/(float)kanji.values.maxCharges;
		else
			return (float)(maxCooldown-currentCooldown)/(float)maxCooldown;
	}

}
