using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class which scales the transform of the object so that it tries to keep the same 
/// size and position as was intended with the original screen size.
/// </summary>
public class ResolutionScaling : MonoBehaviour {

	// Use this for initialization
	void Start () {
		float widthScale = (float)BattleConstants.screenWidth / (float)Screen.width;
		float heightScale = (float)BattleConstants.screenHeight / (float)Screen.height;

		Vector3 original = transform.localScale;
		transform.localScale = new Vector3(
			original.x * widthScale,
			original.y * heightScale,
			original.z
		);
	}

}
