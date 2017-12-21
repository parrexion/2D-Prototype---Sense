using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Collider for the players which takes damage from enemy projectiles.
/// </summary>
public class HurtablePlayerScript : HurtableBaseScript {

	public IntVariable playerMaxHealth;
	public FloatVariable damageTaken;
	public FloatVariable otherDamageTaken;
	public IntVariable playerDefense;

	public UnityEvent playerDiedEvent;
	public UnityEvent takenDamageEvent;

	public bool canBeHurt = true;


	/// <summary>
	/// When hit by an enemy projectile, take damage and remove it.
	/// </summary>
	/// <param name="otherCollider"></param>
	void OnTriggerEnter2D(Collider2D otherCollider){

		Projectile projectile = otherCollider.gameObject.GetComponent<Projectile>();
		if (projectile == null) {
			Debug.Log("Null");
			return;
		}

		if (projectile.isEnemy) {
			Destroy(projectile.gameObject);

			if (!canBeHurt)
				return;

			defense = playerDefense.value;
			int dmg = TakeDamage(projectile.damage);
			damageTaken.value += dmg;
			if (dmg > 0)
				takenDamageEvent.Invoke();

			if (damageTaken.value + otherDamageTaken.value >= playerMaxHealth.value)
				Die();
		}
	}

	public override void Die() {
		base.Die();
		playerDiedEvent.Invoke();
	}
}
