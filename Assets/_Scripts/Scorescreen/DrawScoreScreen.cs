using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DrawScoreScreen : BasicGUIButtons {

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
		timeText.text = "Time:    "+ battleTime.value.ToString("F2") + "s";
		if (playerMaxHealth.value == 0) {
			healthText.text = "";
		}
		else {
			float currentHealth = playerMaxHealth.value - playerNormalDamage.value + playerSpiritDamage.value;
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
		MainControllerScript.instance.storyValues.AdvanceStory();
		buttonClickEvent.Invoke();

		if (MainControllerScript.instance.storyValues.battleType == StoryValues.BattleType.SPECIFIC)
			SceneManager.LoadScene((int)BattleConstants.SCENE_INDEXES.BATTLETOWER);
		else
			SceneManager.LoadScene((int)BattleConstants.SCENE_INDEXES.TUTORIAL);
	}

}
