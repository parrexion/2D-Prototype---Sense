using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CharacterEditorWindow {

	public ScrObjListVariable characterLibrary;
	public CharacterEntry charPrefab;

	// Selection screen
	Rect selectRect = new Rect();
	Texture2D selectTex;
	Vector2 scrollPos;
	int selCharacter = -1;

	// Display screen
	Rect dispRect = new Rect();
	RectOffset dispOffset = new RectOffset();
	Texture2D dispTex;
	string charUuid = "";
	Color charRepColor = new Color();
	string charName = "";
	Texture2D entryImage;

	//Creation
	string uuid = "";
	Color repColor = new Color(0.5f,0.5f,0.5f,1f);


	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="entries"></param>
	/// <param name="prefab"></param>
	public CharacterEditorWindow(ScrObjListVariable entries, CharacterEntry prefab){
		characterLibrary = entries;
		charPrefab = prefab;
		LoadLibrary();
	}

	void LoadLibrary() {

		Debug.Log("Loading character libraries...");

		characterLibrary.GenerateDictionary();

		Debug.Log("Finished loading character libraries");

		InitializeWindow();
	}

	public void InitializeWindow() {
		selectTex = new Texture2D(1, 1);
		selectTex.SetPixel(0, 0, new Color(0.8f, 0.8f, 0.8f));
		selectTex.Apply();

		dispTex = new Texture2D(1, 1);
		dispTex.SetPixel(0, 0, new Color(0.8f, 0.5f, 0.2f));
		dispTex.Apply();

		dispOffset.right = 10;
	}


	public void DrawWindow() {

		GUILayout.Label("Character Editor", EditorStyles.boldLabel);

		GenerateAreas();
		DrawBackgrounds();
		DrawEntryList();
		DrawDisplayWindow();
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
						GUILayout.Height(selectRect.height-150));

		int oldSelected = selCharacter;
		selCharacter = GUILayout.SelectionGrid(selCharacter, characterLibrary.GetRepresentations(),1);
		EditorGUILayout.EndScrollView();

		if (oldSelected != selCharacter)
			SelectCharacter();

		EditorGUIUtility.labelWidth = 110;
		GUILayout.Label("Create Character", EditorStyles.boldLabel);
		uuid = EditorGUILayout.TextField("Character Name", uuid);
		repColor = EditorGUILayout.ColorField("Display Color", repColor);
		if (GUILayout.Button("Create new")) {
			InstansiateCharacter();
		}
		if (GUILayout.Button("Delete character")) {
			DeleteCharacter();
		}

		GUILayout.EndArea();
	}

	void DrawDisplayWindow() {
		GUILayout.BeginArea(dispRect);
		GUI.skin.textField.margin.right = 20;

		GUILayout.Label("Selected Character", EditorStyles.boldLabel);
		EditorGUILayout.SelectableLabel("UUID: " + charUuid);
		charRepColor = EditorGUILayout.ColorField("List color", charRepColor);

		GUILayout.Space(20);

		charName = EditorGUILayout.TextField("Name", charName);

		if (selCharacter != -1) {
			GUILayout.Space(100);

			if (GUILayout.Button("Save Character")) {
				SaveSelectedCharacter();
			}
		}

		GUILayout.EndArea();
	}

	void SelectCharacter() {
		// Nothing selected
		if (selCharacter == -1) {
			charUuid = "";
			charRepColor = new Color();
			charName = "";
		}
		else {
			// Something selected
			CharacterEntry ce = (CharacterEntry)characterLibrary.GetEntryByIndex(selCharacter);
			charUuid = ce.uuid;
			charRepColor = ce.repColor;
			charName = ce.entryName;
		}
	}

	void SaveSelectedCharacter() {
		CharacterEntry ce = (CharacterEntry)characterLibrary.GetEntryByIndex(selCharacter);
		ce.repColor = charRepColor;
		ce.entryName = charName;
		Undo.RecordObject(ce, "Updated character");
		EditorUtility.SetDirty(ce);
	}

	void InstansiateCharacter() {
		if (characterLibrary.ContainsID(uuid)) {
			Debug.Log("uuid already exists!");
			return;
		}
		CharacterEntry c = Editor.CreateInstance<CharacterEntry>();
		c.name = uuid;
		c.uuid = uuid;
		c.entryName = uuid;
		c.representImage = null;
		c.repColor = repColor;
		// c.representImage = characterLibrary.GenerateColorTexture(repColor);
		string path = "Assets/LibraryData/Characters/" + uuid + ".asset";

		AssetDatabase.CreateAsset(c, path);
		characterLibrary.AddEntry(c);
		Undo.RecordObject(characterLibrary, "Added character");
		EditorUtility.SetDirty(characterLibrary);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}

	void DeleteCharacter() {
		CharacterEntry c = (CharacterEntry)characterLibrary.GetEntryByIndex(selCharacter);
		string path = "Assets/LibraryData/Characters/" + c.uuid + ".asset";

		characterLibrary.RemoveEntryByIndex(selCharacter);
		Undo.RecordObject(characterLibrary, "Deleted character");
		EditorUtility.SetDirty(characterLibrary);
		bool res = AssetDatabase.MoveAssetToTrash(path);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		if (res) {
			Debug.Log("Removed character: " + c.uuid);
			selCharacter = -1;
		}
	}
}
