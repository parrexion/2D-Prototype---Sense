using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutsidePlayerController : MonoBehaviour {

	private bool active = true;
	private MoveHomingNoLimit moveToPosition;
	private Camera cam;
	public Text titleText;
	public bool titleSet = false;
	public Text towerPrev;
	public Text towerNext;

	// Use this for initialization
	void Start () {
		moveToPosition = GetComponent<MoveHomingNoLimit>();
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {

		if (!titleSet && MainControllerScript.instance.initiated) {
			titleSet = true;
			StoryValues.BattleType type = MainControllerScript.instance.storyValues.battleType;
			if (type == StoryValues.BattleType.STORY) {
				titleText.text = MainControllerScript.instance.storyValues.bv.scenarioName;
			}
			else if (type != StoryValues.BattleType.RANDOM) {
				towerPrev.text = (MainControllerScript.instance.storyValues.towerLevel -1).ToString();
				towerNext.text = MainControllerScript.instance.storyValues.towerLevel.ToString();
			}
			else {
				titleText.text = "";
			}
		}

		if (!active)
			return;

		if (Input.GetMouseButton(1)) {
			moveToPosition.moveToPosition = cam.ScreenToWorldPoint(Input.mousePosition);
		}
	}


	public void SetActive(bool state) {
		active = state;
		moveToPosition.active = state;
	}
}
