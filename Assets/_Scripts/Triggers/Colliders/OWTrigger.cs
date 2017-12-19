using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class OWTrigger : MonoBehaviour {

	public string uuid = System.Guid.NewGuid().ToString();
	public bool active = true;
	public bool visible = false;
	public UnityEvent startEvent;
	public SpriteRenderer sprite;
	public SpriteRenderer areaSprite;


	void OnEnable() {
		StartCoroutine(CheckTrigger());
	}

	IEnumerator CheckTrigger() {
		while(TriggerController.instance == null)
			yield return null;

		active = TriggerController.instance.CheckActive(uuid);
		areaSprite.enabled = false;
		Startup();
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

		Trigger();
	}

	public void Deactivate() {
		TriggerController.instance.SetActive(uuid, false);
	}

	/// <summary>
	/// Triggers the action depending on the trigger type.
	/// </summary>
	protected abstract void Trigger();

	protected virtual void Startup(){
		if (!active && sprite != null)
			sprite.enabled = visible;
	}
}
