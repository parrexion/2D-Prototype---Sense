using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtablePlayerScript : MonoBehaviour {

	private PlayerStats playerStats;
	public bool normalPlayer;
	public bool beenHurt = false;

	void Start() {
		playerStats = PlayerStats.instance;
	}

	void OnTriggerEnter2D(Collider2D otherCollider){

		Projectile projectile = otherCollider.gameObject.GetComponent<Projectile>();
		if (projectile != null) {

			if (projectile.isEnemy) {
				playerStats.TakeDamage(normalPlayer, projectile.damage);
				Destroy(projectile.gameObject);
				//Debug.Log ("Ouch");
			}
//			else
//				Debug.Log ("Friendly");
		} 
		else
			Debug.Log ("Null");
	}
}
