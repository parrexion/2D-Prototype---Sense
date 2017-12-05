using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class OWTrigger : MonoBehaviour {

	public string uuid = System.Guid.NewGuid().ToString();
	public bool active = true;
	public UnityEvent startEvent;


	void OnEnable() {
		active = TriggerController.instance.CheckActive(uuid);
	}

	
	/// <summary>
	/// When colliding with a BattleTrigger, start the battle.
	/// </summary>
	/// <param name="otherCollider"></param>
	void OnTriggerEnter2D(Collider2D otherCollider){
		if (!active)
			return;

		if (otherCollider.gameObject.tag != "Player") {
			Debug.Log("That was not a player");
			return;
		}

		Debug.Log("That's a trigger!");
		Trigger();

	}

	public void Deactivate() {
		TriggerController.instance.SetActive(uuid, false);
		gameObject.SetActive(false);
	}

	/// <summary>
	/// Triggers the action depending on the trigger type.
	/// </summary>
	protected abstract void Trigger();
}
