using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControllerScript : MonoBehaviour {

	public static MainControllerScript instance { get; private set;}

	public bool initiated { get; private set; }
	public StoryValues storyValues { get; private set;}
	public BattleGUIController battleGUI { get; private set;}
	public Inventory inventory { get; private set;}
	public KanjiList kanjiList { get; private set;}

	// Use this for initialization
	void Awake () {

		if (instance != null) {
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(transform.gameObject);
		instance = this;
		EnemyLibrary.Initialize();
		storyValues = GetComponentInChildren<StoryValues>();
		battleGUI = GetComponent<BattleGUIController>();
		inventory = GetComponent<Inventory>();
		kanjiList = GetComponent<KanjiList>();
		StartCoroutine("WaitForInitiate");
	}

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
