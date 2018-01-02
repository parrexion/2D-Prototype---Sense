using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DAEndDialogue : DialogueAction {

	public override bool Act(DialogueScene scene, DialogueJsonItem data) {
		DialogueEntry de = (DialogueEntry)data.entry;
		switch (de.nextLocation)
		{
			case BattleEntry.NextLocation.OVERWORLD:
				scene.paused.value = false;
				if (de.changePosition) {
					scene.playerPosX.value = de.playerPosition.x;
					scene.playerPosY.value = de.playerPosition.y;
					Debug.Log("Position is now: " + scene.playerPosX.value + ", " + scene.playerPosY.value);
				}
				if (de.nextArea == BattleEntry.OverworldArea.TOWER)
					SceneManager.LoadScene((int)Constants.SCENE_INDEXES.BATTLETOWER);
				else
					SceneManager.LoadScene((int)Constants.SCENE_INDEXES.TUTORIAL_OLD);
				break;
			case BattleEntry.NextLocation.DIALOGUE:
				scene.dialogueUuid.value = de.nextEntry.name;
				SceneManager.LoadScene((int)Constants.SCENE_INDEXES.DIALOGUE);
				break;
			case BattleEntry.NextLocation.BATTLE:
				scene.battleUuid.value = de.nextEntry.uuid;
				SceneManager.LoadScene((int)Constants.SCENE_INDEXES.BATTLE);
				break;
		}

		return false;
	}
}
