using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour {

	[Header("Game speed")]
	public BoolVariable paused;
	public BoolVariable canBeSlowed;
	public BoolVariable slowLeftSide;
	public FloatVariable slowAmount;

	[Header("Libraries")]
	public ScrObjLibraryVariable kanjiLibrary;
	public ScrObjLibraryVariable battleLibrary;
	public StringVariable battleUuid;
	public InvListVariable invKanjiEquip;

	[Header("Player")]
	public IntVariable playerAttack;

	[Header("Kanji")]
	public ContainerKanji[] kanji;
	private float size;
	private float kanjiHeight;
	private Texture2D emptySprite;
	private Texture2D filledSprite;
	private Texture2D chargingSprite;

	private float shootCooldown;


	void Start () {
		shootCooldown = 0f;
		size = Constants.kanjiSize*Screen.height/Constants.SCREEN_HEIGHT;
		
		SetupTextures();
		SetEquippedKanji();
	}

	/// <summary>
	/// Generates the colors used to render the current states of the kanji.
	/// </summary>
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

	/// <summary>
	/// Reduces the shotcooldown and checks if the kanji should be activated.
	/// </summary>
	void Update() {
		if (paused.value)
			return;

		float time = (canBeSlowed.value && !slowLeftSide.value) ? (Time.deltaTime * slowAmount.value) : Time.deltaTime;

		if (shootCooldown > 0) {
			shootCooldown -= time;
		}

		for (int i = 0; i < Constants.MAX_EQUIPPED_KANJI; i++) {
			kanji[i].LowerCooldown(time);
		}
	}

	/// <summary>
	/// Retrieves the equipped kanji indexes to be used in the battle and assigns a kanji to each 
	/// containerKanji in the WeaponSlot.
	/// </summary>
	/// <returns></returns>
	void SetEquippedKanji(){
		float width = Constants.kanjiGuiOffsetWidth;
		kanjiHeight = Constants.kanjiGuiOffsetHeight;
		BattleEntry be = (BattleEntry)battleLibrary.GetEntry(battleUuid.value);

		for (int i = 0; i < Constants.MAX_EQUIPPED_KANJI; i++) {
			if (be.useSpecificKanji) {
				kanji[i].kanji = be.equippedKanji[i];
			}
			else {
				kanji[i].kanji = (Kanji)invKanjiEquip.values[i];
			}
			
			kanji[i].Initialize(i);

			//slotName = new Rect(BattleConstants.kanjiXPos+slot*80,BattleConstants.kanjiYPos-16,400,100);
			kanji[i].slotPos = new Rect(Screen.width*width+i*size*1.25f,Screen.height*kanjiHeight,size,size);
			kanji[i].slotFilled = new Rect(Screen.width*width+i*size*1.25f,Screen.height*kanjiHeight+size,size,size);
		}
	}

	/// <summary>
	/// Renders the current state of all the kanji in the weapon slot.
	/// </summary>
	void OnGUI(){
		if (paused.value)
			return;

		for (int i = 0; i < Constants.MAX_EQUIPPED_KANJI; i++) {

			if (kanji[i].kanji == null)
				continue;

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
			GUI.DrawTexture(kanji[i].slotPos,kanji[i].kanji.icon.texture);
		}
	}

	/// <summary>
	/// Returns if the player is currently attacking.
	/// </summary>
	/// <returns></returns>
	public bool IsAttacking(){
		return (shootCooldown > 0);
	}

	public bool Activate(MouseInformation mouseInfo) {
		if (shootCooldown > 0f){
			return false;
		}

		for (int i = 0; i < 4; i++) {
			if (kanji[i].CanActivate(mouseInfo)) {
				shootCooldown = kanji[i].GetValues().delay;
				kanji[i].reduceCharge();
				kanji[i].CreateEffect(mouseInfo, playerAttack.value);
				return true;
			}
		}

		return false;
	}
}
