using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(LoadEverything());
	}



	IEnumerator LoadEverything() {
		while (MainControllerScript.instance == null)
			yield return null;

		Debug.Log("Maincontroller now exists");

		while (!MainControllerScript.instance.initiated)
			yield return null;

		Debug.Log("Maincontroller now initiated");

		SceneManager.LoadScene((int)BattleConstants.SCENE_INDEXES.MAINMENU);

		yield return null;
	}
}
