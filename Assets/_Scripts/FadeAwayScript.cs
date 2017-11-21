using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAwayScript : MonoBehaviour {

	public float fadeDuration = 0.3f;
	public BoolVariable paused;

	private AudioController audioController;
	private SpriteRenderer rend;
	private Color alphaColor;
	private float colorValue;
	private AudioList audioList;

	// Use this for initialization
	void Start () {
		audioController = AudioController.instance;
		rend = GetComponent<SpriteRenderer>();
		audioList = GetComponent<AudioList>();

		alphaColor = rend.color;
		colorValue = 1.0f;
		audioController.RandomizeSfx(audioList.audioClips);
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
