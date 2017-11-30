using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class ScrObjListVariable : ScriptableObject {

	public string pathToLibrary;
	[SerializeField] private List<ScrObjLibraryEntry> list = new List<ScrObjLibraryEntry>();

	private Dictionary<string, ScrObjLibraryEntry> entries = new Dictionary<string, ScrObjLibraryEntry>();

	public void GenerateDictionary() {
		for (int i = 0; i < list.Count; i++) {
			entries.Add(list[i].uuid, list[i]);
		}
		if (list.Count != entries.Keys.Count)
			Debug.Log("One or more uuids are not unique!.");
		Debug.Log("Loaded " + list.Count + " entries into the " + pathToLibrary + " library");
	}

	public bool ContainsID(string id) {
		return entries.ContainsKey(id);
	}

	public ScrObjLibraryEntry GetEntry(string uuid) {
		return entries[uuid];
	}

	public List<string> GetKeys() {
		return entries.Keys.ToList();
	}

	public void AddEntry(ScrObjLibraryEntry obj) {
		list.Add(obj);
		entries.Add(obj.entryName, obj);
	}
}
