using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEntry : ScrObjLibraryEntry {

	public enum ItemType {KANJI,EQUIP,MISC,DESTROY}
	public ItemType item_type;
	public Sprite icon = null;

}
