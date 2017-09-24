using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedKanjiUI : MonoBehaviour {

	public Transform[] statsTextList;

	private Inventory inventory;
	private ItemKanji selectedKanji;

	//Selected item
	public Text itemName;
	public Image itemIcon;
	private Text[] names;
	private Text[] values;


	void Start () {
		//Set up references
		inventory = Inventory.instance;

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

	void Update () {

		//Update values
		UpdateValues();
	}


	void UpdateValues(){

		selectedKanji = inventory.equippedKanji;
		if (selectedKanji != null) {
			itemName.text = selectedKanji.item_name;
			itemIcon.sprite = selectedKanji.icon;
			itemIcon.enabled = true;
			values[0].text = selectedKanji.type.ToString();
			values[1].text = selectedKanji.damage.ToString();
			values[2].text = selectedKanji.charges.ToString();
			values[3].text = (selectedKanji.rechargeTime != -1) ? selectedKanji.rechargeTime.ToString() + " s" : "-";
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
