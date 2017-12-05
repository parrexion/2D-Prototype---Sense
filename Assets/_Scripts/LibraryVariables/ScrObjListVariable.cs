using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class ScrObjListVariable : ScriptableObject {

	[System.NonSerialized] private bool initialized = false;
	public string pathToLibrary;
	
	[SerializeField] private List<ScrObjLibraryEntry> list = new List<ScrObjLibraryEntry>();
	[SerializeField] private List<GUIContent> representations = new List<GUIContent>();
	private Dictionary<string, ScrObjLibraryEntry> entries = new Dictionary<string, ScrObjLibraryEntry>();

	public void GenerateDictionary() {
		entries.Clear();
		representations.Clear();
		for (int i = list.Count-1; i >= 0 ; i--) {
			entries.Add(list[i].uuid, list[i]);
			AddRepresentation(list[i], false);
		}
		if (list.Count != entries.Keys.Count)
			Debug.Log("One or more uuids are not unique!.");
		Debug.Log("Loaded " + list.Count + " entries into the " + pathToLibrary + " library");
		initialized = true;
	}

	public bool ContainsID(string id) {
		return entries.ContainsKey(id);
	}

	public ScrObjLibraryEntry GetEntry(string uuid) {
		if (!initialized)
			GenerateDictionary();

		if (entries.ContainsKey(uuid))
			return entries[uuid];
		Debug.Log("Could not find uuid: " + uuid);
		return null;
	}

	public ScrObjLibraryEntry GetEntryByIndex(int index) {
		return list[index];
	}

	public ScrObjLibraryEntry GetEntryBySelectedIndex(int index) {
		return list[list.Count - index - 1];
	}

	public ScrObjLibraryEntry GetRandomEntry() {
		return list[Random.Range(0,list.Count)];
	}

	public GUIContent[] GetRepresentations() {
		return representations.ToArray();
	}

	public void AddEntry(ScrObjLibraryEntry obj) {
		list.Add(obj);
		entries.Add(obj.entryName, obj);
		AddRepresentation(obj, true);
	}

	public void RemoveEntryByIndex(int index) {
		ScrObjLibraryEntry entry = GetEntryByIndex(index);
		list.RemoveAt(index);
		entries.Remove(entry.uuid);
		representations.RemoveAt(index);
	}

	private void AddRepresentation(ScrObjLibraryEntry entry, bool insert) {
		GUIContent content = new GUIContent();
		content.text = entry.uuid;
		Texture2D tex;
		if (entry.repColor.a != 0){
			tex = GenerateColorTexture(entry.repColor);
		}
		else {
			tex = GenerateRandomColor();
		}
		content.image = tex;
		if (insert) {
			Debug.Log("Inserted");
			representations.Insert(0,content);
		}
		else
			representations.Add(content);
	}

	Texture2D GenerateRandomColor() {
		Color c = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
		return GenerateColorTexture(c);
	}

	public Texture2D GenerateColorTexture(Color c) {
		int size = 32;
		Texture2D tex = new Texture2D(size,size);
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				tex.SetPixel(i,j,c);
			}
		}
		tex.Apply();
		return tex;
	}

	public Sprite TextureToSprite(Texture2D tex) {
		return Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
	}
}
