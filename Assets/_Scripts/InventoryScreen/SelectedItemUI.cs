using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for displaying the information of the selected item.
/// </summary>
public class SelectedItemUI : MonoBehaviour {

	public Transform[] statsTextList;

	private Inventory inventory;

	//Player stats
	public FloatVariable playerAttack;
	public FloatVariable playerDefense;
	public FloatVariable playerSAttack;
	public FloatVariable playerSDefense;
	private Text[] names;
	private Text[] values;

	//Selected item
	private ItemEquip selectedGear;
	public Text itemName;
	private Text[] changes;
	public Image itemIcon;


	void Start () {
		//Set up references
		inventory = Inventory.instance;

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

	void Update () {
		//Update values
		UpdateValues();
	}

	/// <summary>
	/// Updates the information text of the currently selected item.
	/// </summary>
	void UpdateValues(){
		values[0].text = playerAttack.ToString();
		values[1].text = playerDefense.ToString();
		values[2].text = playerAttack.ToString();
		values[3].text = playerSDefense.ToString();

		selectedGear = inventory.equippedGear;
		if (selectedGear != null) {
			itemName.text = selectedGear.item_name;
			changes[0].text = "+" + selectedGear.attackModifier.ToString();
			changes[1].text = "+" + selectedGear.defenseModifier.ToString();
			changes[2].text = "+" + selectedGear.sAttackModifier.ToString();
			changes[3].text = "+" + selectedGear.sDefenseModifier.ToString();
			itemIcon.sprite = selectedGear.icon;
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
