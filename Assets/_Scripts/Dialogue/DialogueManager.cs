using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DialogueScene))]
public class DialogueManager : MonoBehaviour {

	DialogueParser parser;
	DialogueScene scene;
	public DialogueLines currentLines;

	public Sprite[] backgrounds;
	public Character[] characters;

	public string currentDialogue = "";
	public string dialogue = "";
	public string[] words = new string[0];
	private bool textUpdating = false;
	private bool finishNow = false;

	public Image backgroundImage;
	public Text dialogueBox;
	public Text nameBox;
	public GameObject choiceBox;

	private bool initiated = false;


	// Use this for initialization
	void Start () {
		scene = GetComponent<DialogueScene>();
		parser = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0)) {

			if (textUpdating) {
				finishNow = true;
			}
			else {
				ShowDialogue();
			}
		}

		UpdateUI();
	}

	/// <summary>
	/// Shows the current line of dialogue.
	/// </summary>
	public void ShowDialogue() {
		if (!initiated) {
			currentLines = parser.dialogues.lines[0];
			initiated = true;
		}

		ResetCharacters();
		ParseLine();
	}

	/// <summary>
	/// Removes all the characters from the current scene.
	/// </summary>
	void ResetCharacters() {
		for (int i = 0; i < scene.characters.Length; i++) {
			scene.characters[i].SetCharacterPose(-1,-1);
		}
	}

	/// <summary>
	/// Parses the current dialogue line and splits it into words and sets the characters.
	/// </summary>
	void ParseLine() {
		currentLines.NextDialogue(scene);

		currentDialogue = "";
		dialogue = scene.dialogue;
//		Debug.Log(dialogue);
		words = dialogue.Split(' ');
		StartCoroutine("TextUpdate");
		DisplayImages();
	}

	/// <summary>
	/// Updates the current dialogue line, character by character
	/// </summary>
	/// <returns></returns>
	IEnumerator TextUpdate() {
		float timeInSeconds = .02f;
		textUpdating = true;
		for (int j = 0; j < words.Length; j++) {
			if (FitNextWord(words[j]+" "))
				currentDialogue += "\n";
			for (int i = 0; i < words[j].Length; i++) {
				if (finishNow) {
					currentDialogue = dialogue;
					break;
				}
				currentDialogue += words[j][i];
				yield return new WaitForSeconds(timeInSeconds);
			}
			currentDialogue += ' ';
			yield return new WaitForSeconds(timeInSeconds);
		}
		textUpdating = false;
		finishNow = false;
	}

	/// <summary>
	/// Calculates whether the next word will need a newline. True if a newline is required.
	/// </summary>
	/// <param name="nextWord"></param>
	/// <returns></returns>
	private bool FitNextWord(string nextWord){
		string word = nextWord.Split('\n')[0];
		TextGenerationSettings settings = dialogueBox.GetGenerationSettings(dialogueBox.rectTransform.rect.size);
		float originalHeight = dialogueBox.cachedTextGeneratorForLayout.GetPreferredHeight(currentDialogue,settings);
		float newHeight = dialogueBox.cachedTextGeneratorForLayout.GetPreferredHeight(currentDialogue+word,settings);

		return newHeight > originalHeight;
	}

	/// <summary>
	/// Sets the characters and their poses.
	/// </summary>
	void DisplayImages() {

		backgroundImage.sprite = backgrounds[scene.background];

		for(int i = 0; i < scene.characters.Length; i++){
			scene.characters[i].SetCharacterPose(scene.positions[i],scene.currentPoses[i]);
		}
		Debug.Log("Displayed: " + scene.talkingCharacter + ", " + scene.talkingPose);
		scene.closeup.SetCharacterPose(scene.talkingCharacter, scene.talkingPose);

	}


	/// <summary>
	/// Creates choice buttons in the dialogue
	/// </summary>
	void CreateButtons() {
//		for(int i = 0 ; i < options.Length; i++) {
//			GameObject button = (GameObject)Instantiate(choiceBox);
//			Button b = button.GetComponent<Button>();
//			ChoiceButton cb = button.GetComponent<ChoiceButton>();
//			cb.SetText(options[i].Split(':')[0]);
//			cb.option = options[i].Split(':')[1];
//			cb.box = this;
//			b.transform.SetParent(this.transform);
//			b.transform.localPosition = new Vector3(0,-25+(i*50));
//			b.transform.localScale = new Vector3(1,1,1);
//			buttons.Add(b);
//		}
	}

	/// <summary>
	/// Sets the name of the talker and current dialogue text.
	/// </summary>
	void UpdateUI() {
//		if (!playerTalking) {
//			ClearButtons();
//		}
		nameBox.text = scene.characterName;
		dialogueBox.text = currentDialogue;
//		dialogueBox.text = dialogue;
	}

	/// <summary>
	/// Removes the dialogue buttons.
	/// </summary>
	void ClearButtons() {
//		for (int i = 0; i < buttons.Count; i++) {
//			Button b = buttons[i];
//			buttons.Remove(b);
//			Destroy(b.gameObject);
//		}
	}
}
