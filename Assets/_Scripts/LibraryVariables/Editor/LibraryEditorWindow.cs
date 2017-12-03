using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LibraryEditorWindow : EditorWindow {

	private enum State { BATTLE = 0, CHARACTER = 1, ENEMY = 2 }

	// Header
	Rect headerRect = new Rect();
	Texture2D headerTex;

	public BattleEditorWindow battleEditor;
	public ScrObjListVariable battleLibrary;
	public BattleEntry battlePrefab;

	public CharacterEditorWindow characterEditor;
	public ScrObjListVariable characterLibrary;
	public CharacterEntry charPrefab;

	public EnemyEditorWindow enemyEditor;
	public ScrObjListVariable enemyLibrary;
	public EnemyEntry enemyPrefab;

	private int currentWindow = (int)State.CHARACTER;
	private string[] toolbarStrings = new string[] {"Battles", "Characters", "Enemies"};


	[MenuItem("Window/LibraryEditor")]
	public static void ShowWindow() {
		GetWindow<LibraryEditorWindow>("Library Editor");
	}

	void OnEnable() {
		EditorSceneManager.sceneOpened += SceneOpenedCallback;
		LoadLibraries();
	}

	void OnDisable() {
		EditorSceneManager.sceneOpened -= SceneOpenedCallback;
	}

	void OnGUI() {
		DrawHeader();
		
		switch ((State)currentWindow)
		{
			case State.BATTLE:
				battleEditor.DrawWindow();
				break;
			case State.CHARACTER:
				characterEditor.DrawWindow();
				break;
			case State.ENEMY:
				enemyEditor.DrawWindow();
				break;
		}
	}


	/// <summary>
	/// Makes sure the window stays open when switching scenes.
	/// </summary>
	/// <param name="_scene"></param>
	/// <param name="_mode"></param>
	void SceneOpenedCallback(Scene _scene, OpenSceneMode _mode) {
		Debug.Log("SCENE LOADED");
		InitializeWindow();
	}

	/// <summary>
	/// Loads all the libraries for the editors.
	/// </summary>
	void LoadLibraries() {
		battleEditor = new BattleEditorWindow(battleLibrary, battlePrefab);
		battleEditor.LoadLibrary();
		
		characterEditor = new CharacterEditorWindow(characterLibrary, charPrefab);
		
		enemyEditor = new EnemyEditorWindow(enemyLibrary, enemyPrefab);
		enemyEditor.LoadLibrary();

		InitializeWindow();
	}

	/// <summary>
	/// Initializes all the window specific variables.
	/// </summary>
	void InitializeWindow() {
		headerTex = new Texture2D(1, 1);
		headerTex.SetPixel(0, 0, new Color(0.5f, 0.2f, 0.8f));
		headerTex.Apply();

		switch ((State)currentWindow)
		{
			case State.BATTLE:
				battleEditor.InitializeWindow();
				break;
			case State.CHARACTER:
				characterEditor.InitializeWindow();
				break;
			case State.ENEMY:
				enemyEditor.InitializeWindow();
				break;
		}
	}

	void DrawHeader() {
		headerRect.x = 0;
		headerRect.y = 0;
		headerRect.width = Screen.width;
		headerRect.height = 50;
		GUI.DrawTexture(headerRect, headerTex);

		currentWindow = GUILayout.Toolbar(currentWindow, toolbarStrings);
	}
}
