using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats {

	#region Singleton
	public static PlayerStats instance;

	protected override void Awake() {
		if (instance != null) {
			Destroy(gameObject);
		}
		else {
			base.Awake();
			instance = this;
		}
	}
	#endregion

	public BattleController battleController;
	public int spiritDamageTaken { get; private set; }
	public int normalDamageTaken { get; private set; }

	// Use this for initialization
	void Start () {
		Inventory.instance.onEquipmentChangedCallback += OnEquipmentChanged;
	}

	void OnDisable(){
		Inventory.instance.onEquipmentChangedCallback -= OnEquipmentChanged;
	}

	void OnEquipmentChanged(ItemEquip newItem, ItemEquip oldItem) {
		if (newItem != null) {
			attack.AddAddModifier(newItem.attackModifier);
			defense.AddAddModifier(newItem.defenseModifier);

			sAttack.AddAddModifier(newItem.sAttackModifier);
			sDefense.AddAddModifier(newItem.sDefenseModifier);
		}

		if (oldItem != null) {
			attack.RemoveAddModifier(oldItem.attackModifier);
			defense.RemoveAddModifier(oldItem.defenseModifier);

			sAttack.RemoveAddModifier(oldItem.sAttackModifier);
			sDefense.RemoveAddModifier(oldItem.sDefenseModifier);
		}
	}

	public override int TakeDamage(bool normal, int damage) {
		int dmg = base.TakeDamage(normal, damage);
		if (normal)
			normalDamageTaken += dmg;
		else
			spiritDamageTaken += dmg;
		return dmg;
	}


	public override void Die() {
		base.Die();
		battleController.GameOverTrigger();
	}
}
