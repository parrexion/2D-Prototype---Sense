using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraValues {

	public Constants.OverworldArea area;
	public bool stationary;
	public float size;
	public Vector2 position;
	public Rect cameraBox;
}

public class CameraConstants : MonoBehaviour {

	public float defaultCamSize;
	public CameraValues[] values;


	public CameraValues GetValues(Constants.OverworldArea area) {
		for (int i = 0; i < values.Length; i++) {
			if (values[i].area == area)
				return values[i];
		}
		return null;
	}
}
