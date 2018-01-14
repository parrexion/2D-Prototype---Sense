using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

public class DialogueEditorWindow : EditorWindow {

	public struct SelectedPose
	{
		public int index;
		public int pose;
	}

	public ScrObjLibraryVariable dialogueLibrary;
	public ScrObjLibraryVariable backgroundLibrary;
	public ScrObjLibraryVariable characterLibrary;

	public DialogueEntry dialogueValues;
	DialogueWindowHelpClass d = new DialogueWindowHelpClass();
	public Sprite noBackgroundSprite;

	Rect bkgRect;
	Rect faceRect;
	Rect closeupRect;
	Rect saveRect;

	int[] indexList = new int[]{0,2,4,3,1};
	int[] reverseIndexList = new int[]{0,4,1,3,2};
	string[] nextActionStrings = new string[] { "OVERWORLD", "DIALOGUE", "BATTLE" };
	string[] talkingLabels = new string[] { "Talking", "Talking", "Talking", "Talking", "Talking" };
	string[] poseList = new string[] { "Normal", "Sad", "Happy", "Angry", "Dead", "Hmm", "Pleased", "Surprised", "Worried" };

	// Selected things
	int selectTalker = -1;
	string talkName = "";
	Vector2 frameScrollPos;
	Vector2 dialogueScrollPos;
	int selFrame = -1;
	int selDialogue = -1;

	//Creation
	string dialogueUuid = "";
	Color repColor = new Color(0,0,0,0);


	[MenuItem("Window/DialogueEditor")]
	public static void ShowWindow() {
		GetWindow<DialogueEditorWindow>("Dialogue Editor");
	}

	void OnEnable() {
		EditorSceneManager.sceneOpened += SceneOpenedCallback;
		InitializeWindow();
	}

	void OnDisable() {
		EditorSceneManager.sceneOpened -= SceneOpenedCallback;
	}

	void OnGUI() {

		GUILayout.Label("Dialogue Editor", EditorStyles.boldLabel);
		EditorGUIUtility.labelWidth = 100;
		d.GenerateAreas();
		d.DrawBackgrounds();

		if (selFrame != -1) {
			HeaderStuff();
			CharacterStuff();
			TalkingStuff();
			DialogueTextStuff();
			FrameStuff();
		}
		RightStuff();
	}

	void SceneOpenedCallback( Scene _scene, OpenSceneMode _mode) {
		Debug.Log("SCENE LOADED");
		InitializeWindow();
	}

	void InitializeWindow() {
		backgroundLibrary.initialized = false;
		characterLibrary.initialized = false;
		dialogueLibrary.initialized = false;

		bkgRect = new Rect(420,4,152,72);
		faceRect = new Rect(38,76,64,64);
		closeupRect = new Rect(218,70,64,64);
		saveRect = new Rect(140*5-100,4,100,72);

		d.InitTextures();
	}

	void HeaderStuff() {
		GUILayout.BeginArea(d.headerRect);

		EditorGUILayout.SelectableLabel("Selected Dialogue    UUID: " + 1234, EditorStyles.boldLabel, GUILayout.Height(20));
		dialogueValues.entryName = EditorGUILayout.TextField("Dialogue name", dialogueValues.entryName, GUILayout.Width(400));

		GUILayout.Space(5);

		dialogueValues.frames[selFrame].background = (BackgroundEntry)EditorGUILayout.ObjectField("Background", dialogueValues.frames[selFrame].background, typeof(BackgroundEntry),false, GUILayout.Width(400));
		if (dialogueValues.frames[selFrame].background != null){
			GUI.DrawTexture(bkgRect, dialogueValues.frames[selFrame].background.sprite.texture);
		}
		else
			GUI.DrawTexture(bkgRect, noBackgroundSprite.texture);

		GUILayout.EndArea();
		GUILayout.BeginArea(saveRect);
		if (GUILayout.Button("Save\nDialogue", GUILayout.Height(saveRect.height))){
			SaveSelectedDialogue();
		}
		GUILayout.EndArea();
	}

