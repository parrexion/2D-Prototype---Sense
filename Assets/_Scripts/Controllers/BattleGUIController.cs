using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGUIController : MonoBehaviour {

	public List<DamageNumberDisplay> damages = new List<DamageNumberDisplay>();
	public List<Projectile> projectiles = new List<Projectile>();
	public List<ProjectileEffect> projectileEffects = new List<ProjectileEffect>();

	
	// Update is called once per frame
	void Update () {
		damages.RemoveAll(item => (item == null));
		projectiles.RemoveAll(item => (item == null));
		projectileEffects.RemoveAll(item => (item == null));
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

		foreach (Projectile p in projectiles) {
			if (p != null) {
				p.SetActive(state);
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
