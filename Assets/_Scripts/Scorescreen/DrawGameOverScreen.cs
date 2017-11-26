﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DrawGameOverScreen : BasicGUIButtons {

	public Text timeText;

	public StringVariable wonBattleState;
	public FloatVariable battleTime;

	// Use this for initialization
	void Start () {
		if (wonBattleState.value != "lose") {
			gameObject.SetActive(false);
			return;
		}

		Debug.Log("Oh noes!");
		SetValues();
	}


	private void SetValues(){

		timeText.text = "You lasted for\n"+ battleTime.value.ToString("F2") + "s";
	}


	public void RetryBattle(){
		buttonClickEvent.Invoke();
		SceneManager.LoadScene((int)BattleConstants.SCENE_INDEXES.BATTLE);
	}

	public void ReturnToMainScreen(){
		buttonClickEvent.Invoke();
		SceneManager.LoadScene((int)BattleConstants.SCENE_INDEXES.MAINMENU);
	}

}
