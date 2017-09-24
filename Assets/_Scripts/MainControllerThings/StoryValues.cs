using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(BattleValues))]
public class StoryValues : MonoBehaviour {

	public enum BattleType { STORY,SPECIFIC,TOWER,RANDOM }

	private Inventory inv;

	public bool initiated = false;
	public int storyInt = 0;
	public int towerLevel = 0;
	public bool clearedTutorial = false;
	public BattleType battleType = BattleType.STORY;
	[HideInInspector] public BattleValues bv;
	[HideInInspector] public BattleValues bvRandom;
	[HideInInspector] public BattleValues bvTower;



	void Start(){
		inv = MainControllerScript.instance.inventory;

		BattleValues[] gofika = GetComponents<BattleValues>();
		bv = gofika[0];
		bvRandom = gofika[1];
		bvTower = gofika[2];
		Story();
		StartCoroutine(SetupRandomBattle("start"));

	}

	public void AdvanceStory(){
		if (battleType == BattleType.STORY)
			storyInt++;
		else if (battleType != BattleType.RANDOM)
			RandomBattle("towerNormal");
		else if (battleType == BattleType.RANDOM)
			RandomBattle("start");
		Story();
	}

	public IEnumerator SetupRandomBattle(string area){
		while (!EnemyLibrary.initialized) {
			yield return null;
		}

		RandomBattle(area);
		RandomBattle("towerNormal");

		initiated = true;
		Debug.Log("StoryValues is ready");
		yield break;
	}

	public BattleValues GetBattleValues(){
		switch (battleType) 
		{
		case BattleType.STORY:
			Debug.Log("Story battle start");
			return bv;
		case BattleType.RANDOM:
			Debug.Log("Random battle start");
			return bvRandom;
		case BattleType.TOWER:
		case BattleType.SPECIFIC:
			Debug.Log("Tower battle start");
			return bvTower;
		}

		return null;
	}

	public int[] GetEquippedKanji(){
		switch (battleType) 
		{
		case BattleType.STORY:
			Debug.Log("Story battle kanji");
			return bv.equippedKanji;
		case BattleType.RANDOM:
			Debug.Log("Random battle kanji");
			return inv.GetEquippedKanji();
		case BattleType.TOWER:
		case BattleType.SPECIFIC:
			Debug.Log("Tower battle kanji");
			return inv.GetEquippedKanji();
		}

		return null;
	}

