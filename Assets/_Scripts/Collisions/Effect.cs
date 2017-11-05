﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all effects spawned during battles.
/// This can be anything from projectiles to particles.
/// </summary>
public class Effect : MonoBehaviour {

	protected MoveScript move;

	private bool active = true;
	[HideInInspector] public float lifeTime = 1f;
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
	/// Override to add additional things to activate.
	/// </summary>
	/// <param name="state"></param>
	public virtual void SetActive(bool state) {
		active = state;
	}

	/// <summary>
	/// Sets the movement of the effect by giving speed and rotation to it
	/// </summary>
	/// <param name="baseSpeed"></param>
	/// <param name="rotation"></param>
	public void SetMovement(Vector2 baseSpeed, float rotation){
		move = GetComponent<MoveScript>();
		if (move != null) {
			move.setSpeed(baseSpeed, rotation);
		}
	}
}
