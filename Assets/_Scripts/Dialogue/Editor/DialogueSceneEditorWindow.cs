﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

public class DialogueSceneEditorWindow : EditorWindow {

	DialogueEntry selectedDialogue = null;
	public StringVariable dialogueUUID;

	[MenuItem("Window/DialogueSelector")]
	public static void ShowWindow() {
		GetWindow<DialogueSceneEditorWindow>("Dialogue Selector");
	}

	void OnEnable() {
		EditorSceneManager.sceneOpened += SceneOpenedCallback;
		InitializeWindow();
	}

	void OnDisable() {
		EditorSceneManager.sceneOpened -= SceneOpenedCallback;
	}

	void OnGUI() {

		GUILayout.Label("Dialogue selector", EditorStyles.boldLabel);
		EditorGUIUtility.labelWidth = 100;

		HeaderStuff();
	}

	void SceneOpenedCallback( Scene _scene, OpenSceneMode _mode) {
		Debug.Log("SCENE LOADED");
		InitializeWindow();
	}

	void InitializeWindow() {

	}

	void HeaderStuff() {
		EditorGUILayout.SelectableLabel("Selected Dialogue    UUID: " + dialogueUUID.value, EditorStyles.boldLabel);

		if (GUILayout.Button("Open Dialogue Scene")) {
			EditorSceneManager.OpenScene("Assets/_Scenes/Dialogue.unity");
		}

		GUILayout.Space(5);

		selectedDialogue = (DialogueEntry)EditorGUILayout.ObjectField("Dialogue", selectedDialogue, typeof(DialogueEntry),false);

		if (selectedDialogue != null) {
			if (GUILayout.Button("Set scene")) {
				dialogueUUID.value = selectedDialogue.uuid;
				Undo.RecordObject(dialogueUUID, "Set selected dialogue");
				EditorUtility.SetDirty(dialogueUUID);
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();
			}
		}
	}


}
