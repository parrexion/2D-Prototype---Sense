using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Kanji/Kanji")]
public class Kanji : ScriptableObject {

	public KanjiActivation[] activations;
	public KanjiEffect[] effects;
	public KanjiValues values;


	public bool CanActivate(MouseInformation info) {

		for (int i = 0; i < activations.Length; i++) {
			if (!activations[i].CanActivate(values, info))
				return false;
		}

		return true;
	}

	public void CreateEffects(MouseInformation info){
		for (int i = 0; i < effects.Length; i++) {
			effects[i].Use(values, info);
		}
	}

	/// <summary>
	/// Create a simpler version of the values to be used in the inventory.
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public ItemKanji extractKanjiInformation(int id){
		ItemKanji item = ScriptableObject.CreateInstance("ItemKanji") as ItemKanji;

		item.charges = values.maxCharges;
		item.damage = values.damage;
		item.icon = values.icon;
		item.item_id = id;
		item.item_name = values.kanjiName;
		item.item_type = Item.ItemType.KANJI;
		item.rechargeTime = values.cooldown;

		return item;
	}
}
