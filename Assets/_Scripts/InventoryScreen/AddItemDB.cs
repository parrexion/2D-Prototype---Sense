﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemDB : MonoBehaviour {

	Inventory inventory;
	public ItemKanji[] kanjiList;
	public ItemEquip[] equipList;

	// Use this for initialization
	void Start () {
		inventory = Inventory.instance;
	}

	public void AddSpecificKanji(int id, bool equip){
		if (id >= kanjiList.Length)
			return;
		ItemKanji item = ScriptableObject.Instantiate(kanjiList[id]);
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
		int r = Random.Range(1,kanjiList.Length);
		AddSpecificKanji(r,false);
	}

	public void AddRandomEquip() {
		int r = Random.Range(1,equipList.Length);
		AddSpecificEquip(r, false);
	}
}
