using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAwayScript : MonoBehaviour {

	public float fadeDuration = 0.3f;
	private SpriteRenderer rend;
	private Color alphaColor;
	private float colorValue;

	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer>();
		alphaColor = rend.color;
		colorValue = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		colorValue -= fadeDuration*Time.deltaTime;
		if (colorValue <= 0)
			Destroy(gameObject);
		else {
			alphaColor = rend.color;
			alphaColor.a = colorValue;
			rend.color = alphaColor;
		}
	}
}
