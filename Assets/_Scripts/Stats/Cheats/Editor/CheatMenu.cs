﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class CheatMenu : EditorWindow {

	[Header("Battle values")]
	public BoolVariable invinciblePlayers;
	public BoolVariable invincibleEnemies;
	public BoolVariable alwaysEscapeBattle;

	[Header("Overworld values")]
	public FloatVariable speedHack;

	[Header("Dialogue values")]
	public BoolVariable alwaysSkippableDialogue;

	// Private stuff
	private bool useSpeedHack = false;
	const float defaultSpeedHackSpeed = 2.5f;


	[MenuItem("Window/CheatMenu")]
	public static void ShowWindow() {
		GetWindow<CheatMenu>("Cheat Menu");
	}

	void OnEnable() {
		EditorSceneManager.sceneOpened += SceneOpenedCallback;
	}

	void OnDisable() {
		EditorSceneManager.sceneOpened -= SceneOpenedCallback;
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
	/// Initializes all the window specific variables.
	/// </summary>
	void InitializeWindow() {

	}

	/// <summary>
	/// Renders the window.
	/// </summary>
	void OnGUI() {
		GUILayout.Label("Cheat Menu", EditorStyles.boldLabel);
		GUILayout.Space(20);

		GUILayout.Label("Battle cheats", EditorStyles.boldLabel);

		alwaysEscapeBattle.value = GUILayout.Toggle(alwaysEscapeBattle.value, "Enable escape from all battles");
		invinciblePlayers.value = GUILayout.Toggle(invinciblePlayers.value, "Invincible players");
		invincibleEnemies.value = GUILayout.Toggle(invincibleEnemies.value, "Invincible enemies");

		GUILayout.Space(10);

		GUILayout.Label("Overworld cheats", EditorStyles.boldLabel);

		GUILayout.BeginHorizontal();
		useSpeedHack = GUILayout.Toggle(useSpeedHack, "Speed hack");
		if (!useSpeedHack)
			speedHack.value = 1;
		else if (speedHack.value == 1 || speedHack.value < 0)
			speedHack.value = defaultSpeedHackSpeed;
		EditorGUI.BeginDisabledGroup(!useSpeedHack);
		speedHack.value = EditorGUILayout.FloatField("Speed", speedHack.value);
		EditorGUI.EndDisabledGroup();
		GUILayout.EndHorizontal();

		GUILayout.Space(10);

		GUILayout.Label("Dialogue cheats", EditorStyles.boldLabel);

		GUILayout.Toggle(alwaysSkippableDialogue, "Skippable dialogue");
	}
}
