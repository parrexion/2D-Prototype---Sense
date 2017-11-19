using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

	public BattleValues bv;
	public bool initiated = false;

	public static int enemyId = 0;
	public int numberOfEnemies;
	private List<int> enemySelection;

	public bool spawnTop = true;
	public bool spawnBottom = true;

	public Transform deadEnemy;
	private Transform ggobjN;
	private Transform ggobjS;

	private List<EnemyGroup> groups;
	public List<string> enemyModelNames;
	public List<Transform> enemyNormalModels;
	public List<Transform> enemySpiritModels;


	// Use this for initialization
	void OnEnable () {
		enemyId = 0;
		groups = new List<EnemyGroup>();

		StartCoroutine("CreateEnemies");
	}

	IEnumerator CreateEnemies(){
		while (!MainControllerScript.instance || !MainControllerScript.instance.initiated) {
			Debug.Log("Waiting");
			yield return null;
		}

		bv = MainControllerScript.instance.storyValues.GetBattleValues();

		numberOfEnemies = bv.numberOfEnemies;
		enemySelection = bv.enemyTypes;

		initiated = true;
		Debug.Log("EnemyController is ready");
	}

	/// <summary>
	/// Spawns all the enemies and groups them into enemy groups.
	/// </summary>
	/// <param name="enableBottom"></param>
	/// <param name="enableTop"></param>
	public void CreateEnemies(bool enableBottom, bool enableTop){
		spawnBottom = enableBottom;
		spawnTop = enableTop;
		
		int index, r;
		if (MainControllerScript.instance.storyValues.battleType != StoryValues.BattleType.SPECIFIC) {
			for (int i = 0; i < numberOfEnemies; i++) {
				r = Random.Range(0,enemySelection.Count);
				Debug.Log("r = " + r + ", enemySelection: " + enemySelection.Count);
				index = enemySelection[r];
				groups.Add(CreateGroup(index));
			}
		}
		else {
			for (int i = 0; i < enemySelection.Count; i++) {
				groups.Add(CreateGroup(enemySelection[i]));
			}
		}
	}

	/// <summary>
	/// Creates an enemy group with the given enemy index.
	/// </summary>
	/// <param name="index"></param>
	/// <returns></returns>
	private EnemyGroup CreateGroup(int index){
		EnemyValues values = EnemyLibrary.enemyData[enemyModelNames[index]];
		EnemyGroup group = new EnemyGroup(enemyId, values.maxhp);
		enemyId++;
		group.deadEnemy = deadEnemy;
		CreateN(values,group,index);
		CreateS(values,group,index);
		return group;
	}

	/// <summary>
	/// Creates an enemy for the normal side of the battlefield.
	/// </summary>
	/// <param name="values"></param>
	/// <param name="group"></param>
	/// <param name="index"></param>
	private void CreateN(EnemyValues values, EnemyGroup group, int index){
		if (!spawnBottom)
			return;
		ggobjN = Instantiate(enemyNormalModels[index]) as Transform;
		NStateController state = ggobjN.GetComponent<NStateController>();
		HurtableEnemyScript hurt = ggobjN.GetComponent<HurtableEnemyScript>();
		AttackScript attack = ggobjN.GetComponent<AttackScript>();

		ggobjN.position = state.GetRandomLocation();
		hurt.group = group;
		state.enemyid = enemyId;
		state.values = values;

		group.bot = hurt;
		group.nStateController = state;
		group.nTransform = ggobjN;
		group.nAttackScript = attack;
	}

	/// <summary>
	/// Creates an enemy for the spirit side of the battlefield.
	/// </summary>
	/// <param name="values"></param>
	/// <param name="group"></param>
	/// <param name="index"></param>
	private void CreateS(EnemyValues values, EnemyGroup group, int index){
		if (!spawnTop)
			return;
		ggobjS = Instantiate(enemySpiritModels[index]) as Transform;
		int side = Random.Range(0,2);
		SStateController state = ggobjS.GetComponent<SStateController>();
		HurtableEnemyScript hurt = ggobjS.GetComponent<HurtableEnemyScript>();
		AttackScript attack = ggobjS.GetComponent<AttackScript>();

		if (side == 0) {
			state.leftSide = true;
		}

		ggobjS.position = state.GetRandomLocation();

		hurt.group = group;

		state.enemyid = enemyId;
		state.values = values;

		group.top = hurt;
		group.sStateController = state;
		group.sTransform = ggobjS;
		group.sAttackScript = attack;
	}

	/// <summary>
	/// Sets the active state for all AI groups.
	/// </summary>
	/// <param name="active"></param>
	public void SetAllAIActive(bool active) {
		foreach (EnemyGroup eg in groups) {
			if (eg.alive) {
				eg.SetActiveAI(active);
			}
		}
	}

	/// <summary>
	/// Checks if the enemy is on the left side of the spirit side.
	/// </summary>
	/// <param name="leftside"></param>
	/// <returns></returns>
	public bool CheckIfEnemiesAtSide(bool leftside) {
		foreach (EnemyGroup eg in groups) {
			if (eg.alive && (eg.sStateController.leftSide == leftside)) {
				return true;
			}
		}

		return false;
	}

	/// <summary>
	/// Checks if all the enemies are dead.
	/// </summary>
	/// <returns></returns>
	public bool CheckAllEnemiesDead(){
		foreach (EnemyGroup eg in groups) {
			if (eg.alive) {
				return false;
			}
		}
		return true;
	}
		
	/// <summary>
	/// Selects a group of enemies to hit with the spirit side final attack and returns the 
	/// damage numbers for the attack.
	/// </summary>
	/// <param name="hits"></param>
	/// <param name="baseDamage"></param>
	/// <param name="top"></param>
	/// <param name="leftside"></param>
	/// <returns></returns>
	public List<DamageValues> GetRandomEnemies(int hits, int baseDamage, bool top, bool leftside) {

		List<DamageValues> values = new List<DamageValues>();

		if (top) {
			int i = 0, j= 0, h = 0;
			while (h < hits) {
				if (groups[i].alive && groups[i].sStateController.leftSide == leftside) {
					if (j == h) {
						DamageValues d = new DamageValues(groups[i].sTransform);
						d.baseDamage = baseDamage;
						values.Add(d);
					}
					else {
						values[j].baseDamage += baseDamage;
					}
					h++;
					j++;
				}

				i++;

				if (i >= groups.Count) {
					if (h == 0) {
						Debug.LogWarning("Everyone is dead or not here");
						return null;
					}
					i = 0;
					j = 0;
				}
			}
		}
		else {
			values.Add(new DamageValues(groups[0].nTransform));
		}

		return values;
	}

	/// <summary>
	/// Summarises the exp for all the enemies defeated.
	/// </summary>
	/// <returns></returns>
	public int GetTotalExp(){
		int exp = 0;
		foreach (EnemyGroup eg in groups) {
			if (spawnBottom)
				exp += eg.nStateController.values.exp;
			else if (spawnTop)
				exp += eg.sStateController.values.exp;
		}
		return exp;
	}

	/// <summary>
	/// Summarises the money for all the enemies defeated.
	/// </summary>
	/// <returns></returns>
	public int GetTotalMoney(){
		int money = 0;
		foreach (EnemyGroup eg in groups) {
			if (spawnBottom)
				money += eg.nStateController.values.money;
			else if (spawnTop)
				money += eg.sStateController.values.money;
		}
		return money;
	}

	/// <summary>
	/// Returns a list of the names of the enemies defeated.
	/// </summary>
	/// <returns></returns>
	public List<string> GetEnemiesDefeated(){
		List<string> names = new List<string>();
		foreach (EnemyGroup eg in groups) {
			if (spawnBottom)
				names.Add(eg.nStateController.values.name);
			else if (spawnTop)
				names.Add(eg.sStateController.values.name);
		}
		return names;
	}

	/// <summary>
	/// Returns a list of the loot dropped by the enemies defeated.
	/// </summary>
	/// <returns></returns>
	public List<string> GetTreasures(){
		return new List<string>();
	}
}
