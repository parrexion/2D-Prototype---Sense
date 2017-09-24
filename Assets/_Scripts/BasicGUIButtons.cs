using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioList))]
public class BasicGUIButtons : MonoBehaviour {

	public AudioClip backgroundMusic = null;
	public AudioClip buttonClick;

	protected AudioController audioController;


	// Use this for initialization
	protected virtual void Start () {
		audioController = AudioController.instance;
		audioController.PlayBackgroundMusic(backgroundMusic);
	}

	protected void PlayButtonClick(){
		audioController.PlaySingle(buttonClick);
	}

	/// <summary>
	/// Simple way to move to the next scene. Write a new method if more functionality is needed.
	/// </summary>
	/// <param name="scene">Scene.</param>
	public void SimpleMoveToScene(string scene) {
		PlayButtonClick();
		switch (scene) {
		case "battle":
			SceneManager.LoadScene((int)BattleConstants.SCENE_INDEXES.BATTLE);
			break;
		case "dialogue":
			SceneManager.LoadScene((int)BattleConstants.SCENE_INDEXES.DIALOGUE);
			break;
		case "inventory":
			SceneManager.LoadScene((int)BattleConstants.SCENE_INDEXES.INVENTORY);
			break;
		case "mainmenu":
			SceneManager.LoadScene((int)BattleConstants.SCENE_INDEXES.MAINMENU);
			break;
		case "scorescreen":
			SceneManager.LoadScene((int)BattleConstants.SCENE_INDEXES.SCORE);
			break;
		case "tower":
			SceneManager.LoadScene((int)BattleConstants.SCENE_INDEXES.BATTLETOWER);
			break;
		default:
			Debug.Log("Failed to find a scene to move to");
			break;
		}
	}

}
