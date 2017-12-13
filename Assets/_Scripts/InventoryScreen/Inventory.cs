using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	#region Singleton
	public static Inventory instance;
	void Awake() {
		if (instance != null) {
			Destroy(gameObject);
		}
		else {
			instance = this;
		}
	}
	#endregion

	//Event system callbacks
	public delegate void OnEquipmentChanged(ItemEquip newItem, ItemEquip oldItem);
	public OnEquipmentChanged onEquipmentChangedCallback;
	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	public bool initialized = false;

	public int gearEquipSpace = 4;
	public int gearBagSpace = 20;
	public int kanjiEquipSpace = 4;
	public int kanjiBagSpace = 20;

	public Item[] gearEquippedItems;
	public Item[] gearBagItems;

	public Item[] kanjiEquippedItems;
	public Item[] kanjiBagItems;

	public Item destroyItem;

	public ItemEquip equippedGear;
	public ItemKanji equippedKanji;

	private AddItemDB db;


	void Start() {
		gearEquippedItems = new Item[gearEquipSpace];
		gearBagItems = new Item[gearBagSpace];
		kanjiEquippedItems = new Item[kanjiEquipSpace];
		kanjiBagItems = new Item[kanjiBagSpace];

		destroyItem = ScriptableObject.CreateInstance<Item>();
		destroyItem.item_type = Item.ItemType.DESTROY;

		db = GetComponent<AddItemDB>();
		FillDefault();
		Debug.Log("Inventory initialized");
		initialized = true;
	}

	public void FillDefault(){
		db.inventory = this;
		db.AddSpecificKanji(1,true);
		db.AddSpecificKanji(2,true);
		db.AddSpecificKanji(3,true);
		db.AddSpecificKanji(4,true);
		db.AddSpecificKanji(7,false);
		db.AddSpecificKanji(8,false);
		db.AddSpecificKanji(9,false);
		db.AddSpecificKanji(10,false);

//		db.AddSpecificEquip(1,true);
//		db.AddSpecificEquip(2,true);
//		db.AddSpecificEquip(3,true);
//		db.AddSpecificEquip(4,true);
	}

	public int[] GetEquippedKanji(){
		int[] ids = new int[kanjiEquipSpace];
		for (int i = 0; i < kanjiEquipSpace; i++) {
			ids[i] = (kanjiEquippedItems[i]) ? kanjiEquippedItems[i].item_id : 0;
		}
		return ids;
	}

	public bool AddBag(Item item) {
		Item[] bagItems;
		if (item.item_type == Item.ItemType.KANJI) {
			bagItems = kanjiBagItems;
		}
		else {
			bagItems = gearBagItems;
		}
		for (int i = 0; i < bagItems.Length; i++) {
			if (bagItems[i] == null) {
				bagItems[i] = item;
				if (onItemChangedCallback != null)
					onItemChangedCallback.Invoke();
				return true;
			}
		}

		Debug.Log("Not enough room.");
		return false;
	}

	public bool AddEquip(Item item) {
		Item[] equipItems;
		if (item.item_type == Item.ItemType.KANJI) {
			equipItems = kanjiEquippedItems;
		}
		else {
			equipItems = gearEquippedItems;
		}
		for (int i = 0; i < equipItems.Length; i++) {
			if (equipItems[i] == null) {
				equipItems[i] = item;
				if (onItemChangedCallback != null)
					onItemChangedCallback.Invoke();
				if (item.item_type == Item.ItemType.EQUIP) {
					if (onEquipmentChangedCallback != null)
						onEquipmentChangedCallback.Invoke((ItemEquip)item, null);
				}
				return true;
			}
		}

		Debug.Log("Not enough room.");
		return false;
	}

	public void Swap(SlotID slotA, SlotID slotB) {

		bool kanjiInv = (slotA.type == SlotID.SlotType.KANJI);
		int posA = slotA.id;
		int posB = slotB.id;

		if (slotA.type == SlotID.SlotType.DESTROY)
			Remove(kanjiInv,posB);
		else if (slotB.type == SlotID.SlotType.DESTROY)
			Remove(kanjiInv,posA);

		if (slotA.type != slotB.type)
			return;


		Item temp = GetItem(kanjiInv, posA);
		SetItem(kanjiInv, posA,GetItem(kanjiInv, posB));
		SetItem(kanjiInv, posB,temp);

		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
		if (slotA.type == SlotID.SlotType.EQUIP && Mathf.Sign(posA) != Mathf.Sign(posB)) {
			if (onEquipmentChangedCallback != null) {
				if (posA < posB)
					onEquipmentChangedCallback.Invoke((ItemEquip)GetItem(kanjiInv,posA),(ItemEquip)GetItem(kanjiInv,posB));
				else
					onEquipmentChangedCallback.Invoke((ItemEquip)GetItem(kanjiInv,posB),(ItemEquip)GetItem(kanjiInv,posA));
				Debug.Log(string.Format("Pos A: {0}, Pos B: {1}", posA, posB));
			}
		}
	}

	private void Remove(bool kanjiInv, int index){
		if (index < 0) {
			index = -(index+1);
			RemoveEquip(kanjiInv,index);
		}
		else {
			RemoveItem(kanjiInv,index);
		}
	}

	public void RemoveEquip(bool kanjiInv, int pos) {
		Item[] equipItems;
		if (kanjiInv) {
			equipItems = kanjiEquippedItems;
		}
		else {
			equipItems = gearEquippedItems;
		}
		Item removedItem = equipItems[pos];
		equipItems[pos] = null;
		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
		if (!kanjiInv)
			if (onEquipmentChangedCallback != null)
				onEquipmentChangedCallback.Invoke(null,(ItemEquip)removedItem);
	}

	public void RemoveItem(bool kanjiInv, int pos) {
		Item[] bagItems;
		if (kanjiInv) {
			bagItems = kanjiBagItems;
		}
		else {
			bagItems = gearBagItems;
		}
		bagItems[pos] = null;
		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}

	public void SelectItem(bool kanjiInv, int index){
		if (kanjiInv)
			equippedKanji = (ItemKanji) GetItem(kanjiInv,index);
		else
			equippedGear = (ItemEquip) GetItem(kanjiInv,index);
	}

	public void DeselectItem(bool kanjiInv){
		if (kanjiInv)
			equippedKanji = null;
		else
			equippedGear = null;
	}

	public Item GetItem(bool kanjiInv, int index) {
		if (index < 0) {
			index = -(index+1);
			if (kanjiInv)
				return (index < kanjiEquipSpace) ? kanjiEquippedItems[index] : null;
			else {
				return (index < gearEquipSpace) ? gearEquippedItems[index] : null;
			}
		}
		else {
			if (kanjiInv)
				return (index < kanjiBagSpace) ? kanjiBagItems[index] : null;
			else
				return (index < gearBagSpace) ? gearBagItems[index] : null;
		}
	}

	private void SetItem(bool kanjiInv, int index, Item item) {

		if (kanjiInv) {
			if (index < 0) {
				index = -(index+1);
				if (index < kanjiEquipSpace) {
					kanjiEquippedItems[index] = item;
				}
			}
			else {
				if (index < kanjiBagSpace) {
					kanjiBagItems[index] = item;
				}
			}
		}
		else {

			if (index < 0) {
				index = -(index+1);
				if (index < gearEquipSpace) {
					gearEquippedItems[index] = item;
				}
			}
			else {
				if (index < gearBagSpace) {
					gearBagItems[index] = item;
				}
			}
		}
	}
		
}
