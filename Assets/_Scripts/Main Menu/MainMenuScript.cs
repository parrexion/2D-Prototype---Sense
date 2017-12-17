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

	public StringVariable dialogueUuid;
	public IntVariable bestTowerLevel;
	public IntVariable currentTowerLevel;


	void Start(){
		recordText.text = "Highest level: " + bestTowerLevel.value;
	}

	public void TutorialClicked(){
		buttonClickEvent.Invoke();
		SceneManager.LoadScene((int)Constants.SCENE_INDEXES.BATTLE);
	}

	public void StoryClicked(){
		buttonClickEvent.Invoke();
		dialogueUuid.value = "OldPrologueComplete";
		SceneManager.LoadScene((int)Constants.SCENE_INDEXES.DIALOGUE);
	}

	public void BattleClicked(){
		mainMenuCanvas.enabled = false;
		levelSelectCanvas.enabled = true;
		int bestLevel = bestTowerLevel.value;

		levelMaxButton.gameObject.SetActive(bestLevel > 1);
		levelMaxText.gameObject.SetActive(bestLevel > 1);
		levelMaxButton.gameObject.SetActive(bestLevel > 6);
		levelMaxText.gameObject.SetActive(bestLevel > 6);

		levelMaxButton.GetComponentInChildren<Text>().text = "LEVEL " + bestLevel;
		levelMinus5Button.GetComponentInChildren<Text>().text = "LEVEL " + (bestLevel - 5);
	}

	public void LevelSelectClicked(int levelPosition){
		int bestLevel = bestTowerLevel.value;
		if (levelPosition == 1) 
			currentTowerLevel.value = 1;
		else if (levelPosition == 3)
			currentTowerLevel.value = bestLevel;
		else
			currentTowerLevel.value = bestLevel - 5;

		buttonClickEvent.Invoke();
		SceneManager.LoadScene((int)Constants.SCENE_INDEXES.BATTLETOWER);
	}

}
