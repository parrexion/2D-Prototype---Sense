using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtableAbsorbScript : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D otherCollider){
		Projectile projectile = otherCollider.gameObject.GetComponent<Projectile>();
		if (projectile != null) {
			Destroy(projectile.gameObject);
			//Debug.Log("Found you!");
		}
		else
			Debug.Log ("Null");
	}

}
