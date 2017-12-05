using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeMapTrigger : OWTrigger {

	public string area;
	public Vector2 position;


	protected override void Trigger() {
		Debug.Log("Moving to area: " + area);
		startEvent.Invoke();
	}
}
