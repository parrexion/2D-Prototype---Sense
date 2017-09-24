using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotHandler : MonoBehaviour, IDropHandler {

	public GameObject item {
		get {
			if (transform.childCount > 0) {
				return transform.GetChild(0).gameObject;
			}
			return null;
		}
	}

	private DragHandler dragHandler;
	private Inventory inventory;
	private InventorySlot slot;

	void Start() {
		inventory = Inventory.instance;
		slot = GetComponent<InventorySlot>();
	}

	#region IDropHandler implementation

	public void OnDrop(PointerEventData eventData) {
		if (DragHandler.itemBeingDragged != null) {
			SlotID start_id = DragHandler.itemBeingDragged.GetComponent<DragHandler>().start_id;
			if (slot.slotID.type == SlotID.SlotType.DESTROY)
				inventory.DeselectItem(start_id.type == SlotID.SlotType.KANJI);
//			Debug.Log(string.Format("Swap index {0} and {1}",start_id.id,slot.slotID.id));
			Inventory.instance.Swap(start_id,slot.slotID);
		}
	}

	#endregion
}
