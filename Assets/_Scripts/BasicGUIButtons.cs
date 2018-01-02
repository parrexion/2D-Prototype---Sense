using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/// <summary>
/// Basic class which can be used to transition between scenes. 
/// Can be overridden to add additional features.
/// </summary>
public class BasicGUIButtons : MonoBehaviour {

	public UnityEvent buttonClickEvent;


	/// <summary>
	/// Simple way to move to the next scene. Write a new method if more functionality is needed.
	/// </summary>
	/// <param name="scene">Scene.</param>
	public void SimpleMoveToScene(string scene) {
		buttonClickEvent.Invoke();

		switch (scene) {
		case "battle":
			SceneManager.LoadScene((int)Constants.SCENE_INDEXES.BATTLE);
			break;
		case "dialogue":
			SceneManager.LoadScene((int)Constants.SCENE_INDEXES.DIALOGUE);
			break;
		case "inventory":
			SceneManager.LoadScene((int)Constants.SCENE_INDEXES.INVENTORY);
			break;
		case "mainmenu":
			SceneManager.LoadScene((int)Constants.SCENE_INDEXES.MAINMENU);
			break;
		case "options":
			SceneManager.LoadScene((int)Constants.SCENE_INDEXES.OPTIONS);
			break;
		case "scorescreen":
			SceneManager.LoadScene((int)Constants.SCENE_INDEXES.SCORE);
			break;
		case "tower":
			SceneManager.LoadScene((int)Constants.SCENE_INDEXES.BATTLETOWER);
			break;
		case "tutorial":
			SceneManager.LoadScene((int)Constants.SCENE_INDEXES.TUTORIAL_OLD);
			break;
		case "eastSection":
			SceneManager.LoadScene((int)Constants.SCENE_INDEXES.EAST_SECTION);
			break;
		case "eastSectionRooms":
			SceneManager.LoadScene((int)Constants.SCENE_INDEXES.EAST_SECTION_ROOMS);
			break;
		case "centralSection":
			SceneManager.LoadScene((int)Constants.SCENE_INDEXES.CENTRAL_SECTION);
			break;
		case "centralSectionRooms":
			SceneManager.LoadScene((int)Constants.SCENE_INDEXES.CENTRAL_SECTION_ROOMS);
			break;
		case "westSection":
			SceneManager.LoadScene((int)Constants.SCENE_INDEXES.WEST_SECTION);
			break;
		case "westSectionRooms":
			SceneManager.LoadScene((int)Constants.SCENE_INDEXES.WEST_SECTION_ROOMS);
			break;
		default:
			Debug.Log("Failed to find a scene to move to");
			break;
		}
	}

}
