using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Inventory/Item")]
public class Item : ScriptableObject {

	public enum ItemType {KANJI,EQUIP,MISC,DESTROY}
	public ItemType item_type;
	public int item_id;
	public string item_name;
	public Sprite icon = null;

}
