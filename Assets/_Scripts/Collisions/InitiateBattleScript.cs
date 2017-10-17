using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Overworld trigger which starts a battle when colliding with battle triggers.
/// </summary>
public class InitiateBattleScript : MonoBehaviour {

	public Text fightText;


	/// <summary>
	/// When colliding with a BattleTrigger, start the battle.
	/// </summary>
	/// <param name="otherCollider"></param>
	void OnTriggerEnter2D(Collider2D otherCollider){
		BattleTrigger battle = otherCollider.gameObject.GetComponent<BattleTrigger>();
		if (battle == null) {
			Debug.Log("Null");
			return;
		}

		Debug.Log("Random is: "+battle.battleType);
		MainControllerScript.instance.storyValues.battleType = battle.battleType;
		StartCoroutine(StartBattle(2f));
	}

	/// <summary>
	/// Show the fight text and move on to the battle screen after a while.
	/// </summary>
	/// <param name="time"></param>
	/// <returns></returns>
	private IEnumerator StartBattle(float time){
		fightText.text = "FIGHT!";
		GetComponent<OutsidePlayerController>().SetActive(false);
		yield return new WaitForSeconds(time);
		SceneManager.LoadScene((int)BattleConstants.SCENE_INDEXES.BATTLE);
	}
}
