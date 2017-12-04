﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The main controller class which contains a reference to most of the global modules 
/// used in the game. 
/// </summary>
public class MainControllerScript : MonoBehaviour {

	public static MainControllerScript instance { get; private set;}

	public bool initiated { get; private set; }
	public StoryValues storyValues { get; private set;}
	public BattleGUIController battleGUI { get; private set;}
	public Inventory inventory { get; private set;}
	public KanjiList kanjiList { get; private set;}

	
	/// <summary>
	/// Loads all the modules.
	/// </summary>
	void Awake () {

		if (instance != null) {
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(transform.gameObject);
		instance = this;
		storyValues = GetComponentInChildren<StoryValues>();
		battleGUI = GetComponent<BattleGUIController>();
		inventory = GetComponent<Inventory>();
		kanjiList = GetComponent<KanjiList>();
		StartCoroutine("WaitForInitiate");
	}

	/// <summary>
	/// Waits to make sure that the necessary modules are loaded before the last modules are initiated.
	/// </summary>
	/// <returns></returns>
	IEnumerator WaitForInitiate(){
		while (!storyValues.initiated) {
			Debug.Log("Waiting");
			yield return null;
		}
		inventory.FillDefault();
		SaveController.instance.Load();

		initiated = true;
		Debug.Log("Maincontroller is ready");
	}

}
