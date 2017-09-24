using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

	public Text recordText;


	void Start(){
		recordText.text = "Highest level: "+SaveController.instance.bestLevel;
	}

	public void StoryClicked(){
		SceneManager.LoadScene(BattleConstants.SCENE_DIALOGUE);
	}

	public void TutorialClicked(){
		SceneManager.LoadScene(BattleConstants.SCENE_BATTLE);
	}

	public void BattleClicked(){
		MainControllerScript.instance.storyValues.clearedTutorial = true;
		MainControllerScript.instance.battleValues.scenarioName = "";
		MainControllerScript.instance.storyValues.battleType = StoryValues.BattleType.SPECIFIC;
		SceneManager.LoadScene(BattleConstants.SCENE_BATTLETOWER);
	}

}