	void CharacterStuff() {
		EditorGUIUtility.labelWidth = 70;
		float fieldWidth = d.rectChar[0].width - 8;
		for (int j = 0; j < 5; j++) {
			GUILayout.BeginArea(d.rectChar[j]);

			int index = indexList[j];

			if (index == 4) {
				GUILayout.Label("Name: ", EditorStyles.boldLabel);
				talkName = EditorGUILayout.TextField("", talkName);
				GUILayout.EndArea();
				continue;
			}

			if (dialogueValues.frames[selFrame].characters[index] != null)
				GUILayout.Label(dialogueValues.frames[selFrame].characters[index].entryName, EditorStyles.boldLabel);
			else
				GUILayout.Label("Name: ", EditorStyles.boldLabel);

			GUILayout.Label("Character");
			dialogueValues.frames[selFrame].characters[index] = (CharacterEntry)EditorGUILayout.ObjectField("", dialogueValues.frames[selFrame].characters[index], typeof(CharacterEntry),false, GUILayout.Width(fieldWidth));
			if (dialogueValues.frames[selFrame].characters[index] == null){
				dialogueValues.frames[selFrame].poses[index] = -1;
				GUILayout.EndArea();
				continue;
			}

			if (dialogueValues.frames[selFrame].poses[index] == -1)
				dialogueValues.frames[selFrame].poses[index] = 0;

			if (GUILayout.Button(poseList[dialogueValues.frames[selFrame].poses[index]], GUILayout.Width(fieldWidth))) {
				GenericMenu menu = new GenericMenu();
				SelectedPose selPose = new SelectedPose();
				selPose.index = index;
				for (int p = 0; p < poseList.Length; p++) {
					selPose.pose = p;
					menu.AddItem(new GUIContent(poseList[p]), (p == dialogueValues.frames[selFrame].poses[index]), SetPose, selPose);
				}
				menu.ShowAsContext();
			}
			GUILayout.BeginHorizontal();
			GUI.DrawTexture(faceRect, dialogueValues.frames[selFrame].characters[index].defaultColor.texture);
			GUILayout.FlexibleSpace();
			GUILayout.Label(dialogueValues.frames[selFrame].characters[index].poses[dialogueValues.frames[selFrame].poses[index]].texture);
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();

			GUILayout.EndArea();
		}
		EditorGUIUtility.labelWidth = 100;
	}

	void SetPose(object selectedPose) {
		SelectedPose pose = (SelectedPose)selectedPose;
		dialogueValues.frames[selFrame].poses[pose.index] = pose.pose;
	}

	void TalkingStuff() {
		GUILayout.BeginArea(d.talkingRect);

		selectTalker = GUILayout.SelectionGrid(selectTalker, talkingLabels, 5);
		if (GUILayout.Button("No one is talking")) {
			selectTalker = -1;
		}

		dialogueValues.frames[selFrame].talkingIndex = (selectTalker == -1) ? -1 : indexList[selectTalker];

		if (selectTalker == -1) {
			dialogueValues.frames[selFrame].talkingName = "";
		}
		else if (indexList[selectTalker] == 4) {
			dialogueValues.frames[selFrame].talkingName = talkName;
		}
		else {
			if (dialogueValues.frames[selFrame].characters[indexList[selectTalker]] != null) {
				dialogueValues.frames[selFrame].talkingName = dialogueValues.frames[selFrame].characters[indexList[selectTalker]].entryName;
			}
			else {
				selectTalker = -1;
				dialogueValues.frames[selFrame].talkingIndex = -1;
				dialogueValues.frames[selFrame].talkingName = "";
			}
		}
		GUILayout.EndArea();
	}

	void DialogueTextStuff() {
		GUILayout.BeginArea(d.dialogueRect);

		EditorStyles.textField.wordWrap = true;
		GUILayout.Label("Dialogue Text", EditorStyles.boldLabel);
		dialogueValues.frames[selFrame].dialogueText = EditorGUILayout.TextArea(dialogueValues.frames[selFrame].dialogueText, GUILayout.Width(300), GUILayout.Height(48));

		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();
		GUILayout.Label("Name: ", EditorStyles.boldLabel, GUILayout.Width(80));
		GUILayout.Label("Pose: ", EditorStyles.boldLabel, GUILayout.Width(80));
		GUILayout.EndVertical();

		if (dialogueValues.frames[selFrame].talkingChar != null) {
			GUILayout.BeginVertical();
			GUILayout.Label(dialogueValues.frames[selFrame].talkingName, EditorStyles.boldLabel, GUILayout.Width(80));
			GUILayout.Label("Pose", EditorStyles.boldLabel, GUILayout.Width(80));
			GUILayout.EndVertical();

			GUILayout.BeginHorizontal();
			GUI.DrawTexture(closeupRect, dialogueValues.frames[selFrame].talkingChar.defaultColor.texture);
			if (dialogueValues.frames[selFrame].talkingChar != null)
				GUILayout.Label(dialogueValues.frames[selFrame].talkingChar.poses[dialogueValues.frames[selFrame].talkingPose].texture);
			GUILayout.EndHorizontal();
		}
		else if (dialogueValues.frames[selFrame].talkingIndex == 4) {
			GUILayout.Label(dialogueValues.frames[selFrame].talkingName, EditorStyles.boldLabel, GUILayout.Width(80));
		}

		GUILayout.EndHorizontal();

		GUILayout.EndArea();
	}

