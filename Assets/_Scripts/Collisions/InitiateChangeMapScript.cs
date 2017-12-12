using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/// <summary>
/// Overworld trigger which starts a battle when colliding with battle triggers.
/// </summary>
public class InitiateChangeMapScript : MonoBehaviour {

	public Text screenText;
	public BoolVariable paused;
	public UnityEvent movePlayerEvent;

	/// <summary>
	/// Function which starts the start battle animation.
	/// </summary>
	/// <param name="time"></param>
	/// <returns></returns>
	public void StartTransition(float time) {
		StartCoroutine(TransitionDelay(time));
	}

	/// <summary>
	/// Show the fight text and move on to the battle screen after a while.
	/// </summary>
	/// <param name="time"></param>
	/// <returns></returns>
	private IEnumerator TransitionDelay(float time){
		screenText.text = "CHANGE\nMAP!";
		paused.value = true;
		yield return new WaitForSeconds(time);
		movePlayerEvent.Invoke();
		screenText.text = "";
		paused.value = false;
	}
}
