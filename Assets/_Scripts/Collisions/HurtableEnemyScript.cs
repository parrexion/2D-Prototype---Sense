using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtableEnemyScript : MonoBehaviour {

	public EnemyGroup group;
	public Transform damageNumbers;

	void OnTriggerEnter2D(Collider2D otherCollider){

		Projectile projectile = otherCollider.gameObject.GetComponent<Projectile>();
		if (projectile != null) {

			if (!projectile.isEnemy) {
				if (!projectile.hitEnemies.Contains(group.enemyId)) {
					group.TakeDamage(projectile.damage);
					projectile.hitEnemies.Add(group.enemyId);

					if (projectile.maxHits != -1 && projectile.hitEnemies.Count >= projectile.maxHits) {
						Destroy(projectile.gameObject);
					}

					Transform t = Instantiate(damageNumbers);
					t.position = transform.position;
					DamageNumberDisplay dnd = t.GetComponent<DamageNumberDisplay>();
					dnd.damage = projectile.damage;
					MainControllerScript.instance.battleGUI.damages.Add(dnd);
//					Debug.Log("Hit");
				}
			}
//			else
//				Debug.Log ("Friendly");
		} 
		else
			Debug.Log("Null");
	}

	public void Die(){
		Transform t = Instantiate(group.deadEnemy);
		t.position = transform.position;
		t.localScale = transform.localScale;
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<StateController>().enabled = false;
		GetComponent<Rigidbody2D>().Sleep();
		GetComponent<Collider2D>().enabled = false;
		Destroy(gameObject,1/t.GetComponent<FadeAwayScript>().fadeDuration);
	}
}
