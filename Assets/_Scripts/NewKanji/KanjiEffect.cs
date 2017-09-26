using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KanjiEffect : ScriptableObject {


	abstract public bool Use(WeaponSlot slot, KanjiValues values, MouseInformation info);
}
