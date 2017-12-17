using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStorage : MonoBehaviour {

#region Singleton

	private static UIStorage instance = null;
	void Awake() {
		if (instance != null) {
			Destroy(gameObject);
		}
		else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}
	
#endregion

	public BoolVariable gameStarted;
}
