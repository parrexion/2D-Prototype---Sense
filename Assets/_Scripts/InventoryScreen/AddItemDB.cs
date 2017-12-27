using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class which is used to add items and kanji to the inventory.
/// </summary>
public class AddItemDB : MonoBehaviour {

	public InventoryHandler invItemHandler;
	public InventoryHandler invKanjiHandler;
	public ScrObjLibraryVariable itemLibrary;
	public ScrObjLibraryVariable kanjiLibrary;


	/// <summary>
	/// Adds the specified kanji to the inventory.
	/// </summary>
	/// <param name="id"></param>
	/// <param name="equip"></param>
	public void AddSpecificKanji(int id, bool equip){
		Kanji kanji = (Kanji)kanjiLibrary.GetEntryByIndex(id);
		AddKanji(kanji, equip);
	}

	/// <summary>
	/// Adds the specified item to the inventory.
	/// </summary>
	/// <param name="id"></param>
	/// <param name="equip"></param>
	public void AddSpecificEquip(int id, bool equip){
		ItemEquip item = (ItemEquip)itemLibrary.GetEntryByIndex(id);
		AddItem(item, equip);
	}

	/// <summary>
	/// Adds a random kanji to the bag.
	/// </summary>
	public void AddRandomKanji() {
		Kanji kanji = (Kanji)kanjiLibrary.GetRandomEntry();
		AddKanji(kanji, false);
	}

	/// <summary>
	/// Adds a random item to the bag.
	/// </summary>
	public void AddRandomEquip() {
		ItemEquip item = (ItemEquip)itemLibrary.GetRandomEntry();
		AddItem(item, false);
	}

	/// <summary>
	/// Adds the item to inventory.
	/// </summary>
	/// <param name="item"></param>
	/// <param name="equip"></param>
	void AddItem(ItemEquip item, bool equip) {
		bool added;
		if (equip)
			added = invKanjiHandler.AddEquip(item);
		else
			added = invKanjiHandler.AddBag(item);
		if (!added)
			Debug.Log("No room left to add item.");
	}

	/// <summary>
	/// Adds the kanji to the inventory.
	/// </summary>
	/// <param name="kanji"></param>
	/// <param name="equip"></param>
	void AddKanji(Kanji kanji, bool equip) {
		bool added;
		if (equip)
			added = invKanjiHandler.AddEquip(kanji);
		else
			added = invKanjiHandler.AddBag(kanji);
		if (!added)
			Debug.Log("No room left to add item.");
	}
}
