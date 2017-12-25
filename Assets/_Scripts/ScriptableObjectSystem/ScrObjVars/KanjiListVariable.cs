using UnityEngine;

/// <summary>
/// Representative list of kanji.
/// </summary>
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
	public Kanji GetKanjiByUuid(string uuid) {
		for (int i = 0; i < values.Length; i++) {
			if (values[i].uuid == uuid){
				return values[i];
			}
		}

		Debug.Log("Failed to find kanji with uuid: " + uuid);
		return null;
	}

	//Saving and loading

	/// <summary>
	/// Generates a list of uuids in order to save the list to file.
	/// </summary>
	/// <returns></returns>
	public SaveListUuid GenerateSaveData() {
		SaveListUuid saveData = new SaveListUuid();
		int length = values.Length;
		saveData.size = length;
		saveData.uuids = new string[length];

		for (int i = 0; i < length; i++) {
			saveData.uuids[i] = (values[i] != null) ? values[i].uuid : "";
		}

		return saveData;
	}

	/// <summary>
	/// Loads a list of uuids into the kanji list.
	/// </summary>
	/// <param name="saveData"></param>
	public void LoadKanjiData(SaveListUuid saveData) {
		if (values.Length != saveData.size)
			Debug.LogWarning("Something is wrong with the size of the kanjilist.");
		for (int i = 0; i < saveData.size; i++) {
			values[i] = GetKanjiByUuid((saveData.uuids[i] != null) ? saveData.uuids[i] : null);
		}
		Debug.Log("Loaded the kanji list.");
	}
}
