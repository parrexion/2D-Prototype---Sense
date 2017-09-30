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

	public void ShowDialogue() {
		if (!initiated) {
			currentLines = parser.dialogues.lines[0];
			initiated = true;
		}

		ResetImages();
		ParseLine();
	}

	void ResetImages() {
		if (scene.characterName != "") {
//			GameObject character = GameObject.Find(scene.characterName);
//			SpriteRenderer currSprite = character.GetComponent<SpriteRenderer>();
//			currSprite = null;
		}
	}

	void ParseLine() {
		currentLines.NextDialogue(scene);

		currentDialogue = "";
		dialogue = scene.dialogue;
//		Debug.Log(dialogue);
		words = dialogue.Split(' ');
		StartCoroutine("TextUpdate");
		DisplayImages();
	}

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

	private bool FitNextWord(string nextWord){
		string word = nextWord.Split('\n')[0];
		TextGenerationSettings settings = dialogueBox.GetGenerationSettings(dialogueBox.rectTransform.rect.size);
		float originalHeight = dialogueBox.cachedTextGeneratorForLayout.GetPreferredHeight(currentDialogue,settings);
		float newHeight = dialogueBox.cachedTextGeneratorForLayout.GetPreferredHeight(currentDialogue+word,settings);

		return newHeight > originalHeight;
	}

	void DisplayImages() {

		backgroundImage.sprite = backgrounds[scene.background];

		for(int i = 0; i < scene.characters.Length; i++){
			if (scene.positions[i] == -1)
				scene.characters[i].sprite.sprite = null;
			else 
				scene.characters[i].sprite.sprite = characters[scene.positions[i]].characterPoses[scene.currentPoses[i]];
		}

		if (scene.talkingCharacter == -1 || scene.talkingCharacter == 4)
			scene.closeup.sprite.sprite = null;
		else 
			scene.closeup.sprite.sprite = characters[scene.talkingCharacter].characterPoses[scene.talkingPose];
	}


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

	void UpdateUI() {
//		if (!playerTalking) {
//			ClearButtons();
//		}
		nameBox.text = scene.characterName;
		dialogueBox.text = currentDialogue;
//		dialogueBox.text = dialogue;
	}

	void ClearButtons() {
//		for (int i = 0; i < buttons.Count; i++) {
//			Button b = buttons[i];
//			buttons.Remove(b);
//			Destroy(b.gameObject);
//		}
	}
}
