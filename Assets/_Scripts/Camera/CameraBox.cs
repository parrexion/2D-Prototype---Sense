using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBox : MonoBehaviour {

	public Transform objectToFollow;
	public float top;
	public float left;
	public float right;
	public float bottom;

	Vector3 position;
	float xpos, ypos;
	
	// Update is called once per frame
	void Update () {
		position = objectToFollow.position;
		xpos = position.x;
		ypos = position.y;

		if (xpos < left) {
			xpos = left;
		}
		else if (xpos > right) {
			xpos = right;
		}

		if (ypos > top) {
			ypos = top;
		}
		else if (ypos < bottom) {
			ypos = bottom;
		}

		transform.position = new Vector3(xpos,ypos,transform.localPosition.z);
	}
}
