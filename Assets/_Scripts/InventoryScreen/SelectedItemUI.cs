using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for displaying the information of the selected item.
/// </summary>
public class SelectedItemUI : MonoBehaviour {

	public Transform[] statsTextList;

	//Player stats
	public IntVariable playerAttack;
	public IntVariable playerDefense;
	public IntVariable playerSAttack;
	public IntVariable playerSDefense;

	private Text[] names;
	private Text[] values;

	//Selected item
	public ItemEntryReference selectedItem;
	private ItemEquip currentItem;
	public Text itemName;
	private Text[] changes;
	public Image itemIcon;


	void Start () {
		//Set up the texts showing the stats
		Text[] t;
		names = new Text[statsTextList.Length];
		values = new Text[statsTextList.Length];
		changes = new Text[statsTextList.Length];
		for (int i = 0; i < statsTextList.Length; i++) {
			t = statsTextList[i].GetComponentsInChildren<Text>();
			names[i] = t[0];
			values[i] = t[1];
			changes[i] = t[2];
//			names[i].text = "name";
			values[i].text = i.ToString();
			changes[i].text = "+0";
		}

	}

	void OnEnable() {
		selectedItem.reference = null;
	}

	void Update () {
		//Update values
		UpdateValues();
	}

	/// <summary>
	/// Updates the information text of the currently selected item.
	/// </summary>
	void UpdateValues(){
		values[0].text = playerAttack.value.ToString();
		values[1].text = playerDefense.value.ToString();
		values[2].text = playerAttack.value.ToString();
		values[3].text = playerSDefense.value.ToString();

		if (selectedItem.reference != null) {
			currentItem = (ItemEquip)selectedItem.reference;
			itemName.text = currentItem.entryName;
			changes[0].text = "+" + currentItem.attackModifier.ToString();
			changes[1].text = "+" + currentItem.defenseModifier.ToString();
			changes[2].text = "+" + currentItem.sAttackModifier.ToString();
			changes[3].text = "+" + currentItem.sDefenseModifier.ToString();
			itemIcon.sprite = currentItem.icon;
			itemIcon.enabled = true;
		}
		else {
			itemName.text = "";
			changes[0].text = "";
			changes[1].text = "";
			changes[2].text = "";
			changes[3].text = "";
			itemIcon.enabled = false;
		}
	}
}
