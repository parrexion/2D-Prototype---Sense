using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour {

	public CharacterEntry character;
	public IntVariable poseIndex;

	[SerializeField] private SpriteRenderer characterSprite = null;
	[SerializeField] private SpriteRenderer poseSprite = null;
	

	// Use this for initialization
	void Start () {
		UpdateCharacter();
	}

	public void UpdateCharacter() {
		if (character == null){
			characterSprite.enabled = false;
			poseSprite.enabled = false;
		}
		else {
			characterSprite.enabled = true;
			poseSprite.enabled = true;
			characterSprite.sprite = character.defaultColor;
			poseSprite.sprite = character.poses[poseIndex.value];
		}

	}

}
