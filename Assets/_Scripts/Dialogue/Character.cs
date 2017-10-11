using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour {

	[SerializeField] private SpriteRenderer characterSprite = null;
	[SerializeField] private SpriteRenderer poseSprite = null;
	public Sprite[] characterChars = null;
	public Sprite[] characterPoses = null;

	// Use this for initialization
	void Start () {
		characterSprite.sprite = null;
		poseSprite.sprite = null;
	}


	public void SetCharacterPose(int character, int pose) {
		if (character == -1) {
			characterSprite.enabled = false;
			poseSprite.enabled = false;
			return;
		}

		if (character == 4){
			Debug.LogWarning("Found a 4 as the character :/");
			return;
		}

		characterSprite.sprite = characterChars[character-1];
		poseSprite.sprite = characterPoses[pose];

		characterSprite.enabled = true;
		poseSprite.enabled = true;
	}
}
