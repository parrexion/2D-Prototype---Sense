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
	// Internal representation
	private float _playerMaxHealth;
	private float _playerAttack;
	private float _playerDefense;
	private float _playerSAttack;
	private float _playerSDefense;

	[Header("Battle Values")]
	public FloatVariable spiritDamageTaken;
	public FloatVariable normalDamageTaken;

	[Header("Overworld values")]
	public StringVariable playerArea;
	public FloatVariable playerPosX;
	public FloatVariable playerPosY;

	[Header("Level")]
	public IntVariable playerLevel;
	public IntVariable expTotal;

	[Header("Inventory")]
	public IntVariable money;
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
		CalculateBaseHealth();
		_playerAttack = 0;
		_playerDefense = 0;
		_playerSAttack = 0;
		_playerSDefense = 0;
	}

	void CalculateBaseHealth() {
		_playerMaxHealth = Constants.PLAYER_HEALTH_BASE + playerLevel.value * Constants.PLAYER_HEALTH_SCALE;
	}

	/// <summary>
	/// Recalculates the player's stats using the current equipment.
	/// </summary>
	public void RecalculateStats() {
		Debug.Log("Recalculating stats!");
		
		ResetPlayerStats();

		// Start by adding upp all base stats
		for (int i = 0; i < invItemEquip.values.Length; i++) {
			ItemEquip item = (ItemEquip)invItemEquip.values[i];
			if (item == null)
				continue;

			_playerMaxHealth += item.healthModifier;
			_playerAttack += item.attackModifier;
			_playerDefense += item.defenseModifier;
			_playerSAttack += item.sAttackModifier;
			_playerSDefense += item.sDefenseModifier;
		}

		// Add percent modifiers
		for (int i = 0; i < invItemEquip.values.Length; i++) {
			ItemEquip item = (ItemEquip)invItemEquip.values[i];
			if (item == null)
				continue;

			for (int mod = 0; mod < item.percentModifiers.Count; mod++) {
				AddPercentValue(item.percentModifiers[mod].affectedStat, item.percentModifiers[mod].percentValue);
			}
		}

		// Apply changes
		playerMaxHealth.value = (int)(_playerMaxHealth + 0.5f);
		playerAttack.value = (int)(_playerAttack + 0.5f);
		playerDefense.value = (int)(_playerDefense + 0.5f);
		playerSAttack.value = (int)(_playerSAttack + 0.5f);
		playerSDefense.value = (int)(_playerSDefense + 0.5f);
	}

	void AddPercentValue(StatsPercentModifier.Stat stat, float multiplier) {
		switch(stat)
		{
			case StatsPercentModifier.Stat.ATTACK:
				_playerAttack *= multiplier;
				break;
			case StatsPercentModifier.Stat.DEFENSE:
				_playerDefense *= multiplier;
				break;
			case StatsPercentModifier.Stat.MAXHEALTH:
				_playerMaxHealth *= multiplier;
				break;


			case StatsPercentModifier.Stat.SATTACK:
				_playerSAttack *= multiplier;
				break;
			case StatsPercentModifier.Stat.SDEFENSE:
				_playerSDefense *= multiplier;
				break;
		}
	}
}
