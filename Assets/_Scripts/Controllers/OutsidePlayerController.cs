using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutsidePlayerController : MonoBehaviour {

	public BoolVariable paused;
	private MoveHomingNoLimit moveToPosition;
	private Camera cam;

	// Use this for initialization
	void Start () {
		moveToPosition = GetComponent<MoveHomingNoLimit>();
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {

		if (paused.value)
			return;

		if (Input.GetMouseButton(1)) {
			moveToPosition.moveToPosition = cam.ScreenToWorldPoint(Input.mousePosition);
		}
	}

}
