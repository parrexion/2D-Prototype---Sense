using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public SpriteRenderer sprite = null;
	public Sprite[] characterPoses = null;
	public Sprite[] characterCloseups = null;

	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer>();
		sprite.sprite = null;
	}

}
