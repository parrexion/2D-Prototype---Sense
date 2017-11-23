using UnityEngine;
using UnityEditor;

public class DialogueWindow : EditorWindow {

	public IntVariable backGroundIndex;

	public IntVariable character0;
	public IntVariable character1;
	public IntVariable character2;
	public IntVariable character3;
	public IntVariable character4;

	public IntVariable pose0;
	public IntVariable pose1;
	public IntVariable pose2;
	public IntVariable pose3;
	public IntVariable pose4;

	public StringVariable talkingName;
	public IntVariable talkingIndex;
	public IntVariable talkingPose;

	public SpriteListVariable charSprites;


	IntVariable[] characters;
	IntVariable[] poses;

	Texture2D headerBackground;
	Texture2D backgroundChars;
	Texture2D backgroundChars2;
	Texture2D dialogueBackground;

	Color headerColor = new Color(0.25f,0.5f,0.75f);
	Color charactersColor = new Color(0.35f, 0.75f, 0.35f);
	Color charactersColor2 = new Color(0.15f, 0.55f, 0.15f);
	Color dialogueColor = new Color(0.5f, 0.5f, 0.5f);

	Rect headerRect = new Rect();
	Rect[] rectChar;
	Rect dialogueRect = new Rect();


	[MenuItem("Window/DialogueEditor")]
	public static void ShowWindow() {
		GetWindow<DialogueWindow>("Dialogue Editor");
	}

	void OnEnable() {
		rectChar = new Rect[5];
		characters = new IntVariable[] { character0, character1, character2, character3, character4 };
		poses = new IntVariable[] { pose0, pose1, pose2, pose3, pose4 };
		InitTextures();
	}

	void OnGUI() {

		GUILayout.Label("Character selector", EditorStyles.boldLabel);
		DrawBackgrounds();

		HorizontalStuff();
	}

	void InitTextures() {
		headerBackground = new Texture2D(1, 1);
		headerBackground.SetPixel(0, 0, headerColor);
		headerBackground.Apply();

		backgroundChars = new Texture2D(1, 1);
		backgroundChars.SetPixel(0, 0, charactersColor);
		backgroundChars.Apply();

		backgroundChars2 = new Texture2D(1, 1);
		backgroundChars2.SetPixel(0, 0, charactersColor2);
		backgroundChars2.Apply();

		dialogueBackground = new Texture2D(1, 1);
		dialogueBackground.SetPixel(0, 0, dialogueColor);
		dialogueBackground.Apply();
	}

	void DrawBackgrounds() {
		float screenStep = Screen.width / 5f;
		headerRect.x = 0;
		headerRect.y = 0;
		headerRect.width = Screen.width;
		headerRect.height = 50;

		rectChar[0].x = 0;
		rectChar[0].y = 50;
		rectChar[0].width = screenStep;
		rectChar[0].height = Screen.height - (50 + 200);

		rectChar[1].x = screenStep;
		rectChar[1].y = 50;
		rectChar[1].width = screenStep;
		rectChar[1].height = Screen.height - (50 + 200);

		rectChar[2].x = screenStep *2;
		rectChar[2].y = 50;
		rectChar[2].width = screenStep;
		rectChar[2].height = Screen.height - (50 + 200);

		rectChar[3].x = screenStep *3;
		rectChar[3].y = 50;
		rectChar[3].width = screenStep;
		rectChar[3].height = Screen.height - (50 + 200);

		rectChar[4].x = screenStep *4;
		rectChar[4].y = 50;
		rectChar[4].width = screenStep;
		rectChar[4].height = Screen.height - (50 + 200);

		dialogueRect.x = 0;
		dialogueRect.y = Screen.height - (200);
		dialogueRect.width = Screen.width;
		dialogueRect.height = 200;

		GUI.DrawTexture(headerRect, headerBackground);
		GUI.DrawTexture(rectChar[0], backgroundChars);
		GUI.DrawTexture(rectChar[1], backgroundChars2);
		GUI.DrawTexture(rectChar[2], backgroundChars);
		GUI.DrawTexture(rectChar[3], backgroundChars2);
		GUI.DrawTexture(rectChar[4], backgroundChars);
		GUI.DrawTexture(dialogueRect, dialogueBackground);
	}

	void HorizontalStuff() {
		for (int i = 0; i < 5; i++) {
			GUILayout.BeginArea(rectChar[i]);

			GUILayout.Label("Character " + i, EditorStyles.boldLabel);
			GUILayout.Label("Name ");
			characters[i].value = EditorGUILayout.IntField("Character", characters[i].value);
			characters[i].value = EditorGUILayout.IntField("Pose", characters[i].value);
			GUILayout.Label(charSprites.values[characters[i].value].texture);

			GUILayout.EndArea();
		}
	}
}
