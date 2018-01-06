using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeMapTrigger : OWTrigger {

	public Constants.SCENE_INDEXES area;
	public Vector2 position;
	public IntVariable playerArea;
	public FloatVariable posx, posy;


	protected override void Trigger() {
		Debug.Log("Moving to area: " + area);
		paused.value = true;
		currentArea.value = (int)area;
		playerArea.value = (int)area;
		posx.value = position.x;
		posy.value = position.y;
		Debug.Log("Position is now: " + posx.value + ", " + posy.value);
		startEvent.Invoke();
	}
}
