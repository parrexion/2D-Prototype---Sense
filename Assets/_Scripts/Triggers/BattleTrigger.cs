using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTrigger : MonoBehaviour {

	public string area;
	public int noEnemies;
	public List<string> enemyTypes;

	public StoryValues.BattleType battleType;

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
}
