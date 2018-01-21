using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpMeterUI : MonoBehaviour {

	[Header("EXP values")]
	public IntVariable totalExp;
	public IntVariable nextLevelExp;
	public IntVariable gainedExp;
	private int expLeft;

	[Header("Bar position values")]
	public float bar_xpos;
	public float bar_ypos;
	public float bar_width;
	public float bar_height;
	public Rect bar_rect;

	[Header("Bar Image")]
	public Image valueImage;
	

	// Use this for initialization
	void Start () {
		
	}
	

	IEnumerator FillExpBar() {

		yield break;
	}
}
