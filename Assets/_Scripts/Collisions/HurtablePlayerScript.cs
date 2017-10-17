using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Collider for the players which takes damage from enemy projectiles.
/// </summary>
public class HurtablePlayerScript : MonoBehaviour {

	private PlayerStats playerStats;
	public bool normalPlayer;
	public bool beenHurt = false;


	void Start() {
		playerStats = PlayerStats.instance;
	}

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
			playerStats.TakeDamage(normalPlayer, projectile.damage);
			Destroy(projectile.gameObject);
		}
	}
}
