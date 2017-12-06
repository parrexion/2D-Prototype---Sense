using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeAwayScript : MonoBehaviour {

	public float fadeDuration = 0.3f;
	public BoolVariable paused;
	public UnityEvent playSfx;
	public AudioVariable sfxClip;

	private SpriteRenderer rend;
	private Color alphaColor;
	private float colorValue;
	private AudioList audioList;

	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer>();
		audioList = GetComponent<AudioList>();

		alphaColor = rend.color;
		colorValue = 1.0f;
		sfxClip.value = audioList.RandomClip();
		playSfx.Invoke();
	}
	
	// Update is called once per frame
	void Update () {
		if (paused.value)
			return;
			
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
