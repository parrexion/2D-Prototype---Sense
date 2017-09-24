using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DrawGameOverScreen : MonoBehaviour {

	public ScoreScreenValues values;
	public GameObject canvas;
	public Text timeText;

	// Use this for initialization
	void Start () {
		values = GameObject.Find("SaveValues").GetComponent<ScoreScreenValues>();

		canvas = GameObject.Find("Canvas - Game over");
		timeText = canvas.transform.FindChild("DeadTime").GetComponent<Text>();

		if (!values.lostBattle) {
			canvas.SetActive(false);
			return;
		}

		Debug.Log("Oh noes!");
		SetValues();

	}


	private void SetValues(){

		timeText.text = "You lasted for\n"+ values.time.ToString("F2") + "s";
	}


	public void RetryBattle(){

		SceneManager.LoadScene((int)BattleConstants.SCENE_INDEXES.BATTLE);
	}

	public void ReturnToMainScreen(){
		Destroy(values.gameObject);
		SceneManager.LoadScene((int)BattleConstants.SCENE_INDEXES.MAINMENU);
	}

}
