using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DrawScoreScreen : MonoBehaviour {

	public ScrObjLibraryVariable battleLibrary;
	public StringVariable battleUuid;
	public StringVariable dialogueUuid;

	public IntVariable currentArea;
	public IntVariable playerArea;
	public FloatVariable playerPosX;
	public FloatVariable playerPosY;
	public BoolVariable paused;

	public StringVariable wonBattleState;
	public FloatVariable battleTime;
	public IntVariable playerMaxHealth;
	public FloatVariable playerNormalDamage;
	public FloatVariable playerSpiritDamage;
	public IntVariable enemiesFought;
	public IntVariable expGained;
	public IntVariable moneyGained;

	public Text escapedText;
	public Text timeText;
	public Text healthText;
	public Text noEnemiesText;
	public Text expText;
	public Text moneyText;

	public UnityEvent buttonClickEvent;
	public UnityEvent changeMapEvent;

	// Use this for initialization
	void Start () {
		if (wonBattleState.value == "lose") {
			gameObject.SetActive(false);
			return;
		}

		SetValues();
	}


	private void SetValues(){

		if (wonBattleState.value == "win") {
			escapedText.text = "";
		}
		else if (wonBattleState.value == "escape") {
			expGained.value = 0;
			moneyGained.value = 0;
		}
		timeText.text = "Time:    "+ battleTime.value.ToString("F2") + "s";
		if (playerMaxHealth.value == 0) {
			healthText.text = "";
		}
		else {
			float currentHealth = playerMaxHealth.value + (playerNormalDamage.value + playerSpiritDamage.value);
			healthText.text = "Health left:    "+((currentHealth)/(playerMaxHealth.value) * 100).ToString("F2") + "%";
		}
		if (wonBattleState.value == "win") {
			noEnemiesText.text = "Enemies defeated:   " + enemiesFought.value;
			//Add what type of enemies was defeated
		}
		else {
			noEnemiesText.text = "Enemies faced:   "+enemiesFought.value;
		}

		expText.text = "Experience gained:    "+ expGained.value;
		moneyText.text = "Money gained:    "+ moneyGained.value;
		//Add loot
	}


	public void LeaveScoreScreen(){
		buttonClickEvent.Invoke();
		battleLibrary.GenerateDictionary();
		Debug.Log("UUID is: " + battleUuid.value);
		BattleEntry be = (BattleEntry)battleLibrary.GetEntry(battleUuid.value);
		switch (be.nextLocation)
		{
			case BattleEntry.NextLocation.OVERWORLD:
				paused.value = false;
				if (be.changePosition) {
					if (be.playerArea != Constants.OverworldArea.DEFAULT)
						playerArea.value = (int)be.playerArea;
					playerPosX.value = be.playerPosition.x;
					playerPosY.value = be.playerPosition.y;
				}
				currentArea.value = playerArea.value;
				Debug.Log("Battle -> Overworld");
				changeMapEvent.Invoke();
				break;
			case BattleEntry.NextLocation.DIALOGUE:
				currentArea.value = (int)Constants.SCENE_INDEXES.DIALOGUE;
				dialogueUuid.value = be.nextDialogue.uuid;
				Debug.Log("Battle -> Dialogue");
				changeMapEvent.Invoke();
				break;
			case BattleEntry.NextLocation.BATTLE:
				currentArea.value = (int)Constants.SCENE_INDEXES.BATTLE;
				battleUuid.value = be.nextBattle.uuid;
				Debug.Log("Battle -> Battle");
				changeMapEvent.Invoke();
				break;
		}

	}

}
