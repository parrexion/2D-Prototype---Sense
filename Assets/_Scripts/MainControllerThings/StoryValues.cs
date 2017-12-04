using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Class which contains the information for the battles. 
/// Both random battles as well as tutorial and story battles.
/// </summary>
[RequireComponent(typeof(BattleValues))]
public class StoryValues : MonoBehaviour {

	public enum BattleType { STORY,SPECIFIC,TOWER,RANDOM }

	private Inventory inv;
	private KanjiList kanjiList;
	public ScrObjListVariable enemyLibrary;

	public bool initiated = false;
	public int storyInt = 0;
	public IntVariable towerLevel;
	public bool clearedTutorial = false;
	public BoolVariable equipAvailable;
	public BattleType battleType = BattleType.STORY;
	[HideInInspector] public BattleValues bv;
	[HideInInspector] public BattleValues bvRandom;
	[HideInInspector] public BattleValues bvTower;


	/// <summary>
	/// Sets up the battlevalues.
	/// </summary>
	void Start(){
		inv = MainControllerScript.instance.inventory;
		kanjiList = MainControllerScript.instance.kanjiList;

		BattleValues[] gofika = GetComponents<BattleValues>();
		bv = gofika[0];
		bvRandom = gofika[1];
		bvTower = gofika[2];
		Story();
		StartCoroutine(SetupRandomBattle("start"));
	}

	/// <summary>
	/// Advances the story to the next battle and sets it up.
	/// </summary>
	public void AdvanceStory(){
		if (battleType == BattleType.STORY)
			storyInt++;
		else if (battleType != BattleType.RANDOM)
			RandomBattle("towerNormal");
		else if (battleType == BattleType.RANDOM)
			RandomBattle("start");
		Story();
	}

	/// <summary>
	/// Sets up the random battle for the given area.
	/// </summary>
	/// <param name="area"></param>
	/// <returns></returns>
	public IEnumerator SetupRandomBattle(string area){

		RandomBattle(area);
		RandomBattle("towerNormal");

		initiated = true;
		Debug.Log("StoryValues is ready");
		yield break;
	}

	/// <summary>
	/// Returns the battlevalues which defines the battle from the story type used.
	/// </summary>
	/// <returns></returns>
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

	/// <summary>
	/// Returns an index list of the kanji which are equipped, 
	/// either from the story or the inventory.
	/// </summary>
	/// <returns></returns>
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
	
	/// <summary>
	/// Picks enemies until the quota is filled. Avoids overfilling the quota with more than 100.
	/// </summary>
	/// <param name="quota"></param>
	private void FillQuota(int quota) {
		bvTower.numberOfEnemies = 0;
		EnemyEntry ee;
		int r, hp;
		while (quota > 0) {
			// ee = (EnemyEntry)enemyLibrary.GetRandomEntry();
			r = Random.Range(0,3);
			ee = (EnemyEntry)enemyLibrary.GetEntryByIndex(r);
			hp = ee.maxhp;
			if (hp < quota+100) {
				// Debug.Log("Added "+r);
				bvTower.enemyTypes.Add(r);
				bvTower.numberOfEnemies++;
				quota -= hp;
			}
		}
	}

	/// <summary>
	/// A list of all the tutorial battles in the game which contains what kanji and enemies to use.
	/// </summary>
	public void Story(){

		equipAvailable.value = (battleType != BattleType.STORY || storyInt > 10);

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

			bv.equippedKanji = new int[]{kanjiList.GetKanjiIndex("FireOP"),0,0,0};
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

			bv.equippedKanji = new int[]{kanjiList.GetKanjiIndex("FireDelay"),0,0,0};
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

			bv.equippedKanji = new int[]{0,kanjiList.GetKanjiIndex("Wind"),0,0};
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

			bv.equippedKanji = new int[]{0,0,kanjiList.GetKanjiIndex("Ice"),0};
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

			bv.equippedKanji = new int[]{0,0,0,kanjiList.GetKanjiIndex("Earth")};
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

			bv.equippedKanji = new int[]{kanjiList.GetKanjiIndex("Fire")
										,kanjiList.GetKanjiIndex("Wind")
										,kanjiList.GetKanjiIndex("Ice")
										,kanjiList.GetKanjiIndex("Earth")};
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

			bv.equippedKanji = new int[]{kanjiList.GetKanjiIndex("Fire")
										,kanjiList.GetKanjiIndex("Wind")
										,kanjiList.GetKanjiIndex("Ice")
										,kanjiList.GetKanjiIndex("Earth")};
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

			bv.equippedKanji = new int[]{kanjiList.GetKanjiIndex("Fire")
										,kanjiList.GetKanjiIndex("Wind")
										,kanjiList.GetKanjiIndex("Ice")
										,kanjiList.GetKanjiIndex("Earth")};
			break;
		}

		if (clearedTutorial && storyInt != 11)
			bv.scenarioName = "";
	}

	/// <summary>
	/// Takes the given area and fills the battlevalues with random enemies.
	/// </summary>
	/// <param name="area"></param>
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
		bvRandom.enemyTypes.Add(0);
		bvRandom.enemyTypes.Add(1);
		bvRandom.enemyTypes.Add(2);

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
			FillQuota(30+30*towerLevel.value);
			towerLevel.value++;
			break;
		case "towerBoss":
			break;
		}

	}
}
