using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleClock : MonoBehaviour {

	public Transform arrow;
	public float changeTime = 3f;

	public BoolVariable paused;
	public BoolVariable useSlowTime;
	public BoolVariable leftSideSlow;

	SpriteRenderer sprite;
	float currentTime;


	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer>();
		sprite.color = Color.magenta;
		leftSideSlow.value = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (paused.value || !useSlowTime.value) 
			return;

		currentTime += Time.deltaTime;

		if (currentTime >= changeTime) {
			currentTime -= changeTime;
			leftSideSlow.value = !leftSideSlow.value;
			sprite.color = (leftSideSlow.value) ? Color.yellow : Color.magenta;
		}

		float rotation = 180*currentTime/changeTime;
		if (leftSideSlow.value)
			rotation += 180;

		arrow.localRotation = Quaternion.AngleAxis(rotation,Vector3.forward);
	}
}
