using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class OWTrigger : MonoBehaviour {

	[Header("Is trigger active?")]
	public bool active = true;
	[Header("Is trigger visible?")]
	public bool visible = false;
	
	[Header("Deactivate/Activate triggers")]
	public List<OWTrigger> deactivateTriggers = new List<OWTrigger>();
	public List<OWTrigger> activateTriggers = new List<OWTrigger>();

	[Header("References - don't touch")]
	public string uuid = System.Guid.NewGuid().ToString();
	public SpriteRenderer sprite;
	public SpriteRenderer areaSprite;
	public BoolVariable paused;
	public IntVariable currentArea;

	public UnityEvent startEvent;


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
		sprite.enabled = active && visible && sprite.sprite != null;
	}

	protected void TriggerOtherTriggers() {
		for (int i = 0; i < deactivateTriggers.Count; i++) {
			TriggerController.instance.SetActive(deactivateTriggers[i].uuid, false);
		}
		for (int i = 0; i < activateTriggers.Count; i++) {
			TriggerController.instance.SetActive(activateTriggers[i].uuid, true);
		}
	}
}
