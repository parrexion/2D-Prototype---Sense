using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class which contains functions for handling the inventories.
/// </summary>
public class InventoryScreenController : MonoBehaviour {

	public GameObject equipInventory, kanjiInventory;
	public GameObject equipInfo, kanjiInfo;

	public Button equipButton;
	public Button kanjiButton;


	/// <summary>
	/// Toggles which inventory and information screen to show.
	/// </summary>
	public void ToggleInventory(){
		equipInventory.SetActive(!equipInventory.activeSelf);
		kanjiInventory.SetActive(!kanjiInventory.activeSelf);

		equipInfo.SetActive(!equipInfo.activeSelf);
		kanjiInfo.SetActive(!kanjiInfo.activeSelf);

		equipButton.interactable = !equipButton.interactable;
		kanjiButton.interactable = !kanjiButton.interactable;
	}

}
