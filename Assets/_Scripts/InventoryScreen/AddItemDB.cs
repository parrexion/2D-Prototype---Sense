using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class which contains the information of all the kanji and items in the game.
/// Used to add items to the inventory.
/// </summary>
public class AddItemDB : MonoBehaviour {

	public Inventory inventory;
	public KanjiListVariable kanjiList;
	public ItemListVariable itemList;


	// Use this for initialization
	void Start () {
		inventory = Inventory.instance;
	}

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
		ItemKanji item = kanjiList.GetKanji(id).extractKanjiInformation(id);
		bool added;
		if (equip)
			added = inventory.AddEquip(item);
		else
			added = inventory.AddBag(item);
		if (!added)
			Destroy(item);
	}

	public void AddSpecificEquip(int id, bool equip){
		if (id >= itemList.values.Length)
			return;
		ItemEquip item = ScriptableObject.Instantiate(itemList.values[id]);
		bool added;
		if (equip)
			added = inventory.AddEquip(item);
		else
			added = inventory.AddBag(item);
		if (!added)
			Destroy(item);
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
