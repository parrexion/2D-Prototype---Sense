using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour {

	public ScrObjLibraryVariable backgroundLibrary;
	public IntVariable dialogueBackground;
	private int currentBackground;
	private Image image;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image>();
		UpdateBackground();
	}
	
	// Update is called once per frame
	public void UpdateBackground () {
		if (currentBackground != dialogueBackground.value){
			currentBackground = dialogueBackground.value;
			BackgroundEntry bke = (BackgroundEntry)backgroundLibrary.GetEntryByIndex(currentBackground);
			image.sprite = bke.sprite;
		}
	}
}
