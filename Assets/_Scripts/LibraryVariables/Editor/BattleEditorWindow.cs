using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class BattleEditorWindow {

	public ScrObjListVariable battleLibrary;
	public BattleEntry battlePrefab;

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

	public BattleEditorWindow(ScrObjListVariable entries, BattleEntry prefab){
		battleLibrary = entries;
		battlePrefab = prefab;
	}

	public void LoadLibrary() {

		Debug.Log("Loading character libraries...");

		battleLibrary.GenerateDictionary();

		Debug.Log("Finished loading character libraries");

		InitializeWindow();
	}

	public void InitializeWindow() {
		dispTex = new Texture2D(1, 1);
		dispTex.SetPixel(0, 0, new Color(0.3f, 0.6f, 0.4f));
		dispTex.Apply();

		selectTex = new Texture2D(1, 1);
		selectTex.SetPixel(0, 0, new Color(0.8f, 0.8f, 0.8f));
		selectTex.Apply();
	}


	public void DrawWindow() {

		GUILayout.Label("Battle Editor", EditorStyles.boldLabel);

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
		// List<string> keys = battleLibrary.GetKeys();
		// for (int i = 0; i < keys.Count; i++) {
		// 	GUILayout.Label(keys[i]);
		// }
		selCharacter = GUILayout.SelectionGrid(selCharacter, battleLibrary.GetRepresentations(),1);
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
		if (battleLibrary.ContainsID(charName)) {
			Debug.Log("uuid already exists!");
			return;
		}
		CharacterEntry c = Editor.CreateInstance<CharacterEntry>();
		c.name = charName;
		c.uuid = charName;
		c.entryName = charName;
		string path = "Assets/LibraryData/Characters/" + charName + ".asset";

		AssetDatabase.CreateAsset(c, path);
		battleLibrary.AddEntry(c);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

	}

}