	private void Story(){
		switch(storyInt)
		{
		case 0:
			bv.scenarioName = "First Steps";
			bv.removeSide = BattleValues.RemoveSide.LEFT;
			bv.backgroundHintLeft = 0;
			bv.backgroundHintRight = 1;
			bv.backgroundLeft = 2;
			bv.backgroundRight = 0;
			bv.enemyTypes = new List<int>();
			bv.enemyTypes.Add(0);
			bv.numberOfEnemies = 2;

			bv.escapeTextReq = true;
			bv.escapeButtonEnabled = true;
			bv.storyBattle = true;
			bv.healthEnabled = false;

			bv.equippedKanji = new int[]{0,0,0,0};
			break;
		case 1:
			bv.scenarioName = "Collide to Start";
			bv.removeSide = BattleValues.RemoveSide.LEFT;
			bv.backgroundHintLeft = 3;
			bv.backgroundHintRight = 14;
			bv.backgroundLeft = 14;
			bv.backgroundRight = 0;
			bv.enemyTypes = new List<int>();
			bv.enemyTypes.Add(0);
			bv.numberOfEnemies = 1;

			bv.escapeTextReq = true;
			bv.escapeButtonEnabled = false;
			bv.storyBattle = true;
			bv.healthEnabled = false;

			bv.equippedKanji = new int[]{5,0,0,0};
			break;
		case 2:
			bv.scenarioName = "Second Wind";
			bv.removeSide = BattleValues.RemoveSide.LEFT;
			bv.backgroundHintLeft = -1;
			bv.backgroundHintRight = -1;
			bv.backgroundLeft = 4;
			bv.backgroundRight = 0;
			bv.enemyTypes = new List<int>();
			bv.enemyTypes.Add(0);
			bv.numberOfEnemies = 3;

			bv.escapeTextReq = false;
			bv.escapeButtonEnabled = false;
			bv.storyBattle = true;
			bv.healthEnabled = false;

			bv.equippedKanji = new int[]{1,0,0,0};
			break;
		case 3:
			bv.scenarioName = "Wind the Second";
			bv.removeSide = BattleValues.RemoveSide.LEFT;
			bv.backgroundHintLeft = -1;
			bv.backgroundHintRight = -1;
			bv.backgroundLeft = 9;
			bv.backgroundRight = 0;
			bv.enemyTypes = new List<int>();
			bv.enemyTypes.Add(0);
			bv.numberOfEnemies = 2;

			bv.escapeTextReq = false;
			bv.escapeButtonEnabled = false;
			bv.storyBattle = true;
			bv.healthEnabled = false;

			bv.equippedKanji = new int[]{0,2,0,0};
			break;
		case 4:
			bv.scenarioName = "Ice the Third";
			bv.removeSide = BattleValues.RemoveSide.LEFT;
			bv.backgroundHintLeft = -1;
			bv.backgroundHintRight = -1;
			bv.backgroundLeft = 10;
			bv.backgroundRight = 0;
			bv.enemyTypes = new List<int>();
			bv.enemyTypes.Add(1);
			bv.numberOfEnemies = 4;

			bv.escapeTextReq = false;
			bv.escapeButtonEnabled = false;
			bv.storyBattle = true;
			bv.healthEnabled = false;

			bv.equippedKanji = new int[]{0,0,3,0};
			break;
		case 5:
			bv.scenarioName = "Gathering Power";
			bv.removeSide = BattleValues.RemoveSide.LEFT;
			bv.backgroundHintLeft = -1;
			bv.backgroundHintRight = -1;
			bv.backgroundLeft = 11;
			bv.backgroundRight = 0;
			bv.enemyTypes = new List<int>();
			bv.enemyTypes.Add(2);
			bv.numberOfEnemies = 3;

			bv.escapeTextReq = false;
			bv.escapeButtonEnabled = false;
			bv.storyBattle = true;
			bv.healthEnabled = false;

			bv.equippedKanji = new int[]{0,0,0,4};
			break;
		case 6:
			bv.scenarioName = "Follow the Arrows";
			bv.removeSide = BattleValues.RemoveSide.RIGHT;
			bv.backgroundHintLeft = 5;
			bv.backgroundHintRight = 6;
			bv.backgroundLeft = 0;
			bv.backgroundRight = 5;
			bv.enemyTypes = new List<int>();
			bv.enemyTypes.Add(0);
			bv.numberOfEnemies = 2;

			bv.escapeTextReq = true;
			bv.escapeButtonEnabled = false;
			bv.storyBattle = true;
			bv.healthEnabled = false;

			bv.equippedKanji = new int[]{0,0,0,0};
			break;
		case 7:
			bv.scenarioName = "You'll Never Catch Me";
			bv.removeSide = BattleValues.RemoveSide.RIGHT;
			bv.backgroundHintLeft = 12;
			bv.backgroundHintRight = 13;
			bv.backgroundLeft = 0;
			bv.backgroundRight = 13;
			bv.enemyTypes = new List<int>();
			bv.enemyTypes.Add(1);
			bv.numberOfEnemies = 3;

			bv.escapeTextReq = true;
			bv.escapeButtonEnabled = false;
			bv.storyBattle = true;
			bv.healthEnabled = false;

			bv.equippedKanji = new int[]{0,0,0,0};
			break;
		case 8:
			bv.scenarioName = "Don't Lose Balance";
			bv.removeSide = BattleValues.RemoveSide.RIGHT;
			bv.backgroundHintLeft = -1;
			bv.backgroundHintRight = -1;
			bv.backgroundLeft = 0;
			bv.backgroundRight = 8;
			bv.enemyTypes = new List<int>();
			bv.enemyTypes.Add(2);
			bv.numberOfEnemies = 3;

			bv.escapeTextReq = false;
			bv.escapeButtonEnabled = false;
			bv.storyBattle = true;
			bv.healthEnabled = false;

			bv.equippedKanji = new int[]{0,0,0,0};
			break;
		case 9:
			bv.scenarioName = "Put it all together";
			bv.removeSide = BattleValues.RemoveSide.NONE;
			bv.backgroundHintLeft = 7;
			bv.backgroundHintRight = 6;
			bv.backgroundLeft = 0;
			bv.backgroundRight = 0;
			bv.enemyTypes = new List<int>();
			bv.enemyTypes.Add(2);
			bv.numberOfEnemies = 3;

			bv.escapeTextReq = true;
			bv.escapeButtonEnabled = false;
			bv.storyBattle = true;
			bv.healthEnabled = false;

			bv.equippedKanji = new int[]{1,2,3,4};
			break;

		case 10:
			bv.scenarioName = "Playing For Reals";
			bv.removeSide = BattleValues.RemoveSide.NONE;
			bv.backgroundHintLeft = -1;
			bv.backgroundHintRight = -1;
			bv.backgroundLeft = 0;
			bv.backgroundRight = 0;
			bv.enemyTypes = new List<int>();
			bv.enemyTypes.Add(0);
			bv.enemyTypes.Add(1);
			bv.enemyTypes.Add(2);
			bv.numberOfEnemies = 3;

			bv.escapeTextReq = false;
			bv.escapeButtonEnabled = true;
			bv.storyBattle = false;
			bv.healthEnabled = true;

			bv.equippedKanji = new int[]{ 1, 2, 3, 4 };
			break;

		default:
			Debug.Log("You've cleared the tutorial!");
			clearedTutorial = true;
			bv.scenarioName = "Tutorial Cleared!";
			bv.removeSide = BattleValues.RemoveSide.NONE;
			bv.backgroundHintLeft = -1;
			bv.backgroundHintRight = -1;
			bv.backgroundLeft = 0;
			bv.backgroundRight = 0;
			bv.enemyTypes = new List<int>();
			bv.enemyTypes.Add(0);
			bv.enemyTypes.Add(1);
			bv.enemyTypes.Add(2);
			bv.numberOfEnemies = 3;

			bv.escapeTextReq = false;
			bv.escapeButtonEnabled = true;
			bv.storyBattle = false;
			bv.healthEnabled = true;

			bv.equippedKanji = new int[]{1,2,3,4};
			break;
		}

		if (clearedTutorial && storyInt != 11)
			bv.scenarioName = "";
	}

