using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

	public bool moveable = true;
	public SlotID slotID;
	public Image icon;
	Item item;


	public void SetID(SlotID.SlotType type, int id) {
		slotID = new SlotID();
		slotID.type = type;
		slotID.id = id;
	}

	public void AddItem(Item newItem) {
		item = newItem;

		icon.sprite = item.icon;
		icon.enabled = true;
	}
		
	public void ClearSlot() {

		item = null;
		icon.sprite = null;
		icon.enabled = false;
	}

}
