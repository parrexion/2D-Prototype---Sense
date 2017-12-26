using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScreenController : MonoBehaviour {

	public enum MenuScreen {KANJI,EQUIP,MAP,MESSAGE,JOURNAL,SAVE}

	public MenuScreen currentScreen = MenuScreen.KANJI;
	public bool isEditor = false;
	bool menuLock = true;

	[Header("Screens")]
	public GameObject kanjiScreen;
	public GameObject equipScreen;
	public GameObject mapScreen;
	public GameObject messageScreen;
	public GameObject journalScreen;
	public GameObject saveScreen;

	[Space(3)]
	[Header("Buttons")]
	public Button kanjiButton;
	public Button equipButton;
	public Button mapButton;
	public Button messageButton;
	public Button journalButton;
	public Button saveButton;

	[Header("Outside values")]
	public StringVariable playerArea;
	public FloatVariable fadeSpeed;

	public UnityEvent buttonClickedEvent;
	public UnityEvent fadeOutEvent;


	// Use this for initialization
	void Start () {
		StartCoroutine(WaitForFadeIn());
#if UNITY_EDITOR
		isEditor = true;
#else
		kanjiButton.gameObject.SetActive(false);
		equipButton.gameObject.SetActive(false);
		mapButton.gameObject.SetActive(false);
		messageButton.gameObject.SetActive(false);
		journalButton.gameObject.SetActive(false);
		saveButton.gameObject.SetActive(false);
#endif
		UpdateCurrentScreen();
	}


	public void SetCurrentScreen(int screenIndex) {
		if (menuLock)
			return;

		buttonClickedEvent.Invoke();
		MenuScreen screen = (MenuScreen)screenIndex;
		if (currentScreen != screen) {
			currentScreen = screen;
			UpdateCurrentScreen();
		}
	}

	void UpdateCurrentScreen() {

		//Set current screen
		kanjiScreen.SetActive(currentScreen == MenuScreen.KANJI);
		equipScreen.SetActive(currentScreen == MenuScreen.EQUIP);
		if (isEditor) {
			mapScreen.SetActive(currentScreen == MenuScreen.MAP);
			messageScreen.SetActive(currentScreen == MenuScreen.MESSAGE);
			journalScreen.SetActive(currentScreen == MenuScreen.JOURNAL);
			saveScreen.SetActive(currentScreen == MenuScreen.SAVE);
		}

		//Set current buttons
		kanjiButton.interactable = (currentScreen != MenuScreen.KANJI);
		equipButton.interactable = (currentScreen != MenuScreen.EQUIP);
		if (isEditor) {
			mapButton.interactable = (currentScreen != MenuScreen.MAP);
			messageButton.interactable = (currentScreen != MenuScreen.MESSAGE);
			journalButton.interactable = (currentScreen != MenuScreen.JOURNAL);
			saveButton.interactable = (currentScreen != MenuScreen.SAVE);
		}
	}

	public void ReturnToGame() {
		if (menuLock)
			return;
		
		menuLock = true;
		buttonClickedEvent.Invoke();
		fadeOutEvent.Invoke();
		StartCoroutine(WaitForFadeOut());
	}

	IEnumerator WaitForFadeIn() {
		yield return new WaitForSeconds(fadeSpeed.value);
		menuLock = false;
		yield break;
	}

	IEnumerator WaitForFadeOut() {

		yield return new WaitForSeconds(fadeSpeed.value);

		if (playerArea.value == "Tower") {
            SceneManager.LoadScene((int)Constants.SCENE_INDEXES.BATTLETOWER);
        }
		else {
			SceneManager.LoadScene((int)Constants.SCENE_INDEXES.TUTORIAL);
		}

		yield break;
	}
}
