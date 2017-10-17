using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Projectile class which is used by all kinds of projectiles and attacks.
/// Contains the information needed to deal damage and trigger effects.
/// </summary>
public class Projectile : MonoBehaviour {

	private bool active = true;
    public int damage = 1;
	public float attackScaling = 1f;
	public float time = 10;
	public bool isEnemy = false;
	public int maxHits = -1;
	public List<int> hitEnemies = new List<int>();
	public MoveScript move;

	private float currentTime = 0f;


	void Start() {
		move = GetComponent<MoveScript>();
	}

	/// <summary>
	/// Update the current lifetime of the projectile.
	/// </summary>
	void Update() {
		if (active) {
			currentTime += Time.deltaTime;

			if (currentTime >= time)
				Destroy(gameObject);
		}
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
	/// Enabling the movement of the projectile if there is something.
	/// </summary>
	/// <param name="state"></param>
	public void SetActive(bool state) {

		active = state;
		if (move) {
			move.active = state;
		}

	}
}
