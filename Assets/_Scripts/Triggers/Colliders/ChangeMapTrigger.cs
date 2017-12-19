using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeMapTrigger : OWTrigger {

	public BattleEntry.OverworldArea area;
	public Vector2 position;
	public BoolVariable paused;
	public FloatVariable posx, posy;


	protected override void Trigger() {
		Debug.Log("Moving to area: " + area);
		// paused.value = true;
		posx.value = position.x;
		posy.value = position.y;
		startEvent.Invoke();
	}
}
