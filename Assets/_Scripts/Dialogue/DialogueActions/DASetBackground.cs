﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DASetBackground : DialogueAction {

	public override bool Act(DialogueScene scene, DialogueJsonItem data) {

		scene.background = data.character;

		return true;
	}
}