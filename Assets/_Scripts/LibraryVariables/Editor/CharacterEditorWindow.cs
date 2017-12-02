using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CharacterEditorWindow {

	public ScrObjListVariable characterLibrary;

	public CharacterEntry charPrefab;

	// Display screen
	Rect dispRect = new Rect();
	Texture2D dispTex;
	string entryName = "";
	Texture2D entryImage;

	// Selection screen
	Rect selectRect = new Rect();
	Texture2D selectTex;
	Vector2 scrollPos;
	string charName;
	int selCharacter;

	public CharacterEditorWindow(ScrObjListVariable entries, CharacterEntry prefab){
		characterLibrary = entries;
		charPrefab = prefab;
	}

	public void LoadLibrary() {

		Debug.Log("Loading character libraries...");

		characterLibrary.GenerateDictionary();

		Debug.Log("Finished loading character libraries");

		InitializeWindow();
	}

	public void InitializeWindow() {
		dispTex = new Texture2D(1, 1);
		dispTex.SetPixel(0, 0, new Color(0.8f, 0.5f, 0.2f));
		dispTex.Apply();

		selectTex = new Texture2D(1, 1);
		selectTex.SetPixel(0, 0, new Color(0.8f, 0.8f, 0.8f));
		selectTex.Apply();
	}


	public void DrawWindow() {

		GUILayout.Label("Character Editor", EditorStyles.boldLabel);

		GenerateAreas();
		DrawBackgrounds();
		DrawEntryList();
	}

	void GenerateAreas() {

		selectRect.x = 0;
		selectRect.y = 50;
		selectRect.width = 200;
		selectRect.height = Screen.height - 50;

		dispRect.x = 200;
		dispRect.y = 50;
		dispRect.width = Screen.width - 200;
		dispRect.height = Screen.height - 50;
	}

	void DrawBackgrounds() {
		GUI.DrawTexture(selectRect, selectTex);
		GUI.DrawTexture(dispRect, dispTex);
	}

	void DrawEntryList() {
		GUILayout.BeginArea(selectRect);

		scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(selectRect.width), 
																GUILayout.Height(selectRect.height-75));
		// List<string> keys = characterLibrary.GetKeys();
		// for (int i = 0; i < keys.Count; i++) {
		// 	GUILayout.Label(keys[i]);
		// }
		selCharacter = GUILayout.SelectionGrid(selCharacter, characterLibrary.GetRepresentations(),1);
		EditorGUILayout.EndScrollView();

		charName = EditorGUILayout.TextField("Character Name", charName);
		if (GUILayout.Button("Create new")) {
			InstansiateCharacter();
		}

		GUILayout.EndArea();
	}

	void DrawDisplayWindow() {
		GUILayout.Label("Selected Character", EditorStyles.boldLabel);
	}

	void InstansiateCharacter() {
		if (characterLibrary.ContainsID(charName)) {
			Debug.Log("uuid already exists!");
			return;
		}
		CharacterEntry c = Editor.CreateInstance<CharacterEntry>();
		c.name = charName;
		c.uuid = charName;
		c.entryName = charName;
		string path = "Assets/LibraryData/Characters/" + charName + ".asset";

		AssetDatabase.CreateAsset(c, path);
		characterLibrary.AddEntry(c);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

	}
}
