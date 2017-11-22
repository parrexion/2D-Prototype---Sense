using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour {

	[SerializeField] private SpriteRenderer characterSprite = null;
	[SerializeField] private SpriteRenderer poseSprite = null;

	// Use this for initialization
	void Start () {
		characterSprite.sprite = null;
		poseSprite.sprite = null;
	}


	public void SetCharacterPose(Sprite character, Sprite pose) {
		if (character == null) {
			characterSprite.enabled = false;
			poseSprite.enabled = false;
			return;
		}

		characterSprite.sprite = character;
		poseSprite.sprite = pose;
		characterSprite.enabled = true;
		poseSprite.enabled = true;
	}

	public Sprite GetCharacterSprite(){
		if (characterSprite.enabled)
			return characterSprite.sprite;
		
		return null;
	}

	public Sprite GetPoseSprite(){
		if (poseSprite.enabled)
			return poseSprite.sprite;
		
		return null;
	}
}
