using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class ScrObjListVariable : ScriptableObject {

	public string pathToLibrary;
	
	[SerializeField] private List<ScrObjLibraryEntry> list = new List<ScrObjLibraryEntry>();
	[SerializeField] private List<GUIContent> representations = new List<GUIContent>();
	private Dictionary<string, ScrObjLibraryEntry> entries = new Dictionary<string, ScrObjLibraryEntry>();
	
	Texture2D tex;

	public void GenerateDictionary() {
		entries.Clear();
		representations.Clear();
		tex = new Texture2D(1,1);
		for (int i = 0; i < list.Count; i++) {
			entries.Add(list[i].uuid, list[i]);
			AddRepresentation(list[i]);
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

	public GUIContent[] GetRepresentations() {
		return representations.ToArray();
	}

	public void AddEntry(ScrObjLibraryEntry obj) {
		list.Add(obj);
		entries.Add(obj.entryName, obj);
		AddRepresentation(obj);
	}

	private void AddRepresentation(ScrObjLibraryEntry entry) {
		GUIContent con = new GUIContent();
		con.text = entry.entryName;
		tex.SetPixel(0,0,Random.ColorHSV());
		tex.Apply();
		con.image = tex;
		representations.Add(con);
	}
}
