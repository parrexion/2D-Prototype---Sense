using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Projectile class which is used by all kinds of projectiles and attacks.
/// Contains the information needed to deal damage and trigger effects.
/// </summary>
public class Projectile : Effect {

    public int damage = -1;
	public bool isEnemy = false;
	public int maxHits = -1;
	public List<int> hitEnemies = new List<int>();


	/// <summary>
	/// Set the damage for the projectile.
	/// </summary>
	/// <param name="attackValue"></param>
	public void SetDamage(int baseDamage, int attackValue, float damageScale){
		// Debug.Log("Added some damage: " + (int)(damageScale*attackValue));
		damage = baseDamage + (int)(damageScale*attackValue);
	}
}