	public void RandomBattle(string area) {

		//Settings which are always the same
		bvRandom.removeSide = BattleValues.RemoveSide.NONE;
		bvRandom.backgroundHintLeft = -1;
		bvRandom.backgroundHintRight = -1;
		bvRandom.backgroundLeft = 0;
		bvRandom.backgroundRight = 0;
		bvRandom.escapeTextReq = false;
		bvRandom.escapeButtonEnabled = true;
		bvRandom.storyBattle = false;
		bvRandom.healthEnabled = true;
		bvRandom.enemyTypes = new List<int>();

		bvTower.removeSide = BattleValues.RemoveSide.NONE;
		bvTower.backgroundHintLeft = -1;
		bvTower.backgroundHintRight = -1;
		bvTower.backgroundLeft = 0;
		bvTower.backgroundRight = 0;
		bvTower.escapeTextReq = false;
		bvTower.escapeButtonEnabled = true;
		bvTower.storyBattle = false;
		bvTower.healthEnabled = true;
		bvTower.enemyTypes = new List<int>();

		//Random battles depending on the area
		switch(area)
		{
		case "start":
			bvRandom.scenarioName = "Random Battles!";
			bvRandom.enemyTypes.Add(0);
			bvRandom.enemyTypes.Add(1);
			bvRandom.enemyTypes.Add(2);
			bvRandom.numberOfEnemies = 3;
			break;
		case "towerNormal":
			bvTower.scenarioName = "Level "+towerLevel;
			bvTower.numberOfEnemies = -1;
			FillQuota(30+30*towerLevel);
			towerLevel++;
			break;
		case "towerBoss":
			break;
		}
		Debug.Log("BV tower: "+bvTower.numberOfEnemies);
	}


	private void FillQuota(int quota) {
		Dictionary<string,EnemyValues> enemyData = EnemyLibrary.enemyData;
		List<string> list = enemyData.Keys.ToList();
		int r, hp;
		while (quota > 0) {
			r = Random.Range(0,3);
			hp = enemyData[list[r]].maxhp;
			if (quota-hp > -100) {
				Debug.Log("Added "+r);
				bvTower.enemyTypes.Add(r);
				quota -= hp;
			}
		}
	}
}
