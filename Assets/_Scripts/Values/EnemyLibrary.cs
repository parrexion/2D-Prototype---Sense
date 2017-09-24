using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLibrary : MonoBehaviour {

	public static Dictionary<string,EnemyValues> enemyData;
	public static bool initialized = false;

	public static void Initialize() {
		enemyData = new Dictionary<string, EnemyValues>();
		EnemyValues e;

		//Wolf
		e = new EnemyValues();
		e.name = "wolf";
		e.maxhp = 75;
		e.speed = new Vector2(4f,3f);

		e.waitTimeLimits = new RangedFloat(3.0f,5.0f);
		e.waitStates.Add(StateController.WaitStates.CHASE);
		e.waitStates.Add(StateController.WaitStates.MOVE);

		e.meleeRange = 1.2f;
		e.attackRate = 1f;
		e.attacks = 1;
		e.meleeTimeStartup = 0.5f;
		e.meleeTimeAnimation = 0.5f;

		e.chaseTimeLimit = 5f;

		e.fleeDistance = 3f;
		e.fleeTimeLimit = 0.8f;

		e.exp = 40;
		e.money = 20;

		enemyData.Add("wolf",e);
		Debug.Log(e.name+" is loaded");

		//Quill
		e = new EnemyValues();
		e.name = "quill";
		e.maxhp = 60;
		e.speed = new Vector2(4f,3f);

		e.waitTimeLimits = new RangedFloat(3.0f,5.0f);
		e.waitStates.Add(StateController.WaitStates.CHASE);
		e.waitStates.Add(StateController.WaitStates.MOVE);

		e.meleeRange = 0.5f;
		e.attackRate = 1f;
		e.attacks = 1;
		e.meleeTimeStartup = 0.5f;
		e.meleeTimeAnimation = 0.5f;

		e.chaseTimeLimit = 5f;

		e.fleeDistance = 3f;
		e.fleeTimeLimit = 0.8f;

		e.exp = 50;
		e.money = 10;

		enemyData.Add("quill",e);
		Debug.Log(e.name+" is loaded");

		//Brute
		e = new EnemyValues();
		e.name = "brute";
		e.maxhp = 140;
		e.speed = new Vector2(1f,1f);

		e.waitTimeLimits = new RangedFloat(2.0f,3.0f);
		e.waitStates.Add(StateController.WaitStates.CHASE);

		e.meleeRange = 1.6f;
		e.attackRate = 1f;
		e.attacks = 1;
		e.meleeTimeStartup = 0.5f;
		e.meleeTimeAnimation = 0.5f;

		e.chaseTimeLimit = 0.8f;

		e.fleeDistance = 0.6f;
		e.fleeTimeLimit = 0.5f;

		e.exp = 20;
		e.money = 50;

		enemyData.Add("brute",e);
		Debug.Log(e.name+" is loaded");

		initialized = true;
	}
}
