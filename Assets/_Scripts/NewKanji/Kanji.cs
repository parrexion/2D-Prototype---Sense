using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Kanji/Kanji")]
public class Kanji : ScriptableObject {

	public KanjiActivation[] activations;
	public KanjiEffect[] effects;
	public KanjiValues values;


	public bool CanActivate(MouseInformation info) {

		for (int i = 0; i < activations.Length; i++) {
			if (!activations[i].CanActivate(values, info))
				return false;
		}

		return true;
	}

	public void CreateEffects(MouseInformation info){
		for (int i = 0; i < effects.Length; i++) {
			effects[i].Use(values, info);
		}
	}
}
