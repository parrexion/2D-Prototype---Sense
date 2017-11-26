using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCloseup : MonoBehaviour {

	public StringListVariable characterNameLibrary;
	public SpriteListVariable characterLibrary;
	public SpriteListVariable poseLibrary;

	public StringVariable characterName;
	public IntVariable characterIndex;
	public IntVariable poseIndex;

	public Text characterNameBox;
	public SpriteRenderer characterRenderer;
	public SpriteRenderer poseRenderer;


	// Use this for initialization
	void Start () {
		UpdateCloseup();
	}
	
	// Update is called once per frame
	public void UpdateCloseup() {

		characterNameBox.text = characterName.value;

		if (characterIndex.value == -1 || poseIndex.value == -1){
			characterRenderer.enabled = false;
			poseRenderer.enabled = false;
		}
		else {
			characterRenderer.enabled = true;
			poseRenderer.enabled = true;
			characterRenderer.sprite = characterLibrary.values[characterIndex.value];
			poseRenderer.sprite = poseLibrary.values[poseIndex.value];
		}
	}
}
