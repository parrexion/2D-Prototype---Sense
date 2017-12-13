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
	public FloatVariable spiritDamageTaken;
	public FloatVariable normalDamageTaken;
	public FloatVariable playerPosX;
	public FloatVariable playerPosY;

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
			normalDamageTaken.value += dmg;
		else
			spiritDamageTaken.value += dmg;
		return dmg;
	}


	public override void Die() {
		base.Die();
		battleController.GameOverTrigger();
	}
}
