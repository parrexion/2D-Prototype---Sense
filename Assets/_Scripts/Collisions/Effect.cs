using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour {

	public float lifeTime = 1f;

	private bool active = true;
	private float currentTime = 0f;

	
	/// <summary>
	/// Update the current lifetime of the effect.
	/// </summary>
	void Update() {
		if (active) {
			currentTime += Time.deltaTime;

			if (currentTime >= lifeTime)
				Destroy(gameObject);
		}
	}

	/// <summary>
	/// Enables or disables the effect.
	/// </summary>
	/// <param name="state"></param>
	public virtual void SetActive(bool state) {

		active = state;
	}
}
