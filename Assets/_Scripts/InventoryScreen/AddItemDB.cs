﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class which contains the information of all the kanji and items in the game.
/// Used to add items to the inventory.
/// </summary>
public class AddItemDB : MonoBehaviour {

	Inventory inventory;
	KanjiList kanjiList;
	public ItemEquip[] equipList;


	// Use this for initialization
	void Start () {
		inventory = Inventory.instance;
		kanjiList = MainControllerScript.instance.kanjiList;
	}

	/// <summary>
	/// Add the given kanji to the first open slot to either the equip or other inventory.
	/// </summary>
	/// <param name="id"></param>
	/// <param name="equip"></param>
	public void AddSpecificKanji(int id, bool equip){
		if (id >= kanjiList.ListSize())
			return;
		ItemKanji item = kanjiList.GetKanji(id).extractKanjiInformation(id);
		bool added;
		if (equip)
			added = inventory.AddEquip(item);
		else
			added = inventory.Add(item);
		if (!added)
			Destroy(item);
	}

	public void AddSpecificEquip(int id, bool equip){
		if (id >= equipList.Length)
			return;
		ItemEquip item = ScriptableObject.Instantiate(equipList[id]);
		bool added;
		if (equip)
			added = inventory.AddEquip(item);
		else
			added = inventory.Add(item);
		if (!added)
			Destroy(item);
	}

	public void AddRandomKanji() {
		int r = Random.Range(1,kanjiList.ListSize());
		AddSpecificKanji(r,false);
	}

	public void AddRandomEquip() {
		int r = Random.Range(1,equipList.Length);
		AddSpecificEquip(r, false);
	}
}
