using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KanjiEffect : ScriptableObject {


	abstract public bool Use(KanjiValues values, MouseInformation info);
}
