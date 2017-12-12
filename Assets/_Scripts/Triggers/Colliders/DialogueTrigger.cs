﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : OWTrigger {

	public DialogueEntry dialogue;
	public StringVariable dialogueUuid;


	protected override void Trigger() {
		Debug.Log("Start dialogue: "+ dialogue.name);
		dialogueUuid.value = dialogue.uuid;
		Deactivate();
		startEvent.Invoke();
	}
}
