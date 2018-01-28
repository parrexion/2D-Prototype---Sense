using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScreenValues : MonoBehaviour {

	public StringVariable wonBattleState;
	public FloatVariable time;
	public FloatVariable normalHealth;
	public FloatVariable spiritHealth;
	public IntVariable maxHealth;
	public IntVariable noEnemies;

	[Header("Loot")]
	public IntVariable exp;
	public IntVariable money;
}
