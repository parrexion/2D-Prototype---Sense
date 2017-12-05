using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractTrigger : OWTrigger {

	public string action;


	protected override void Trigger() {
		Debug.Log("Start battle: "+ action);
		startEvent.Invoke();
	}
}
