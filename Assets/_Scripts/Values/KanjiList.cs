using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanjiList : MonoBehaviour {


	//public List<KanjiBaseClass> kanjiList;
	[SerializeField]
	private Kanji[] newKanjiList;


	/// <summary>
	/// Returns the kanji representation with activations, projectiles and values etc...
	/// </summary>
	/// <param name="index"></param>
	/// <returns></returns>
	public Kanji GetKanji(int index) {
		if (index >= newKanjiList.Length || index < 0)
			index = 0;

		return newKanjiList[index];
	}
}
