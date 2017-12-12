using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DrawScoreScreen : BasicGUIButtons {

	public ScrObjLibraryVariable battleLibrary;
	public StringVariable battleUuid;
	public StringVariable dialogueUuid;
	public FloatVariable playerPosX;
	public FloatVariable playerPosY;
	public BoolVariable paused;

	public StringVariable wonBattleState;
	public FloatVariable battleTime;
	public FloatVariable playerMaxHealth;
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
			float currentHealth = playerMaxHealth.value - (playerNormalDamage.value + playerSpiritDamage.value);
			healthText.text = "Health left:    "+((currentHealth)/(playerMaxHealth.value) * 100) + "%";
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
		BattleEntry be = (BattleEntry)battleLibrary.GetEntry(battleUuid.value);
		switch (be.nextLocation)
		{
			case BattleEntry.NextLocation.OVERWORLD:
				paused.value = false;
				if (be.changePosition) {
					playerPosX.value = be.playerPosition.x;
					playerPosY.value = be.playerPosition.y;
				}
				if (be.playerArea == BattleEntry.OverworldArea.TOWER)
					SceneManager.LoadScene((int)Constants.SCENE_INDEXES.BATTLETOWER);
				else
					SceneManager.LoadScene((int)Constants.SCENE_INDEXES.TUTORIAL);
				break;
			case BattleEntry.NextLocation.DIALOGUE:
				dialogueUuid.value = be.nextDialogue.name;
				SceneManager.LoadScene((int)Constants.SCENE_INDEXES.DIALOGUE);
				break;
			case BattleEntry.NextLocation.BATTLE:
				battleUuid.value = be.nextBattle.uuid;
				SceneManager.LoadScene((int)Constants.SCENE_INDEXES.BATTLE);
				break;
		}

	}

}
