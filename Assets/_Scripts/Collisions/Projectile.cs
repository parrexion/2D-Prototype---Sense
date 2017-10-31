using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Projectile class which is used by all kinds of projectiles and attacks.
/// Contains the information needed to deal damage and trigger effects.
/// </summary>
public class Projectile : Effect {

    public int damage = 1;
	public float attackScaling = 1f;
	public bool isEnemy = false;
	public int maxHits = -1;
	public List<int> hitEnemies = new List<int>();
	public MoveScript move;


	void Start() {
		move = GetComponent<MoveScript>();
	}

	/// <summary>
	/// Set the damage for the projectile.
	/// </summary>
	/// <param name="attackValue"></param>
	public void SetDamage(int attackValue){
		Debug.Log("Added some damage: " + (int)(attackScaling*attackValue));
		damage += (int)(attackScaling*attackValue);
	}

	/// <summary>
	/// Enabling the movement of the projectile if there is any.
	/// </summary>
	/// <param name="state"></param>
	public override void SetActive(bool state) {
		base.SetActive(state);
		if (move) {
			move.active = state;
		}
	}
}
