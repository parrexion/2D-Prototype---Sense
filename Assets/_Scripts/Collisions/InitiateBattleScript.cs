using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InitiateBattleScript : MonoBehaviour {

	public Text fightText;

	void OnTriggerEnter2D(Collider2D otherCollider){

		BattleTrigger battle = otherCollider.gameObject.GetComponent<BattleTrigger>();
		if (battle != null) {
			Debug.Log("Random is: "+battle.battleType);
			MainControllerScript.instance.storyValues.battleType = battle.battleType;
			StartCoroutine(StartBattle(2f));
		} 
		else
			Debug.Log ("Null");
	}


	private IEnumerator StartBattle(float time){
		fightText.text = "FIGHT!";
		GetComponent<OutsidePlayerController>().SetActive(false);
		yield return new WaitForSeconds(time);
		SceneManager.LoadScene(BattleConstants.SCENE_BATTLE);
	}
}
