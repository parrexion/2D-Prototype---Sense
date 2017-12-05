using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : OWTrigger {

	public Dialogue dialogue;


	protected override void Trigger() {
		Debug.Log("Start dialogue: "+ dialogue.name);
		startEvent.Invoke();
	}
}
