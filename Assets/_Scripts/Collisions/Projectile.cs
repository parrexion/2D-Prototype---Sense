using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	void Update() {
		if (active) {
			currentTime += Time.deltaTime;

			if (currentTime >= time)
				Destroy(gameObject);
		}
	}

	public void SetDamage(int attackValue){
		Debug.Log("Added some damage: " + (int)(attackScaling*attackValue));
		damage += (int)(attackScaling*attackValue);
	}


	public void SetActive(bool state) {

		active = state;
		if (move) {
			move.active = state;
//			Debug.Log("Move active = " + move.active);
		}
//		else
//			Debug.Log("Move active = is null");
	}
}
