using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour {

	private SpriteRenderer characterSprite = null;
	private SpriteRenderer poseSprite = null;
	public Sprite[] characterPoses = null;
	public Sprite[] characterCloseups = null;

	// Use this for initialization
	void Start () {
		characterSprite = GetComponent<SpriteRenderer>();
		characterSprite.sprite = null;

		poseSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
		poseSprite.sprite = null;
	}


	public void SetCharacterPose(int character, int pose) {
		if (character == -1) {
			characterSprite.enabled = false;
			poseSprite.enabled = false;
			return;
		}

		characterSprite.sprite = null;
		poseSprite.sprite = null;

		characterSprite.enabled = true;
		poseSprite.enabled = true;
	}
}
