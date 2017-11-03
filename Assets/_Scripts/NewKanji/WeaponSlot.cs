using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour {

	public bool active = true;
	private bool showKanji = true;
	public ContainerKanji[] kanji;

	private bool attacking = false;

	private float size;
	private float kanjiHeight;
	private Texture2D emptySprite;
	private Texture2D filledSprite;
	private Texture2D chargingSprite;

	private float shootCooldown;


	void Start () {
		shootCooldown = 0f;
		size = BattleConstants.kanjiSize*Screen.height/BattleConstants.screenHeight;
		
		SetupTextures();
		StartCoroutine(SetEquippedKanji());
	}

	void SetupTextures(){
		emptySprite = new Texture2D(1,1);
		emptySprite.SetPixel(0,0,Color.grey);
		emptySprite.Apply();
		filledSprite = new Texture2D(1,1);
		filledSprite.SetPixel(0,0,Color.white);
		filledSprite.Apply();
		chargingSprite = new Texture2D(1,1);
		chargingSprite.SetPixel(0,0,Color.yellow);
		chargingSprite.Apply();
	}

	void Update() {

		if (!active)
			return;

		if (shootCooldown > 0) {
			shootCooldown -= Time.deltaTime;
		}

		for (int i = 0; i < 4; i++) {
			kanji[i].Update();
		}
	}

	IEnumerator SetEquippedKanji(){
		int[] kanjiIndex;
		MainControllerScript mainController = MainControllerScript.instance;

		while(mainController.storyValues.initiated == false)
			yield return null;

		// kanjiIndex = mainController.storyValues.GetEquippedKanji();
		kanjiIndex = new int[]{0,1,2,3};

		float width = BattleConstants.kanjiGuiOffsetWidth;
		kanjiHeight = BattleConstants.kanjiGuiOffsetHeight;
		for (int i = 0; i < 4; i++) {
			if (kanjiIndex.Length <= i) {
				kanji[i].kanji = mainController.kanjiList.GetKanji(0);
			}
			else {
				Debug.Log("Equipped: "+kanjiIndex[i]);
				kanji[i].kanji = mainController.kanjiList.GetKanji(kanjiIndex[i]);
			}
			kanji[i].Initialize(i);

			//slotName = new Rect(BattleConstants.kanjiXPos+slot*80,BattleConstants.kanjiYPos-16,400,100);
			kanji[i].slotPos = new Rect(Screen.width*width+i*size*1.25f,Screen.height*kanjiHeight,size,size);
			kanji[i].slotFilled = new Rect(Screen.width*width+i*size*1.25f,Screen.height*kanjiHeight+size,size,size);
		}
	}

	void OnGUI(){
		if (!showKanji || !active)
			return;

		for (int i = 0; i < 4; i++) {

			if (kanji[i].GetValues().icon == null)
				continue; //TODO actually make an empty slot sprite

			GUI.DrawTexture(kanji[i].slotPos,emptySprite);
			if (kanji[i].active) {
				kanji[i].slotFilled.height = size*kanji[i].GetCharge();
				kanji[i].slotFilled.y = Screen.height*kanjiHeight+size-kanji[i].slotFilled.height;
				GUI.DrawTexture(kanji[i].slotFilled,chargingSprite);
			}
			else {
				kanji[i].slotFilled.height = size*kanji[i].GetCharge();
				kanji[i].slotFilled.y = Screen.height*kanjiHeight+size-kanji[i].slotFilled.height;
				GUI.DrawTexture(kanji[i].slotFilled,filledSprite);
			}
			GUI.DrawTexture(kanji[i].slotPos,kanji[i].GetValues().icon);
		}
	}

	public bool IsAttacking(){
		bool IsAttacking = attacking;
		if (IsAttacking)
			attacking = false;
		return IsAttacking;
	}

	public void SetVisible(bool state) {
		showKanji = state;
	}

	public bool Activate(MouseInformation mouseInfo) {
		if (shootCooldown > 0f){
			return false;
		}

		for (int i = 0; i < 4; i++) {
			if (kanji[i].CanActivate(mouseInfo)) {
				shootCooldown = kanji[i].GetValues().delay;
				kanji[i].reduceCharge();
				kanji[i].CreateEffect(mouseInfo);
				return true;
			}
		}

		return false;
	}
}
