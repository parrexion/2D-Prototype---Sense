using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackScript : MonoBehaviour {

	protected BattleGUIController bgui;
	public Transform projectile;
	public Transform effect;

	public abstract void Attack(StateController controller);


	void Start() {
		bgui = MainControllerScript.instance.battleGUI;
	}
}
