using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainMenuScript : MonoBehaviour {

	public Text recordText;

	public IntVariable currentArea;
	public IntVariable playerArea;

	public Canvas mainMenuCanvas;
	public Canvas levelSelectCanvas;

	public Button levelMaxButton;
	public Text levelMaxText;
	public Button levelMinus5Button;
	public Text levelMinus5Text;

	public StringVariable dialogueUuid;
	public IntVariable bestTowerLevel;
	public IntVariable currentTowerLevel;
	
	public UnityEvent buttonClickEvent;
	public UnityEvent mapChangeEvent;


	void Start(){
		recordText.text = "Highest level: " + bestTowerLevel.value;
	}

	public void StoryClicked(){
		buttonClickEvent.Invoke();
		dialogueUuid.value = "TrainToClassrom";
		currentArea.value = (int)Constants.SCENE_INDEXES.DIALOGUE;
		mapChangeEvent.Invoke();
	}

	public void BattleClicked(){
		mainMenuCanvas.enabled = false;
		levelSelectCanvas.enabled = true;
		int bestLevel = bestTowerLevel.value;

		levelMaxButton.GetComponentInChildren<Text>().text = "LEVEL " + bestLevel;
		levelMinus5Button.GetComponentInChildren<Text>().text = "LEVEL " + (bestLevel - 5);

		levelMaxButton.gameObject.SetActive(bestLevel > 1);
		levelMaxText.gameObject.SetActive(bestLevel > 1);
		levelMinus5Button.gameObject.SetActive(bestLevel > 6);
		levelMinus5Text.gameObject.SetActive(bestLevel > 6);
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
		currentArea.value = (int)Constants.SCENE_INDEXES.BATTLETOWER;
		playerArea.value = (int)Constants.SCENE_INDEXES.BATTLETOWER;
		mapChangeEvent.Invoke();
	}

}
