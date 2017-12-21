using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


	void RecalculateStats() {
		for (int i = 0; i < Inventory.instance.gearEquippedItems.Length; i++)
		{
			ItemEquip item = (ItemEquip)Inventory.instance.gearEquippedItems[i];
			if (item == null)
				return;

			playerAttack.value += item.attackModifier;
			playerDefense.value += item.defenseModifier;
			playerSAttack.value += item.sAttackModifier;
			playerSDefense.value += item.sDefenseModifier;
		}
	}
}
