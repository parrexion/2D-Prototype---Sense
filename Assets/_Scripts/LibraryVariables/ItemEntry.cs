using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEntry : ScrObjLibraryEntry {

	public enum ItemType {KANJI,EQUIP,MISC,DESTROY}
	public ItemType item_type;
	public Sprite icon = null;
	public Color tintColor = Color.white;

	/// <summary>
	/// Copies the values from another entry.
	/// </summary>
	/// <param name="other"></param>
	public override void CopyValues(ScrObjLibraryEntry other) {
		base.CopyValues(other);
		ItemEntry item = (ItemEntry)other;

		item_type = item.item_type;
		icon = item.icon;
		tintColor = item.tintColor;
	}
}
