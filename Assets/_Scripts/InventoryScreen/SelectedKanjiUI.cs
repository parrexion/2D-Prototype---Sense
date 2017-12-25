using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for displaying the information of the selected kanji.
/// </summary>
public class SelectedKanjiUI : MonoBehaviour {

	public Transform[] statsTextList;

	//Selected item
	public ItemEntryReference selectedKanji;
	private Kanji currentKanji;
	public Text itemName;
	public Image itemIcon;
	private Text[] names;
	private Text[] values;


	void Start () {

		//Set up the texts showing the stats
		Text[] t;
		names = new Text[statsTextList.Length];
		values = new Text[statsTextList.Length];
		for (int i = 0; i < statsTextList.Length; i++) {
			t = statsTextList[i].GetComponentsInChildren<Text>();
			names[i] = t[0];
			values[i] = t[1];
			values[i].text = i.ToString();
		}

	}

	void OnEnable() {
		selectedKanji.reference = null;
	}

	void Update () {
		//Update values
		UpdateValues();
	}


	/// <summary>
	/// Updates the information text of the currently selected kanji.
	/// </summary>
	void UpdateValues(){

		if (selectedKanji.reference != null) {
			currentKanji = (Kanji)selectedKanji.reference;
			itemName.text = currentKanji.entryName;
			itemIcon.sprite = currentKanji.icon;
			itemIcon.enabled = true;
			values[0].text = currentKanji.values.kanjiType.ToString();
			values[1].text = currentKanji.values.damage.ToString();
			values[2].text = currentKanji.values.maxCharges.ToString();
			values[3].text = (currentKanji.values.cooldown != -1) ? currentKanji.values.cooldown.ToString() + " s" : "-";
		}
		else {
			itemName.text = "";
			itemIcon.enabled = false;
			values[0].text = "";
			values[1].text = "";
			values[2].text = "";
			values[3].text = "";
		}
	}
}
