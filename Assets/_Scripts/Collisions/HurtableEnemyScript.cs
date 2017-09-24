using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateController),typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer),typeof(Rigidbody2D))]
public class HurtableEnemyScript : MonoBehaviour {

	public EnemyGroup group;
	public Transform damageNumbers;

	private BattleGUIController battleGUI;
	private SpriteRenderer spriteRenderer;
	private StateController stateController;
	private Rigidbody2D rigid;
	private Collider2D collider2d;


	void Start(){
		battleGUI = MainControllerScript.instance.battleGUI;
		spriteRenderer = GetComponent<SpriteRenderer>();
		stateController = GetComponent<StateController>();
		rigid = GetComponent<Rigidbody2D>();
		collider2d = GetComponent<Collider2D>();
	}

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
					battleGUI.damages.Add(dnd);
				}
			}
		} 
		else
			Debug.Log("Null");
	}

	public void Die(){
		spriteRenderer.enabled = false;
		stateController.enabled = false;
		rigid.Sleep();
		collider2d.enabled = false;
		
		Transform t = Instantiate(group.deadEnemy);
		t.position = transform.position;
		t.localScale = transform.localScale;
		Destroy(gameObject,1/t.GetComponent<FadeAwayScript>().fadeDuration);
	}
}
