using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGUIController : MonoBehaviour {

	public List<DamageNumberDisplay> damages = new List<DamageNumberDisplay>();
	public List<Effect> effectList = new List<Effect>();

	
	// Update is called once per frame
	void Update () {
		damages.RemoveAll(item => (item == null));
		effectList.RemoveAll(item => (item == null));
	}


	public void SetActive(bool state) {

		foreach (DamageNumberDisplay dnd in damages) {
			if (dnd != null) {
				dnd.active = state;
			}
			else {
//				Debug.Log("So null");
			}
		}

		foreach (Effect eff in effectList) {
			if (eff != null) {
				eff.SetActive(state);
			}
			else {
//				Debug.Log("So null");
			}
		}
	}


	//
	//	public bool removeNullCompare(Projectile p){
	//		bool res = (p == null);
	//		if (res)
	//			Debug.Log("removed a null");
	//		return res;
	//	}
}
