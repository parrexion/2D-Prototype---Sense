using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleValues : MonoBehaviour {

	public enum RemoveSide {NONE,LEFT,RIGHT};

	public string scenarioName = "";
	public RemoveSide removeSide = RemoveSide.NONE;
	public int backgroundHintLeft = -1;
	public int backgroundHintRight = -1;
	public int backgroundLeft = 0;
	public int backgroundRight = 0;
	public int numberOfEnemies = 1;
	public List<int> enemyTypes;
	public bool escapeTextReq = false;

	public bool storyBattle = false;
	public bool escapeButtonEnabled = true;
	public bool healthEnabled = true;

	public int[] equippedKanji;
}
