using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DrawScoreScreen : BasicGUIButtons {

	public ScoreScreenValues values;
	public GameObject canvas;
	public Text escapedText;
	public Text timeText;
	public Text healthText;
	public Text noEnemiesText;
	public Text expText;
	public Text moneyText;

	// Use this for initialization
	protected override void Start () {
		base.Start();

		values = GameObject.Find("SaveValues").GetComponent<ScoreScreenValues>();
		canvas = GameObject.Find("Canvas - Victory");

		if (values.lostBattle) {
			canvas.SetActive(false);
			return;
		}

		escapedText = canvas.transform.Find("Escaped").GetComponent<Text>();
		timeText = canvas.transform.Find("Time").GetComponent<Text>();
		healthText = canvas.transform.Find("Health").GetComponent<Text>();
		noEnemiesText = canvas.transform.Find("NoEnemies").GetComponent<Text>();
		expText = canvas.transform.Find("EXP").GetComponent<Text>();
		moneyText = canvas.transform.Find("Money").GetComponent<Text>();

		SetValues();
	}


	private void SetValues(){

		if (values.wonBattle) {
			escapedText.text = "";
		}
		timeText.text = "Time:    "+ values.time.ToString("F2") + "s";
		if (values.maxHealth == 0) {
			healthText.text = "";
		}
		else
			healthText.text = "Health left:    "+((float)(values.currentHealth)/(float)(values.maxHealth) * 100) + "%";
		if (values.wonBattle) {
			noEnemiesText.text = "Enemies defeated:   "+values.noEnemies;
			//Add what type of enemies was defeated
		}
		else {
			noEnemiesText.text = "Enemies faced:   "+values.noEnemies;
		}

		expText.text = "Experience gained:    "+ values.exp;
		moneyText.text = "Money gained:    "+ values.money;
		//Add loot
	}


	public void LeaveScoreScreen(){
		MainControllerScript.instance.storyValues.AdvanceStory();
		PlayButtonClick();
		Destroy(values.gameObject);

		if (MainControllerScript.instance.storyValues.battleType == StoryValues.BattleType.SPECIFIC)
			SceneManager.LoadScene((int)BattleConstants.SCENE_INDEXES.BATTLETOWER);
		else
			SceneManager.LoadScene((int)BattleConstants.SCENE_INDEXES.TUTORIAL);
	}

}
