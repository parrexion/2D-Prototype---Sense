using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class which contains the information of all the kanji and items in the game.
/// Used to add items to the inventory.
/// </summary>
public class AddItemDB : MonoBehaviour {

	public InventoryHandler inventoryHandler;
	public ItemListVariable itemList;
	public KanjiListVariable kanjiList;


	/// <summary>
	/// Add the given kanji to the first open slot to either the equip or other inventory.
	/// </summary>
	/// <param name="id"></param>
	/// <param name="equip"></param>
	public void AddSpecificKanji(int id, bool equip){
		if (id >= kanjiList.values.Length){
			Debug.LogWarning("Kanji index " + id + " does not exist!");
			return;
		}
		Kanji kanji = kanjiList.GetKanji(id);
		bool added;
		if (equip)
			added = inventoryHandler.AddEquip(kanji);
		else
			added = inventoryHandler.AddBag(kanji);
		if (!added)
			Debug.Log("No room left to add kanji.");
	}

	public void AddSpecificEquip(int id, bool equip){
		if (id >= itemList.values.Length)
			return;
		ItemEquip item = itemList.GetItem(id);
		bool added;
		if (equip)
			added = inventoryHandler.AddEquip(item);
		else
			added = inventoryHandler.AddBag(item);
		if (!added)
			Debug.Log("No room left to add item.");
	}

	public void AddRandomKanji() {
		int r = Random.Range(1,kanjiList.values.Length);
		AddSpecificKanji(r,false);
	}

	public void AddRandomEquip() {
		int r = Random.Range(1,itemList.values.Length);
		AddSpecificEquip(r, false);
	}
}
