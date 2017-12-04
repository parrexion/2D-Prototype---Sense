using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour {

	public ScrObjListVariable characterLibrary;
	// public SpriteListVariable characterSprites;
	// public SpriteListVariable poseSprites;
	public IntVariable characterIndex;
	public IntVariable poseIndex;

	[SerializeField] private SpriteRenderer characterSprite = null;
	[SerializeField] private SpriteRenderer poseSprite = null;
	

	// Use this for initialization
	void Start () {
		UpdateCharacter();
	}

	public void UpdateCharacter() {
		if (characterIndex.value == -1 || poseIndex.value == -1){
			characterSprite.enabled = false;
			poseSprite.enabled = false;
		}
		else {
			CharacterEntry ce = (CharacterEntry)characterLibrary.GetEntryByIndex(characterIndex.value);
			characterSprite.enabled = true;
			poseSprite.enabled = true;
			// characterSprite.sprite = characterSprites.values[characterIndex.value];
			// poseSprite.sprite = poseSprites.values[poseIndex.value];
			characterSprite.sprite = ce.defaultColor;
			poseSprite.sprite = ce.poses[poseIndex.value];
		}

	}

}
