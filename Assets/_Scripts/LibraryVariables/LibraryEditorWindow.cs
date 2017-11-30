using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LibraryEditorWindow : EditorWindow {

	public ScrObjListVariable characterLibrary;

	public CharacterEntry charPrefab;

	Vector2 scrollPos;
	string charName;
	int selCharacter;


	[MenuItem("Window/LibraryEditor")]
	public static void ShowWindow() {
		GetWindow<LibraryEditorWindow>("Library Editor");
	}

	void OnEnable() {
		EditorSceneManager.sceneOpened += SceneOpenedCallback;
		LoadLibrary();
	}

	void OnDisable() {
		EditorSceneManager.sceneOpened -= SceneOpenedCallback;
	}

	void SceneOpenedCallback(Scene _scene, OpenSceneMode _mode) {
		Debug.Log("SCENE LOADED");
		InitializeWindow();
	}


	void LoadLibrary() {

		Debug.Log("Loading libraries...");

		characterLibrary.GenerateDictionary();

		Debug.Log("Finished loading libraries");

		InitializeWindow();
	}

	void InitializeWindow() {
		
	}


	void OnGUI() {

		GUILayout.Label("Character Editor", EditorStyles.boldLabel);

		DrawEntryList();

	}

	void DrawEntryList() {
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(200), GUILayout.Height(200));
		List<string> keys = characterLibrary.GetKeys();
		for (int i = 0; i < keys.Count; i++) {
			GUILayout.Label(keys[i]);
		}
		//selCharacter = GUILayout.SelectionGrid(selCharacter, new Texture[1], 1);
		EditorGUILayout.EndScrollView();

		charName = EditorGUILayout.TextField("Character Name", charName);
		if (GUILayout.Button("Create new")) {
			InstansiateCharacter();
		}
	}

	void InstansiateCharacter() {
		if (characterLibrary.ContainsID(charName)) {
			Debug.Log("uuid already exists!");
			return;
		}
		CharacterEntry c = CreateInstance<CharacterEntry>();
		c.name = charName;
		c.uuid = charName;
		c.entryName = charName;
		string path = "Assets/LibraryData/Characters/" + charName + ".asset";

		AssetDatabase.CreateAsset(c, path);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		characterLibrary.AddEntry(c);
	}
}
