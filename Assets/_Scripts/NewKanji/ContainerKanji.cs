using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerKanji : MonoBehaviour {

	public bool active = true;
	public Kanji kanji;

	[HideInInspector] public Rect slotPos;
	[HideInInspector] public Rect slotFilled;

	private float currentCooldown;
	private float currentCharge;

	// Use this for initialization
	public void Initialize (int slot) {
		currentCharge = kanji.values.maxCharges;
		currentCooldown = kanji.values.cooldown;

		if (currentCharge == 0) {
			active = false;
		}
	}
	
	// Update is called once per frame
	public void Update () {
		if (!active) {
			currentCooldown -= Time.deltaTime;
			if (currentCooldown <= 0f) {
				active = true;
				currentCooldown = 0f;
				currentCharge = kanji.values.maxCharges;
			}
		}
	}

	public bool CanActivate (MouseInformation info) {

		if (!active)
			return false;

		return kanji.CanActivate(info);
	}

	public void CreateEffect(MouseInformation info) {
		kanji.CreateEffects(info);
	}

	public KanjiValues GetValues(){
		return kanji.values;
	}

	public void reduceCharge(float amount = 1) {
		currentCharge -= amount;

		if (currentCharge <= 0) {
			active = false;
			currentCooldown = kanji.values.cooldown;
		}
	}

	public float GetCharge() {
		if (active)
			return (float)currentCharge/(float)kanji.values.maxCharges;
		else
			return (float)(kanji.values.cooldown-currentCooldown)/(float)kanji.values.cooldown;
	}

}
