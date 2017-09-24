using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryScreenController : MonoBehaviour {

	public GameObject equipInventory, kanjiInventory;
	public GameObject equipInfo, kanjiInfo;

	public Button equipButton;
	public Button kanjiButton;


	public void ToggleInventory(){
		equipInventory.SetActive(!equipInventory.activeSelf);
		kanjiInventory.SetActive(!kanjiInventory.activeSelf);

		equipInfo.SetActive(!equipInfo.activeSelf);
		kanjiInfo.SetActive(!kanjiInfo.activeSelf);

		equipButton.interactable = !equipButton.interactable;
		kanjiButton.interactable = !kanjiButton.interactable;
	}

}
