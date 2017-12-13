using UnityEngine;

[CreateAssetMenu(menuName="List ScrObj Variables/Kanji List Variable")]
public class KanjiListVariable : ScriptableObject {
	public Kanji[] values;

	
	/// <summary>
	/// Returns the kanji representation with activations, projectiles and values etc...
	/// Takes the index for the position in the kanji list.
	/// </summary>
	/// <param name="index"></param>
	/// <returns></returns>
	public Kanji GetKanji(int index) {
		if (index >= values.Length || index < 0) {
			Debug.Log("Index is out of bounds! " + index);
			return null;
		}

		return values[index];
	}
	
	/// <summary>
	/// Returns the kanji representation with activations, projectiles and values etc...
	/// Searches on the name of the kanji.
	/// </summary>
	/// <param name="index"></param>
	/// <returns></returns>
	public Kanji GetKanjiByName(string kanjiName) {
		for (int i = 0; i < values.Length; i++) {
			if (values[i].values.kanjiName.ToLower() == kanjiName.ToLower()){
				return values[i];
			}
		}

		Debug.Log("Failed to find kanji with name: " + kanjiName);
		return null;
	}

	/// <summary>
	/// Returns the kanji representation with activations, projectiles and values etc...
	/// Searches on the name of the kanji.
	/// </summary>
	/// <param name="index"></param>
	/// <returns></returns>
	public int GetKanjiIndex(string kanjiName) {
		if (kanjiName == "") {
			Debug.Log("Can't search on an empty string!");
			return -1;
		}

		for (int i = 0; i < values.Length; i++) {
			if (values[i].values.kanjiName.ToLower() == kanjiName.ToLower()){
				return i;
			}
		}

		Debug.Log("Failed to find kanji with name: " + kanjiName);
		return -1;
	}

}
