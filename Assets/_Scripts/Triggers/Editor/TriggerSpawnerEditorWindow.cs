﻿using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TriggerSpawnerEditorWindow : EditorWindow {

	public GameObject battleTrigger;
	public GameObject dialogueTrigger;
	public GameObject changeMapTrigger;

	Transform triggerParent;
	GUIContent[] buttonList;

	//Selections
	string triggerToSpawn = "";


	[MenuItem("Window/TriggerSpawner")]
	public static void ShowWindow() {
		GetWindow<TriggerSpawnerEditorWindow>("Trigger Spawner");
	}

	void OnEnable() {
		EditorSceneManager.sceneOpened += SceneOpenedCallback;
		InitGuiContent();
	}

	void OnDisable() {
		EditorSceneManager.sceneOpened -= SceneOpenedCallback;
	}

	void InitGuiContent() {
		triggerParent = GameObject.Find("TRIGGER PARENT").transform;

		List<GUIContent> contentList = new List<GUIContent>();
		GUIContent guic;

		guic = new GUIContent();
		guic.text = "Battle";
		guic.image = GenerateImage(Color.yellow);
		contentList.Add(guic);

		guic = new GUIContent();
		guic.text = "Dialogue";
		guic.image = GenerateImage(Color.magenta);
		contentList.Add(guic);

		guic = new GUIContent();
		guic.text = "Change Map";
		guic.image = GenerateImage(Color.red);
		contentList.Add(guic);

		guic = new GUIContent();
		guic.text = "Interact";
		guic.image = GenerateImage(Color.grey);
		contentList.Add(guic);

		guic = new GUIContent();
		guic.text = "Shop";
		guic.image = GenerateImage(Color.grey);
		contentList.Add(guic);

		guic = new GUIContent();
		guic.text = "Unlock";
		guic.image = GenerateImage(Color.grey);
		contentList.Add(guic);

		buttonList = contentList.ToArray();
	}

	void OnGUI() {
		triggerToSpawn = "";

		if (GUILayout.Button(buttonList[0])) {
			triggerToSpawn = "battle";
		}

		if (GUILayout.Button(buttonList[1])) {
			triggerToSpawn = "dialogue";
		}

		if (GUILayout.Button(buttonList[2])) {
			triggerToSpawn = "changeMap";
		}

		if (GUILayout.Button(buttonList[3])) {
			triggerToSpawn = "interact";
		}

		if (GUILayout.Button(buttonList[4])) {
			triggerToSpawn = "shop";
		}

		if (GUILayout.Button(buttonList[5])) {
			triggerToSpawn = "unlock";
		}


		GameObject trigger = GetTriggerObject();
		if (trigger != null) {
			Debug.Log("Scene view position: " + SceneView.lastActiveSceneView.camera.transform.position);
			trigger.transform.SetParent(triggerParent);
			Vector3 spawnPos = new Vector3(
				SceneView.lastActiveSceneView.camera.transform.position.x,
				SceneView.lastActiveSceneView.camera.transform.position.y,
				0);
			trigger.transform.localPosition = spawnPos;
		}
	}


	/// <summary>
	/// Makes sure the window stays open when switching scenes.
	/// </summary>
	/// <param name="_scene"></param>
	/// <param name="_mode"></param>
	void SceneOpenedCallback(Scene _scene, OpenSceneMode _mode) {
		Debug.Log("SCENE LOADED");
		InitGuiContent();
	}



	Texture2D GenerateImage(Color color) {
		int size = 16;
		Texture2D image = new Texture2D(size, size);

		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				image.SetPixel(i, j, color);
			}
		}
		image.Apply();

		return image;
	}

	GameObject GetTriggerObject() {
		GameObject trigger = null;

		switch (triggerToSpawn) 
		{
			case "battle":
				trigger = Instantiate(battleTrigger);
				break;
			case "dialogue":
				trigger = Instantiate(dialogueTrigger);
				break;
			case "changeMap":
				trigger = Instantiate(changeMapTrigger);
				break;
		}

		return trigger;
	}
}