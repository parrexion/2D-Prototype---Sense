using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
[RequireComponent(typeof(DialogueScene))]
public class DialogueLines : MonoBehaviour {

	public ScrObjLibraryVariable dialogueLibrary;
	public StringVariable dialogueUuid;
	private DialogueEntry dialogueEntry;

	public IntVariable currentFrame;
	private DialogueScene scene;
	
	public UnityEvent backgroundChanged;
	public UnityEvent characterChanged;
	public UnityEvent closeupChanged;
	public UnityEvent dialogueTextChanged;


	void Start() {
		scene = GetComponent<DialogueScene>();
		dialogueLibrary.GenerateDictionary();
		dialogueEntry = (DialogueEntry)dialogueLibrary.GetEntry(dialogueUuid.value);
		currentFrame.value = 0;
		scene.SetFromFrame(dialogueEntry.frames[0]);

		backgroundChanged.Invoke();
		characterChanged.Invoke();
		closeupChanged.Invoke();
		dialogueTextChanged.Invoke();
	}

	public void NextFrame(){

		if (dialogueEntry.size == 0) {
			dialogueEntry = (DialogueEntry)dialogueLibrary.GetEntry(dialogueUuid.value);
			Debug.Log("Lines: " + dialogueEntry.size);
		}

		if (currentFrame.value >= dialogueEntry.size) {
			Debug.Log("Reached the end");
			DialogueAction da = (DAEndDialogue)ScriptableObject.CreateInstance("DAEndDialogue");
			da.Act(scene,null);
		}
		else {
			CompareScenes(dialogueEntry.frames[currentFrame.value]);
			currentFrame.value++;
		}
	}

	private void CompareScenes(Frame frame) {
		DialogueAction da;
		DialogueJsonItem data;
		if (!ScrObjLibraryEntry.CompareEntries(scene.background.value,frame.background)) {
			da = (DASetBackground)ScriptableObject.CreateInstance("DASetBackground");
			data = new DialogueJsonItem();
			data.entry = frame.background;
			da.Act(scene,data);
			backgroundChanged.Invoke();
		}
		bool changed = false;
		for (int i = 0; i < 4; i++) {
			if (!ScrObjLibraryEntry.CompareEntries(scene.characters[i].value,frame.characters[i]) || scene.poses[i].value != frame.poses[i]) {
				da = (DAAddCharacter)ScriptableObject.CreateInstance("DAAddCharacter");
				data = new DialogueJsonItem();
				data.position1 = i;
				data.entry = frame.characters[i];
				data.value = frame.poses[i];
				da.Act(scene,data);
				changed = true;
			}
		}
		if (changed) {

			Debug.Log("current: ");	
			characterChanged.Invoke();
		}

		changed = false;
		if (scene.talkingIndex.value != frame.talkingIndex || scene.talkingPose.value != frame.talkingPose) {
			da = (DAChangeTalking)ScriptableObject.CreateInstance("DAChangeTalking");
			data = new DialogueJsonItem();
			data.position1 = frame.talkingIndex;
			data.position2 = scene.poses[frame.talkingIndex].value;
			da.Act(scene,data);
			changed = true;
		}

		if (scene.talkingName.value != frame.talkingName) {
			da = (DASetName)ScriptableObject.CreateInstance("DASetName");
			data = new DialogueJsonItem();
			data.text = frame.talkingName;
			da.Act(scene,data);
			changed = true;
		}

		if (changed)
			closeupChanged.Invoke();

		if (scene.dialogueText.value != frame.dialogueText) {
			da = (DASetText)ScriptableObject.CreateInstance("DASetText");
			data = new DialogueJsonItem();
			data.text = frame.dialogueText;
			da.Act(scene,data);
			dialogueTextChanged.Invoke();
		}
	}
}
