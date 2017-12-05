using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutsidePlayerController : MonoBehaviour {

	public BoolVariable paused;
	private MoveHomingNoLimit moveToPosition;
	private Camera cam;
	public FloatVariable posx, posy;

	// Use this for initialization
	void Start () {
		moveToPosition = GetComponent<MoveHomingNoLimit>();
		cam = Camera.main;
		transform.position = new Vector3(posx.value,posy.value,0);
	}
	
	// Update is called once per frame
	void Update () {

		if (paused.value)
			return;

		if (Input.GetMouseButton(1)) {
			moveToPosition.moveToPosition = cam.ScreenToWorldPoint(Input.mousePosition);
		}
		posx.value = transform.position.x;
		posy.value = transform.position.y;
	}

}
