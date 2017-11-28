using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyKeyButton : MonoBehaviour {
	
	public Canvas nextCanvas;
	public BoolVariable gameStarted;


	// Update is called once per frame
	void Update () {
		if (Input.anyKey || gameStarted.value) {
			gameStarted.value = true;
			nextCanvas.enabled = true;
			gameObject.SetActive(false);
		}
	}
}
