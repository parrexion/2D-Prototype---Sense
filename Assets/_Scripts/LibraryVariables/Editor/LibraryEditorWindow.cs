﻿using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LibraryEditorWindow : EditorWindow {

	private enum State { BATTLE = 0, CHARACTER = 1, ENEMY = 2, BACKGROUND = 3 }

	// Header
	Rect headerRect = new Rect();
	Texture2D headerTex;

	public BattleEditorWindow battleEditor;
	public ScrObjLibraryVariable battleLibrary;
	public BattleEntry battleContainer;

	public CharacterEditorWindow characterEditor;
	public ScrObjLibraryVariable characterLibrary;
	public CharacterEntry charContainer;
	public SpriteListVariable poseList;

	public EnemyEditorWindow enemyEditor;
	public ScrObjLibraryVariable enemyLibrary;
	public EnemyEntry enemyContainer;

	public BackgroundEditorWindow backgroundEditor;
	public ScrObjLibraryVariable backgroundLibrary;
	public BackgroundEntry backgroundContainer;

	private int currentWindow = (int)State.CHARACTER;
	private string[] toolbarStrings = new string[] {"Battles", "Characters", "Enemies", "Background"};


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
			case State.BACKGROUND:
				backgroundEditor.DrawWindow();
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
		battleEditor = new BattleEditorWindow(battleLibrary, battleContainer);
		
		characterEditor = new CharacterEditorWindow(characterLibrary, charContainer, poseList);
		
		enemyEditor = new EnemyEditorWindow(enemyLibrary, enemyContainer);

		backgroundEditor = new BackgroundEditorWindow(backgroundLibrary, backgroundContainer);

		InitializeWindow();
	}

	/// <summary>
	/// Initializes all the window specific variables.
	/// </summary>
	void InitializeWindow() {
		headerTex = new Texture2D(1, 1);
		headerTex.SetPixel(0, 0, new Color(0.5f, 0.2f, 0.8f));
		headerTex.Apply();

		battleEditor.InitializeWindow();
		characterEditor.InitializeWindow();
		enemyEditor.InitializeWindow();
		backgroundEditor.InitializeWindow();
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
