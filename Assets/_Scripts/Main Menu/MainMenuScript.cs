using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MainMenuScript : BasicGUIButtons {

	public UnityEvent buttonClickEvent;

	public Text recordText;

	public Canvas mainMenuCanvas;
	public Canvas levelSelectCanvas;

	public Button levelMaxButton;
	public Text levelMaxText;
	public Button levelMinus5Button;
	public Text levelMinus5Text;


	void Start(){
		recordText.text = "Highest level: "+SaveController.instance.bestLevel;
	}

	public void TutorialClicked(){
		MainControllerScript.instance.storyValues.battleType = StoryValues.BattleType.STORY;
		buttonClickEvent.Invoke();
		SceneManager.LoadScene((int)BattleConstants.SCENE_INDEXES.BATTLE);
	}

	public void BattleClicked(){
		mainMenuCanvas.enabled = false;
		levelSelectCanvas.enabled = true;
		int bestLevel = SaveController.instance.bestLevel;

		levelMaxButton.gameObject.SetActive(bestLevel > 1);
		levelMaxText.gameObject.SetActive(bestLevel > 1);
		levelMaxButton.gameObject.SetActive(bestLevel > 6);
		levelMaxText.gameObject.SetActive(bestLevel > 6);

		levelMaxButton.GetComponentInChildren<Text>().text = "LEVEL " + bestLevel;
		levelMinus5Button.GetComponentInChildren<Text>().text = "LEVEL " + (bestLevel - 5);
	}

	public void LevelSelectClicked(int levelPosition){
		int bestLevel = SaveController.instance.bestLevel;
		if (levelPosition == 1) 
			MainControllerScript.instance.storyValues.towerLevel = 1;
		else if (levelPosition == 3)
			MainControllerScript.instance.storyValues.towerLevel = bestLevel;
		else
			MainControllerScript.instance.storyValues.towerLevel = bestLevel - 5;

		MainControllerScript.instance.storyValues.clearedTutorial = true;
		MainControllerScript.instance.storyValues.bv.scenarioName = "";
		MainControllerScript.instance.storyValues.battleType = StoryValues.BattleType.TOWER;
		buttonClickEvent.Invoke();
		SceneManager.LoadScene((int)BattleConstants.SCENE_INDEXES.BATTLETOWER);
	}

}