	void FrameStuff() {
		GUILayout.BeginArea(d.frameRect);

		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Delete\nFrame", GUILayout.Width(100), GUILayout.Height(48))) {
			DeleteFrame();
		}
		if (GUILayout.Button("Insert\nFrame", GUILayout.Height(48))) {
			InsertFrame();
		}
		GUILayout.EndHorizontal();

		GUILayout.Space(4);

		GUILayout.Label("Next Action", EditorStyles.boldLabel);
		dialogueValues.nextLocation = (BattleEntry.NextLocation)GUILayout.Toolbar((int)dialogueValues.nextLocation, nextActionStrings);
		switch (dialogueValues.nextLocation)
		{
			case BattleEntry.NextLocation.OVERWORLD:
				GUILayout.BeginHorizontal();
				EditorGUIUtility.labelWidth = 150;
				dialogueValues.changePosition = EditorGUILayout.Toggle("Change player position", dialogueValues.changePosition, GUILayout.Width(190));
				if (dialogueValues.changePosition) {
					EditorGUIUtility.labelWidth = 60;
					dialogueValues.nextArea = (Constants.OverworldArea)EditorGUILayout.EnumPopup("Area",dialogueValues.nextArea);
					EditorGUIUtility.labelWidth = 100;
					GUILayout.EndHorizontal();
					dialogueValues.playerPosition = EditorGUILayout.Vector2Field("Player Position", dialogueValues.playerPosition, GUILayout.Width(d.frameRect.width-8));
				}
				else {
					EditorGUIUtility.labelWidth = 100;
					GUILayout.EndHorizontal();
				}
				break;
			case BattleEntry.NextLocation.DIALOGUE:
				dialogueValues.nextEntry = (DialogueEntry)EditorGUILayout.ObjectField("Next dialogue", dialogueValues.nextEntry, typeof(DialogueEntry),false);
				break;
			case BattleEntry.NextLocation.BATTLE:
				dialogueValues.nextEntry = (BattleEntry)EditorGUILayout.ObjectField("Next battle", dialogueValues.nextEntry, typeof(BattleEntry),false);
				break;
		}
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Shave off\nBefore", GUILayout.Width(100), GUILayout.Height(48))) {
			ShaveoffBefore();
		}
		if (GUILayout.Button("Shave off\nAfter", GUILayout.Width(100), GUILayout.Height(48))) {
			ShaveoffAfter();
		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}

	void RightStuff() {
		GUILayout.BeginArea(d.rightRect);

		GUILayout.Space(10);

		// Frame scroll
		GUILayout.Label("Frames", EditorStyles.boldLabel);
		frameScrollPos = GUILayout.BeginScrollView(frameScrollPos, GUILayout.Width(d.rightRect.width), 
					GUILayout.Height(d.rightRect.height * 0.35f));
		if (selFrame != -1) {
			int oldFrame = selFrame;
			selFrame = GUILayout.SelectionGrid(selFrame, dialogueValues.GenerateFrameRepresentation(), 1);
			if (oldFrame != selFrame) {
				GUI.FocusControl(null);
				int tIndex = dialogueValues.frames[selFrame].talkingIndex;
				selectTalker = (tIndex != -1) ? reverseIndexList[dialogueValues.frames[selFrame].talkingIndex] : -1;
				talkName = dialogueValues.frames[selFrame].talkingName;
			}
		}

		GUILayout.EndScrollView();

		GUILayout.Space(10);

		// Dialogue scroll
		GUILayout.Label("Dialogues", EditorStyles.boldLabel);
		dialogueScrollPos = GUILayout.BeginScrollView(dialogueScrollPos, GUILayout.Width(d.rightRect.width), 
					GUILayout.Height(d.rightRect.height * 0.3f));
		
		int oldSelected = selDialogue;
		selDialogue = GUILayout.SelectionGrid(selDialogue, dialogueLibrary.GetRepresentations(),2);

		if (oldSelected != selDialogue) {
			GUI.FocusControl(null);
			SelectDialogue();
		}

		GUILayout.EndScrollView();

		dialogueUuid = EditorGUILayout.TextField("Dialogue uuid", dialogueUuid);
		if (GUILayout.Button("Create dialogue")) {
			InstansiateDialogue();
		}
		if (GUILayout.Button("Delete dialogue")) {
			DeleteDialogue();
		}
		GUILayout.EndArea();
	}



	// Data manipulation (Creating, saving, etc...)
	
	void SelectDialogue() {
		// Nothing selected
		if (selDialogue == -1) {
			selFrame = -1;
			dialogueValues.ResetValues();
		}
		else {
			// Something selected
			GUI.FocusControl(null);
			DialogueEntry de = (DialogueEntry)dialogueLibrary.GetEntryByIndex(selDialogue);
			dialogueValues.CopyValues(de);
			selFrame = 0;
			int tIndex = dialogueValues.frames[selFrame].talkingIndex;
			selectTalker = (tIndex != -1) ? reverseIndexList[dialogueValues.frames[selFrame].talkingIndex] : -1;
			talkName = dialogueValues.frames[selFrame].talkingName;
		}
	}

	void SaveSelectedDialogue() {
		DialogueEntry de = (DialogueEntry)dialogueLibrary.GetEntryByIndex(selDialogue);
		de.CopyValues(dialogueValues);
		Undo.RecordObject(de, "Updated dialogue");
		EditorUtility.SetDirty(de);
	}

	void InsertFrame() {
		GUI.FocusControl(null);
		Frame frame = new Frame();
		frame.CopyValues(dialogueValues.frames[selFrame]);
		frame.dialogueText = "";
		dialogueValues.InsertFrame(selFrame+1,frame);
		selFrame++;

		SaveSelectedDialogue();
	}

	void DeleteFrame() {
		if (dialogueValues.frames.Count <= 1)
			return;

		GUI.FocusControl(null);
		dialogueValues.RemoveFrame(selFrame);
		selFrame = Mathf.Min(selFrame, dialogueValues.frames.Count - 1);

		SaveSelectedDialogue();
	}

	void ShaveoffAfter() {
		if (dialogueValues.frames.Count <= 1)
			return;

		GUI.FocusControl(null);
		int shaveSize = selFrame + 1;
		while (dialogueValues.frames.Count > shaveSize)
			dialogueValues.RemoveFrame(shaveSize);
		selFrame = Mathf.Min(selFrame, dialogueValues.frames.Count - 1);

		SaveSelectedDialogue();
	}

	void ShaveoffBefore() {
		if (dialogueValues.frames.Count <= 1)
			return;

		GUI.FocusControl(null);
		int shaveSize = selFrame;
		for (int i = 0; i < shaveSize; i++) {
			dialogueValues.RemoveFrame(0);
		}
		selFrame = 0;

		SaveSelectedDialogue();
	}

	void InstansiateDialogue() {
		GUI.FocusControl(null);
		if (dialogueLibrary.ContainsID(dialogueUuid)) {
			Debug.Log("uuid already exists!");
			return;
		}
		DialogueEntry de = Editor.CreateInstance<DialogueEntry>();
		de.name = dialogueUuid;
		de.uuid = dialogueUuid;
		de.entryName = dialogueUuid;
		de.repColor = repColor;
		de.frames.Add(new Frame());
		string path = "Assets/LibraryData/Dialogues/" + dialogueUuid + ".asset";

		dialogueLibrary.InsertEntry(de,0);
		Undo.RecordObject(dialogueLibrary, "Added dialogue");
		EditorUtility.SetDirty(dialogueLibrary);
		AssetDatabase.CreateAsset(de, path);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		dialogueUuid = "";
		selDialogue = 0;
		SelectDialogue();
	}

	void DeleteDialogue() {
		GUI.FocusControl(null);
		DialogueEntry de = (DialogueEntry)dialogueLibrary.GetEntryByIndex(selDialogue);
		string path = "Assets/LibraryData/Dialogues/" + de.uuid + ".asset";

		dialogueLibrary.RemoveEntryByIndex(selDialogue);
		Undo.RecordObject(dialogueLibrary, "Deleted dialogue");
		EditorUtility.SetDirty(dialogueLibrary);
		bool res = AssetDatabase.MoveAssetToTrash(path);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		if (res) {
			Debug.Log("Removed dialogue: " + de.uuid);
			selDialogue = -1;
		}
	}
}
