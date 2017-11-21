using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScreenValues : MonoBehaviour {

	public bool wonBattle;
	public bool lostBattle;
	public float time;
	public FloatVariable normalHealth;
	public FloatVariable spiritHealth;
	public FloatVariable maxHealth;
	public int noEnemies;
	public List<string> enemiesDefeated;
	public int exp;
	public int money;
	public List<string> treasures;
}
