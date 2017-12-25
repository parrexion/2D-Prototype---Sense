using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Library/Equipment")]
public class ItemEquip : ItemEntry {

	public int attackModifier;      // Increase/decrease in damage
	public int defenseModifier;		// Increase/decrease in defense

	public int sAttackModifier;
	public int sDefenseModifier;

	// List of additional effects
}
