using UnityEngine;
using System.Collections;

/// <summary>
/// Projectile effect which is only the graphical component.
/// Used for after effects.
/// </summary>
public class ProjectileEffect : MonoBehaviour {

	public bool active = true;
	public float time = 1;
	private float currentTime;


	/// <summary>
	/// Update the current lifetime of the projectile.
	/// </summary>
	void Update() {
		if (active) {
			currentTime += Time.deltaTime;

			if (currentTime >= time)
				Destroy(gameObject);
		}
	}
}
