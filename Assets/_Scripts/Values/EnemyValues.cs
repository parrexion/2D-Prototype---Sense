using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyValues {

	public string name = "";
	public int maxhp = 1;
	public Vector2 speed = new Vector2(0f,0f);

	[Space(5)]

	[Header("AI Values")]
	public List<StateController.WaitStates> waitStates = new List<StateController.WaitStates>();
	[MinMaxRangeAttribute(0.1f,10.0f)]
	public RangedFloat waitTimeLimits = new RangedFloat(20f,30f);

	[Range(0.5f,20.0f)]
	public float chaseTimeLimit = 30f;

	[Range(1.0f,10.0f)]
	public float fleeDistance = 3f;
	[Range(0.5f,3.0f)]
	public float fleeTimeLimit = 30f;

	[Space(5)]

	[Header("Attacking")]
	[Range(0.5f,3.0f)]
	public float meleeRange = 1f;
	[Range(0.1f,10.0f)]
	public float attackRate = 1f;
	public int attacks = 1;
	public float meleeTimeStartup = 0f;
	public float meleeTimeAnimation = 1f;

	[Space(5)]

	[Header("Reward")]
	public int exp = 0;
	public int money = 0;
	//Add some kind of loot table
}
