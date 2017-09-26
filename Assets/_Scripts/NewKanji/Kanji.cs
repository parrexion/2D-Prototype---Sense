using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Kanji/Kanji")]
public class Kanji : ScriptableObject {

	public KanjiActivation[] activations;
	public KanjiEffect[] effects;
	public KanjiValues values;
}
