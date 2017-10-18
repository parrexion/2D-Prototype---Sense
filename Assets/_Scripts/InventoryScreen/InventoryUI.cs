using UnityEngine;

/// <summary>
/// The UI component of the inventory screen containing all the images of the inventory.
/// </summary>
public class InventoryUI : MonoBehaviour {

	public Transform gearEquipItemsParent;
	public Transform gearBagItemsParent;
	public Transform kanjiEquipItemsParent;
	public Transform kanjiBagItemsParent;
	public Transform destroyTransform;

	Inventory inventory;
	InventorySlot[] gearEquipSlots;
	InventorySlot[] gearBagSlots;
	InventorySlot[] kanjiEquipSlots;
	InventorySlot[] kanjiBagSlots;
	InventorySlot destroySlot;


	// Use this for initialization
	void Start () {
		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateUI;

		//Equipment initialization
		gearEquipSlots = gearEquipItemsParent.GetComponentsInChildren<InventorySlot>();
		for (int i = 0; i < gearEquipSlots.Length; i++) {
			gearEquipSlots[i].SetID(SlotID.SlotType.EQUIP,-(i+1));
		}
		gearBagSlots = gearBagItemsParent.GetComponentsInChildren<InventorySlot>();
		for (int i = 0; i < gearBagSlots.Length; i++) {
			gearBagSlots[i].SetID(SlotID.SlotType.EQUIP,i);
		}

		//Kanji initialization
		kanjiEquipSlots = kanjiEquipItemsParent.GetComponentsInChildren<InventorySlot>();
		for (int i = 0; i < kanjiEquipSlots.Length; i++) {
			kanjiEquipSlots[i].SetID(SlotID.SlotType.KANJI,-(i+1));
		}
		kanjiBagSlots = kanjiBagItemsParent.GetComponentsInChildren<InventorySlot>();
		for (int i = 0; i < kanjiBagSlots.Length; i++) {
			kanjiBagSlots[i].SetID(SlotID.SlotType.KANJI,i);
		}

		destroySlot = destroyTransform.GetComponent<InventorySlot>();
		destroySlot.SetID(SlotID.SlotType.DESTROY,-999);

		Debug.Log("Initiated the slot ids");
		UpdateUI();
	}

	/// <summary>
	/// Remove the events when leaving the screen.
	/// </summary>
	void OnDisable() {
		inventory.onItemChangedCallback -= UpdateUI;
	}

	/// <summary>
	/// Update function for the UI. Uses the inventory to update the images of all the inventory slots.
	/// </summary>
	void UpdateUI() {
		if (!inventory.initialized) {
			return;
		}

		//Update the equipment
		for (int i = 0; i < gearEquipSlots.Length; i++) {
			if (inventory.gearEquippedItems[i] != null) {
				gearEquipSlots[i].AddItem(inventory.gearEquippedItems[i]);
			}
			else {
				gearEquipSlots[i].ClearSlot();
			}
		}

		for (int i = 0; i < gearBagSlots.Length; i++) {
			if (inventory.gearBagItems[i] != null) {
				gearBagSlots[i].AddItem(inventory.gearBagItems[i]);
			}
			else {
				gearBagSlots[i].ClearSlot();
			}
		}

		//Update the kanji
		for (int i = 0; i < kanjiEquipSlots.Length; i++) {
			if (inventory.kanjiEquippedItems[i] != null) {
				kanjiEquipSlots[i].AddItem(inventory.kanjiEquippedItems[i]);
			}
			else {
				kanjiEquipSlots[i].ClearSlot();
			}
		}

		for (int i = 0; i < kanjiBagSlots.Length; i++) {
			if (inventory.kanjiBagItems[i] != null) {
				kanjiBagSlots[i].AddItem(inventory.kanjiBagItems[i]);
			}
			else {
				kanjiBagSlots[i].ClearSlot();
			}
		}
	}
}
