using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnlockTrigger : OWTrigger {

	public string unlockString;


	protected override void Trigger() {
		Debug.Log("Unlocking id: "+ unlockString);
		startEvent.Invoke();
	}
}
