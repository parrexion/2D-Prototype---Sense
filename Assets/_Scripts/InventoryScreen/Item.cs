﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Inventory/Item")]
public class Item : ScriptableObject {

	public string uuid = System.Guid.NewGuid().ToString();
	public string entity_name;
	
	public enum ItemType {KANJI,EQUIP,MISC,DESTROY}
	public ItemType item_type;
	public Sprite icon = null;

}
