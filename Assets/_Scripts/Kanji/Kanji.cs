using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Kanji : MonoBehaviour {

	protected WeaponContainer weaponContainer;
	public Transform projectile;
	public Transform effect;

	public Texture2D sprite;
	[HideInInspector] public Texture2D emptySprite;
	[HideInInspector] public Texture2D filledSprite;
	[HideInInspector] public Texture2D chargingSprite;
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

		emptySprite = new Texture2D(1,1);
		emptySprite.SetPixel(0,0,Color.grey);
		emptySprite.Apply();
		filledSprite = new Texture2D(1,1);
		filledSprite.SetPixel(0,0,Color.white);
		filledSprite.Apply();
		chargingSprite = new Texture2D(1,1);
		chargingSprite.SetPixel(0,0,Color.yellow);
		chargingSprite.Apply();

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

//	public virtual void RenderKanji() {
//
//		float size = 64*512/Screen.height;
//
//		//slotName = new Rect(BattleConstants.kanjiXPos+slot*80,BattleConstants.kanjiYPos-16,400,100);
//		slotPos = new Rect(Screen.width*kanji_width+slot*size*1.25f,Screen.height*kanji_height,size,size);
//		slotFilled = new Rect(Screen.width*kanji_width+slot*size*1.25f,Screen.height*kanji_height+size,size,size);
//
//
//		GUI.DrawTexture(slotPos,emptySprite);
//		//GUI.Label(slotName,kanjiName);
//		if (active) {
//			slotFilled.height = size*(float)currentCharge/(float)charge;
//			slotFilled.y = Screen.height*kanji_height+size-slotFilled.height;
//			//Debug.Log("Hieght: "+slotFilled.height);
//			GUI.DrawTexture(slotFilled,chargingSprite);
//		}
//		else {
//			slotFilled.height = size*(float)(cooldown-currentCooldown)/(float)cooldown;
//			slotFilled.y = Screen.height*kanji_height+size-slotFilled.height;
//			//Debug.Log("Hieght: "+slotFilled.height);
//			GUI.DrawTexture(slotFilled,filledSprite);
//		}
//		GUI.DrawTexture(slotPos,sprite);
//	}

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
