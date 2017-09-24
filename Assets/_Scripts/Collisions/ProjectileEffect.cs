using UnityEngine;
using System.Collections;

public class ProjectileEffect : MonoBehaviour {

	public bool active = true;
	public float time = 1;
	private float currentTime;


	void Update() {
		if (active) {
			currentTime += Time.deltaTime;

			if (currentTime >= time)
				Destroy(gameObject);
		}
	}
}
