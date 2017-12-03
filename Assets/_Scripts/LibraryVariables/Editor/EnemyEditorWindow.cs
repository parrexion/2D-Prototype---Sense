using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class EnemyEditorWindow {

	public ScrObjListVariable enemyLibrary;
	public EnemyEntry enemyValues;

	// Display screen
	Rect dispRect = new Rect();
	Texture2D dispTex;

	// Selection screen
	Rect selectRect = new Rect();
	Texture2D selectTex;
	Vector2 scrollPos;
	string enemyUuid;
	Color repColor = new Color();
	int selEnemy = -1;

	public EnemyEditorWindow(ScrObjListVariable entries, EnemyEntry container){
		enemyLibrary = entries;
		enemyValues = container;
	}

	public void LoadLibrary() {

		Debug.Log("Loading character libraries...");

		enemyLibrary.GenerateDictionary();

		Debug.Log("Finished loading character libraries");

		InitializeWindow();
	}

	public void InitializeWindow() {
		dispTex = new Texture2D(1, 1);
		dispTex.SetPixel(0, 0, new Color(0.1f, 0.4f, 0.6f));
		dispTex.Apply();

		selectTex = new Texture2D(1, 1);
		selectTex.SetPixel(0, 0, new Color(0.8f, 0.8f, 0.8f));
		selectTex.Apply();
	}


	public void DrawWindow() {

		GUILayout.BeginHorizontal();
		GUILayout.Label("Enemy Editor", EditorStyles.boldLabel);
		if (selEnemy != -1) {
			if (GUILayout.Button("Save Enemy")){
				SaveSelectedEnemy();
			}
		}
		GUILayout.EndHorizontal();

		GenerateAreas();
		DrawBackgrounds();
		DrawEntryList();
		if (selEnemy != -1)
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
																GUILayout.Height(selectRect.height-130));

		int oldSelected = selEnemy;
		selEnemy = GUILayout.SelectionGrid(selEnemy, enemyLibrary.GetRepresentations(),1);
		EditorGUILayout.EndScrollView();
		
		if (oldSelected != selEnemy)
			SelectEnemy();

		EditorGUIUtility.labelWidth = 90;
		GUILayout.Label("Create new enemy", EditorStyles.boldLabel);
		enemyUuid = EditorGUILayout.TextField("Enemy uuid", enemyUuid);
		repColor = EditorGUILayout.ColorField("Display Color", repColor);
		if (GUILayout.Button("Create new")) {
			InstansiateEnemy();
		}
		if (GUILayout.Button("Delete enemy")) {
			DeleteEnemy();
		}
		EditorGUIUtility.labelWidth = 0;

		GUILayout.EndArea();
	}

	void DrawDisplayWindow() {
		GUILayout.BeginArea(dispRect);
		GUI.skin.textField.margin.right = 20;

		GUILayout.Label("Selected Enemy", EditorStyles.boldLabel);
		EditorGUILayout.SelectableLabel("UUID: " + enemyValues.uuid);
		enemyValues.repColor = EditorGUILayout.ColorField("List color", enemyValues.repColor);

		GUILayout.Space(20);

		enemyValues.entryName = EditorGUILayout.TextField("Name", enemyValues.entryName);

		GUILayout.EndArea();
	}
	
	void SelectEnemy() {
		// Nothing selected
		if (selEnemy == -1) {
			enemyValues.ResetValues();
		}
		else {
			// Something selected
			EnemyEntry ee = (EnemyEntry)enemyLibrary.GetEntryBySelectedIndex(selEnemy);
			enemyValues.CopyValues(ee);
		}
	}

	void SaveSelectedEnemy() {
		EnemyEntry ee = (EnemyEntry)enemyLibrary.GetEntryBySelectedIndex(selEnemy);
		ee.CopyValues(enemyValues);
		Undo.RecordObject(ee, "Updated enemy");
		EditorUtility.SetDirty(ee);
	}

	void InstansiateEnemy() {
		if (enemyLibrary.ContainsID(enemyUuid)) {
			Debug.Log("uuid already exists!");
			return;
		}
		EnemyEntry ee = Editor.CreateInstance<EnemyEntry>();
		ee.name = enemyUuid;
		ee.uuid = enemyUuid;
		ee.entryName = enemyUuid;
		ee.repColor = repColor;
		string path = "Assets/LibraryData/Enemies/" + enemyUuid + ".asset";

		enemyLibrary.AddEntry(ee);
		Undo.RecordObject(enemyLibrary, "Added enemy");
		EditorUtility.SetDirty(enemyLibrary);
		AssetDatabase.CreateAsset(ee, path);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		SelectEnemy();
	}

	void DeleteEnemy() {
		EnemyEntry ee = (EnemyEntry)enemyLibrary.GetEntryBySelectedIndex(selEnemy);
		string path = "Assets/LibraryData/Enemies/" + ee.uuid + ".asset";

		enemyLibrary.RemoveEntryByIndex(selEnemy);
		Undo.RecordObject(enemyLibrary, "Deleted enemy");
		EditorUtility.SetDirty(enemyLibrary);
		bool res = AssetDatabase.MoveAssetToTrash(path);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		if (res) {
			Debug.Log("Removed enemy: " + ee.uuid);
			selEnemy = -1;
		}
	}
}
