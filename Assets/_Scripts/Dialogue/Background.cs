using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour {

	public SpriteListVariable backgrounds;
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
			image.sprite = backgrounds.values[currentBackground];
		}
	}
}
