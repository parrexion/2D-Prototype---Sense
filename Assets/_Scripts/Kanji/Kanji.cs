using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class which contains all information used by a kanji.
/// Contains activation requirements, effects and stats.
/// </summary>
[CreateAssetMenu (menuName = "Library/Kanji")]
public class Kanji : ItemEntry {

	public KanjiActivation[] activations;
	public KanjiEffect[] effects;
	public KanjiValues values;


	/// <summary>
	/// Checks if all activation requirements are fullfilled.
	/// </summary>
	/// <param name="info"></param>
	/// <returns></returns>
	public bool CanActivate(MouseInformation info) {

		for (int i = 0; i < activations.Length; i++) {
			if (!activations[i].CanActivate(values, info))
				return false;
		}

		return true;
	}

	/// <summary>
	/// Creates the effect of the kanji.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="attackValue"></param>
	public void CreateEffects(MouseInformation info, int attackValue){
		for (int i = 0; i < effects.Length; i++) {
			effects[i].Use(values, attackValue, info);
		}
	}

}
