using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Inventory/EquipItem")]
public class ItemEquip : Item {

	public int attackModifier;      // Increase/decrease in damage
	public int defenseModifier;		// Increase/decrease in armor


	public int sAttackModifier;
	public int sDefenseModifier;
}
