using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContainer : MonoBehaviour {

	private bool active = true;
	private bool showKanji = true;
	public Kanji[] equipped;
	public List<Projectile> projectiles;
	public List<ProjectileEffect> projectileEffects;

	private float shootCooldown;

	private float size;
	public float kanji_width = 0.1f;
	public float kanji_height = 0.1f;


	void Start () {

		SetEquippedKanji();

		shootCooldown = 0f;

		size = 64*Screen.height/512;
		for (int i = 0; i < 4; i++) {
			equipped[i].Initialize(this, i);

			//slotName = new Rect(BattleConstants.kanjiXPos+slot*80,BattleConstants.kanjiYPos-16,400,100);
			equipped[i].slotPos = new Rect(Screen.width*kanji_width+i*size*1.25f,Screen.height*kanji_height,size,size);
			equipped[i].slotFilled = new Rect(Screen.width*kanji_width+i*size*1.25f,Screen.height*kanji_height+size,size,size);
		}
	}

	void Update() {

		if (!active)
			return;

		projectiles.RemoveAll(item => (item == null));
		projectileEffects.RemoveAll(item => (item == null));

		if (shootCooldown > 0) {
			shootCooldown -= Time.deltaTime;
		}

		for (int i = 0; i < 4; i++) {
			equipped[i].Update();
		}
	}

	public void SetEquippedKanji(){
		int[] kanjiIndex;
		kanjiIndex = MainControllerScript.instance.storyValues.GetEquippedKanji();

		equipped = new Kanji[kanjiIndex.Length];
		for (int i = 0; i < kanjiIndex.Length; i++) {
			Debug.Log("Equipped: "+kanjiIndex[i]);
			equipped[i] = MainControllerScript.instance.kanjiList.kanjiList[kanjiIndex[i]];
		}
	}

//	public void RenderKanji(){
//
//		if (!showKanji || !active)
//			return;
//
//		for (int i = 0; i < 4; i++) {
//			equipped[i].RenderKanji();
//		}
//	}

	void OnGUI(){
		//RenderKanji();
		if (!showKanji || !active)
			return;

		for (int i = 0; i < 4; i++) {

			if (equipped[i].sprite == null)
				continue; //TODO actually make an empty slot sprite

			GUI.DrawTexture(equipped[i].slotPos,equipped[i].emptySprite);
			//GUI.Label(slotName,kanjiName);
			if (equipped[i].active) {
				equipped[i].slotFilled.height = size*equipped[i].GetCharge();
				equipped[i].slotFilled.y = Screen.height*kanji_height+size-equipped[i].slotFilled.height;
//				Debug.Log("Hieght: "+equipped[i].slotFilled.height);
				GUI.DrawTexture(equipped[i].slotFilled,equipped[i].chargingSprite);
			}
			else {
				equipped[i].slotFilled.height = size*equipped[i].GetCharge();
				equipped[i].slotFilled.y = Screen.height*kanji_height+size-equipped[i].slotFilled.height;
//				Debug.Log("Hieght: "+equipped[i].GetCharge());
				GUI.DrawTexture(equipped[i].slotFilled,equipped[i].filledSprite);
			}
			GUI.DrawTexture(equipped[i].slotPos,equipped[i].sprite);
		}
	}

	public void SetVisible(bool state) {
		showKanji = state;
		SetActive(state);
	}

	public void SetActive(bool state) {
		active = state;
		foreach (Projectile p in projectiles) {
			if (p != null) {
				p.SetActive(state);
			}
			else {
				Debug.Log("So null");
			}
		}
		foreach (ProjectileEffect pe in projectileEffects) {
			if (pe != null) {
				pe.active = state;
			}
			else {
				Debug.Log("So null");
			}
		}
	}

	public bool Activate(MouseInformation mouseInfo) {
		if (shootCooldown > 0f)
			return false;

		for (int i = 0; i < 4; i++) {
			if (equipped[i].Activate(mouseInfo)) {
				shootCooldown = equipped[i].delay;
				return true;
			}
		}

		return false;
	}
}
