using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleTrigger : MonoBehaviour {

	public string area;
	public int noEnemies;
	public List<string> enemyTypes;
	public StoryValues.BattleType battleType;
	public UnityEvent battleStartEvent;


	void Start() {
		bool tutorial = MainControllerScript.instance.storyValues.clearedTutorial;
		bool random = MainControllerScript.instance.storyValues.battleType == StoryValues.BattleType.RANDOM;

		if (battleType == StoryValues.BattleType.RANDOM) {
			if (!random && !tutorial)
				gameObject.SetActive(false);
		}
		else if (battleType == StoryValues.BattleType.STORY){
			if (tutorial || random)
				gameObject.SetActive(false);
		}
	}
	
	/// <summary>
	/// When colliding with a BattleTrigger, start the battle.
	/// </summary>
	/// <param name="otherCollider"></param>
	void OnTriggerEnter2D(Collider2D otherCollider){
		if (otherCollider.gameObject.tag != "Player") {
			Debug.Log("That was not a player");
			return;
		}

		Debug.Log("Battle is: "+ battleType);
		MainControllerScript.instance.storyValues.battleType = battleType;
		battleStartEvent.Invoke();
	}
}
