using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

public class DialogueWindow : EditorWindow {

	public struct SelectedPose
	{
		public int index;
		public int pose;
	}

	public ScrObjLibraryVariable dialogueLibrary;
	public ScrObjLibraryVariable backgroundLibrary;
	public ScrObjLibraryVariable characterLibrary;

	public DialogueEntry dialogueValues;
	Frame frame = new Frame();
	DialogueWindowHelpClass d = new DialogueWindowHelpClass();

	Rect bkgRect;
	Rect faceRect;
	Rect closeupRect;
	Rect saveRect;

	int[] indexList = new int[]{0,2,4,3,1};
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
		GetWindow<DialogueWindow>("Dialogue Editor");
	}

	void OnEnable() {
		EditorSceneManager.sceneOpened += SceneOpenedCallback;
		InitializeWindow();
	}

	void OnDisable() {
		EditorSceneManager.sceneOpened -= SceneOpenedCallback;
	}

	void OnGUI() {

		GUILayout.Label("Character selector", EditorStyles.boldLabel);
		EditorGUIUtility.labelWidth = 100;
		d.GenerateAreas();
		d.DrawBackgrounds();

		HeaderStuff();
		CharacterStuff();
		TalkingStuff();
		DialogueTextStuff();
		FrameStuff();
		RightStuff();
	}

	void SceneOpenedCallback( Scene _scene, OpenSceneMode _mode) {
		Debug.Log("SCENE LOADED");
		InitializeWindow();
	}

	void InitializeWindow() {
		// backgroundLibrary.GenerateDictionary();
		// characterLibrary.GenerateDictionary();
		// dialogueLibrary.GenerateDictionary();
		backgroundLibrary.initialized = false;
		characterLibrary.initialized = false;
		dialogueLibrary.initialized = false;

		bkgRect = new Rect(420,4,152,72);
		faceRect = new Rect(38,76,64,64);
		closeupRect = new Rect(236,72,64,64);
		saveRect = new Rect(140*5-100,4,100,72);

		d.InitTextures();
	}

	void HeaderStuff() {
		GUILayout.BeginArea(d.headerRect);

		EditorGUILayout.SelectableLabel("Selected Dialogue    UUID: " + 1234, EditorStyles.boldLabel, GUILayout.Height(20));
		dialogueValues.entryName = EditorGUILayout.TextField("Dialogue name", dialogueValues.entryName, GUILayout.Width(400));

		GUILayout.Space(5);

		frame.background = (BackgroundEntry)EditorGUILayout.ObjectField("Background", frame.background, typeof(BackgroundEntry),false, GUILayout.Width(400));
		if (frame.background != null){
			GUI.DrawTexture(bkgRect, frame.background.sprite.texture);
		}

		GUILayout.EndArea();
		GUILayout.BeginArea(saveRect);
		if (GUILayout.Button("Save\nframe", GUILayout.Height(saveRect.height))){

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

			if (frame.characters[index] != null)
				GUILayout.Label(frame.characters[index].entryName, EditorStyles.boldLabel);
			else
				GUILayout.Label("Name: ", EditorStyles.boldLabel);

			GUILayout.Label("Character");
			frame.characters[index] = (CharacterEntry)EditorGUILayout.ObjectField("", frame.characters[index], typeof(CharacterEntry),false, GUILayout.Width(fieldWidth));
			if (frame.characters[index] == null){
				frame.poses[index] = -1;
				GUILayout.EndArea();
				continue;
			}

			if (frame.poses[index] == -1)
				frame.poses[index] = 0;

			if (GUILayout.Button(poseList[frame.poses[index]], GUILayout.Width(fieldWidth))) {
				GenericMenu menu = new GenericMenu();
				SelectedPose selPose = new SelectedPose();
				selPose.index = index;
				for (int p = 0; p < poseList.Length; p++) {
					selPose.pose = p;
					menu.AddItem(new GUIContent(poseList[p]), (p == frame.poses[index]), SetPose, selPose);
				}
				menu.ShowAsContext();
			}
			GUILayout.BeginHorizontal();
			GUI.DrawTexture(faceRect, frame.characters[index].defaultColor.texture);
			GUILayout.FlexibleSpace();
			GUILayout.Label(frame.characters[index].poses[frame.poses[index]].texture);
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();

			GUILayout.EndArea();
		}
		EditorGUIUtility.labelWidth = 100;
	}

	void SetPose(object selectedPose) {
		SelectedPose pose = (SelectedPose)selectedPose;
		frame.poses[pose.index] = pose.pose;
	}

	void TalkingStuff() {
		GUILayout.BeginArea(d.talkingRect);

		selectTalker = GUILayout.SelectionGrid(selectTalker, talkingLabels, 5);
		if (GUILayout.Button("No one is talking")) {
			selectTalker = -1;
		}

		if (selectTalker == -1)
			frame.talkingIndex = -1;
		else {
			frame.talkingIndex = indexList[selectTalker];
			if (frame.characters[frame.talkingIndex] == null) {
				selectTalker = -1;
				frame.talkingIndex = -1;
			}
		}
		GUILayout.EndArea();
	}

	void DialogueTextStuff() {
		GUILayout.BeginArea(d.dialogueRect);

		EditorStyles.textField.wordWrap = true;
		GUILayout.Label("Dialogue Text", EditorStyles.boldLabel);
		frame.dialogueText = EditorGUILayout.TextArea(frame.dialogueText, GUILayout.Width(300), GUILayout.Height(48));

		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();
		GUILayout.Label("Name: ", EditorStyles.boldLabel);
		GUILayout.Label("Pose: ", EditorStyles.boldLabel);
		GUILayout.EndVertical();

		if (frame.talkingIndex != -1) {

			GUILayout.BeginVertical();
			GUILayout.Label(frame.talkingName, EditorStyles.boldLabel);
			GUILayout.Label("Pose", EditorStyles.boldLabel);
			GUILayout.EndVertical();

			GUILayout.BeginHorizontal();
			GUI.DrawTexture(closeupRect, frame.characters[frame.talkingIndex].defaultColor.texture);
			GUILayout.Label(frame.characters[frame.talkingIndex].poses[frame.poses[frame.talkingIndex]].texture);
			GUILayout.EndHorizontal();
			frame.talkingName = frame.characters[frame.talkingIndex].entryName;
		}
		else {
			GUILayout.Label("");
			frame.talkingName = "";
		}
		GUILayout.EndHorizontal();

		GUILayout.Space(8);
		if (GUILayout.Button("Open Dialogue Scene", GUILayout.Width(180))) {
			EditorSceneManager.OpenScene("Assets/_Scenes/Dialogue.unity");
			d.InitTextures();
		}

		GUILayout.EndArea();
	}

	void FrameStuff() {
		GUILayout.BeginArea(d.frameRect);

		if (GUILayout.Button("Delete Frame")) {

		}
		if (GUILayout.Button("Insert Frame", GUILayout.Height(48))) {

		}

		GUILayout.EndArea();
	}

	void RightStuff() {
		GUILayout.BeginArea(d.rightRect);

		GUILayout.Space(10);

		GUILayout.Label("Frames", EditorStyles.boldLabel);
		frameScrollPos = GUILayout.BeginScrollView(frameScrollPos, GUILayout.Width(d.rightRect.width), 
					GUILayout.Height(d.rightRect.height * 0.35f));
		
		selFrame = GUILayout.SelectionGrid(selFrame, new GUIContent[0],1);

		GUILayout.EndScrollView();

		GUILayout.Space(10);

		GUILayout.Label("Dialogues", EditorStyles.boldLabel);
		dialogueScrollPos = GUILayout.BeginScrollView(dialogueScrollPos, GUILayout.Width(d.rightRect.width), 
					GUILayout.Height(d.rightRect.height * 0.3f));
		
		int oldSelected = selDialogue;
		selDialogue = GUILayout.SelectionGrid(selDialogue, dialogueLibrary.GetRepresentations(),2);

		if (oldSelected != selDialogue)
			SelectDialogue();

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
			dialogueValues.ResetValues();
		}
		else {
			// Something selected
			DialogueEntry de = (DialogueEntry)dialogueLibrary.GetEntryBySelectedIndex(selDialogue);
			dialogueValues.CopyValues(de);
		}
	}

	void InstansiateDialogue() {
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

		dialogueLibrary.AddEntry(de);
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
		DialogueEntry de = (DialogueEntry)dialogueLibrary.GetEntryBySelectedIndex(selDialogue);
		string path = "Assets/LibraryData/Dialogues/" + de.uuid + ".asset";

		dialogueLibrary.RemoveEntryBySelectedIndex(selDialogue);
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
