using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

	public Stats maxHealth;
	public int currentHealth { get; protected set;}

	public Stats attack;
	public Stats defense;

	public Stats sAttack;
	public Stats sDefense;

	public BoolVariable invincible;


	protected virtual void Awake() {
		currentHealth = maxHealth.GetValue();
		Debug.Log("Health: "+currentHealth);
	}

	public float GetPercentHealth(){
		return (float)currentHealth/(float)maxHealth.GetValue();
	}

	public virtual int TakeDamage(bool normal, int damage) {

		if (invincible.value){
			Debug.Log("I'm invincible!!");
			return 0;
		}

		damage -= defense.GetValue();
		damage = Mathf.Max(0,damage);

		currentHealth -= damage;
		// Debug.Log("Took "+damage+" damage. HP left: " + currentHealth);

		if (currentHealth <= 0) {
			Die();
		}
		return damage;
	}

	public virtual int Heal(int heal){
		int earlier = currentHealth;
		currentHealth += heal;
		currentHealth = Mathf.Clamp(currentHealth,0,maxHealth.GetValue());
		Debug.Log("Healed "+heal+" damage");

		return heal;
	}
	
	public virtual void Die(){
		//Implement for what should happen when
		//the character dies.
		Debug.Log("The character died!");
	}
}
