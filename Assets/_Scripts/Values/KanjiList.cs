using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanjiList : MonoBehaviour {


	//public List<KanjiBaseClass> kanjiList;
	[SerializeField]
	private Kanji[] newKanjiList;


	/// <summary>
	/// Returns the kanji representation with activations, projectiles and values etc...
	/// Takes the index for the position in the kanji list.
	/// </summary>
	/// <param name="index"></param>
	/// <returns></returns>
	public Kanji GetKanji(int index) {
		if (index >= newKanjiList.Length || index < 0)
			index = 0;

		return newKanjiList[index];
	}

	/// <summary>
	/// Returns the kanji representation with activations, projectiles and values etc...
	/// Searches on the name of the kanji.
	/// </summary>
	/// <param name="index"></param>
	/// <returns></returns>
	public int GetKanjiIndex(string kanjiName) {
		if (kanjiName == "")
			return 0;

		for (int i = 0; i < newKanjiList.Length; i++) {
			if (newKanjiList[i].values.kanjiName.ToLower() == kanjiName.ToLower()){
				return i;
			}
		}
		return 0;
	}

	/// <summary>
	/// The number of different kanji stored in the list.
	/// </summary>
	/// <returns></returns>
	public int ListSize(){
		return newKanjiList.Length;
	}
}
