using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : BasicGUIButtons {

	public Text recordText;


	protected override void Start(){
		base.Start();
		recordText.text = "Highest level: "+SaveController.instance.bestLevel;
	}

	public void TutorialClicked(){
		MainControllerScript.instance.storyValues.battleType = StoryValues.BattleType.STORY;
		PlayButtonClick();
		SceneManager.LoadScene((int)BattleConstants.SCENE_INDEXES.TUTORIAL);
	}

	public void BattleClicked(){
		MainControllerScript.instance.storyValues.clearedTutorial = true;
		MainControllerScript.instance.storyValues.bv.scenarioName = "";
		MainControllerScript.instance.storyValues.battleType = StoryValues.BattleType.TOWER;
		PlayButtonClick();
		SceneManager.LoadScene((int)BattleConstants.SCENE_INDEXES.BATTLETOWER);
	}

}
