using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Inventory/KanjiItem")]
public class ItemKanji : Item {

	public enum KanjiType {
		CLICK,SLASH,RISE,HOLD,DOWN,OTHER
	}
	public KanjiBaseClass kanji;
	public KanjiType type = KanjiType.OTHER;
	public int damage;
	public int charges;
	public float rechargeTime; 
}
