using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TriggerSpawnerEditorWindow : EditorWindow {


	[MenuItem("Window/TriggerSpawner")]
	public static void ShowWindow() {
		GetWindow<TriggerSpawnerEditorWindow>("Trigger Spawner");
	}

	void OnEnable() {
		EditorSceneManager.sceneOpened += SceneOpenedCallback;
	}

	void OnDisable() {
		EditorSceneManager.sceneOpened -= SceneOpenedCallback;
	}

	void OnGUI() {

		if (GUILayout.Button("Position")) {
			// Debug.Log("Scene view position: " + SceneView.lastActiveSceneView.position);
			Debug.Log("Scene view position: " + SceneView.lastActiveSceneView.position);
			// SceneView.currentDrawingSceneView.camera
		}
	}


	/// <summary>
	/// Makes sure the window stays open when switching scenes.
	/// </summary>
	/// <param name="_scene"></param>
	/// <param name="_mode"></param>
	void SceneOpenedCallback(Scene _scene, OpenSceneMode _mode) {
		Debug.Log("SCENE LOADED");
	}

}
