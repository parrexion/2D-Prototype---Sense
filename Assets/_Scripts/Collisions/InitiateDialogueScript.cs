using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Overworld trigger which starts a battle when colliding with battle triggers.
/// </summary>
public class InitiateDialogueScript : MonoBehaviour {

	public Text screenText;
	public BoolVariable paused;

	/// <summary>
	/// Function which starts the start battle animation.
	/// </summary>
	/// <param name="time"></param>
	/// <returns></returns>
	public void StartDialogue(float time) {
		StartCoroutine(DialogueDelay(time));
	}

	/// <summary>
	/// Show the fight text and move on to the battle screen after a while.
	/// </summary>
	/// <param name="time"></param>
	/// <returns></returns>
	private IEnumerator DialogueDelay(float time){
		screenText.text = "DIALOGUE!";
		paused.value = true;
		yield return new WaitForSeconds(time);
		SceneManager.LoadScene((int)Constants.SCENE_INDEXES.DIALOGUE);
	}
}
