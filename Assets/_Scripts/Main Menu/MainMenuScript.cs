using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : BasicGUIButtons {

	public Text recordText;

	public Canvas mainMenuCanvas;
	public Canvas levelSelectCanvas;

	public Button levelMaxButton;
	public Text levelMaxText;
	public Button levelMinus5Button;
	public Text levelMinus5Text;

	public IntVariable currentTowerLevel;


	void Start(){
		recordText.text = "Highest level: " + SaveController.instance.bestLevel;
	}

	public void TutorialClicked(){
		StoryValues story = MainControllerScript.instance.storyValues;
		story.battleType = StoryValues.BattleType.STORY;
		story.storyInt = 0;
		story.Story();

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
			currentTowerLevel.value = 1;
		else if (levelPosition == 3)
			currentTowerLevel.value = bestLevel;
		else
			currentTowerLevel.value = bestLevel - 5;

		MainControllerScript.instance.storyValues.clearedTutorial = true;
		MainControllerScript.instance.storyValues.bv.scenarioName = "";
		MainControllerScript.instance.storyValues.battleType = StoryValues.BattleType.TOWER;
		buttonClickEvent.Invoke();
		SceneManager.LoadScene((int)BattleConstants.SCENE_INDEXES.BATTLETOWER);
	}

}
