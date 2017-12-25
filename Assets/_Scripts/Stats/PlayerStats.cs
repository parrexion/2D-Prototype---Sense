using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class which keeps a reference to the player's stats throughout the game.
/// Can also recalculate the stats when needed.
/// </summary>
public class PlayerStats : MonoBehaviour {

	#region Singleton
	private static PlayerStats instance;

	protected void Awake() {
		if (instance != null) {
			Destroy(gameObject);
		}
		else {
			DontDestroyOnLoad(gameObject);
			instance = this;
		}
	}
	#endregion

	[Header("Player Stats")]
	public IntVariable playerMaxHealth;
	public IntVariable playerAttack;
	public IntVariable playerDefense;
	public IntVariable playerSAttack;
	public IntVariable playerSDefense;

	[Header("Battle Values")]
	public FloatVariable spiritDamageTaken;
	public FloatVariable normalDamageTaken;

	[Header("Overworld values")]
	public StringVariable playerArea;
	public FloatVariable playerPosX;
	public FloatVariable playerPosY;

	[Header("Inventory")]
	public InvListVariable invItemEquip;
	public InvListVariable invItemBag;
	public InvListVariable invKanjiEquip;
	public InvListVariable invKanjiBag;

	void Start() {
		RecalculateStats();
	}

	/// <summary>
	/// Resets the player's stats.
	/// </summary>
	void ResetPlayerStats() {
		playerAttack.value = 0;
		playerDefense.value = 0;
		playerSAttack.value = 0;
		playerSDefense.value = 0;
	}

	/// <summary>
	/// Recalculates the player's stats using the current equipment.
	/// </summary>
	public void RecalculateStats() {
		Debug.Log("Recalculating stats!");
		
		ResetPlayerStats();

		for (int i = 0; i < invItemEquip.values.Length; i++)
		{
			ItemEquip item = (ItemEquip)invItemEquip.values[i];
			if (item == null)
				continue;

			playerAttack.value += item.attackModifier;
			playerDefense.value += item.defenseModifier;
			playerSAttack.value += item.sAttackModifier;
			playerSDefense.value += item.sDefenseModifier;
		}
	}
}
