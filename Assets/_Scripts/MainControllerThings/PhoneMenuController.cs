using UnityEngine;
using UnityEngine.SceneManagement;

public class PhoneMenuController : MonoBehaviour {

	public void GoToEquipScreen() {
		SceneManager.LoadScene(BattleConstants.SCENE_INVENTORY);
	}
}
