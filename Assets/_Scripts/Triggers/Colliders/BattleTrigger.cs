using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleTrigger : OWTrigger {

	public BattleEntry battle;
	public StringVariable battleUuid;

	protected override void Trigger() {
		Debug.Log("Start battle: "+ battle.entryName);
		battleUuid.value = battle.uuid;
		Deactivate();
		startEvent.Invoke();
	}
}
