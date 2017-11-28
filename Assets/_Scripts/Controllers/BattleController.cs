using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[RequireComponent(typeof(BattleValues))]
public class BattleController : MonoBehaviour {

	public BattleValues bv;
	private BackgroundChanger backchange;
	private StoryValues storyValues;
	private AudioController audioController;

	private bool initiated = false;
	public BoolVariable paused;
	public BoolVariable invincible;
	public UnityEvent pauseEvent;

	public Text winText;
	public EnemyController enemyController;
	public PlayerController playerController;
	public SpiritGridController spiritController;
	public CharacterHealthGUI characterHealth;
	public AudioClip pauseClip;

	public float startupTime = 3.0f;
	public int state = 0;
	public bool escape = false;
	public float currentTime = 0f;


	// Use this for initialization
	void Start () {
		storyValues = MainControllerScript.instance.storyValues;
		audioController = AudioController.instance;
		bv = storyValues.GetBattleValues();

		escape = bv.escapeTextReq;
		invincible.value = true;

		backchange = GameObject.Find("Background Background").GetComponent<BackgroundChanger>();
		backchange.escapeBattleButton.interactable = bv.escapeButtonEnabled;
		backchange.cameraNormal.enabled = false;
		backchange.cameraSpirit.enabled = false;
		backchange.gridController.enabled = false;

		SetupBackgrounds();
		paused.value = true;
		StartCoroutine(CreateEnemies());
	}

	IEnumerator CreateEnemies(){
		while (!enemyController.initiated) {
			Debug.Log("Waiting");
			yield return null;
		}
		enemyController.CreateEnemies(bv.removeSide != BattleValues.RemoveSide.RIGHT, bv.removeSide != BattleValues.RemoveSide.LEFT);
		initiated = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (!initiated)
			return;

		if (state != -1)
			currentTime += Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (escape) {
				escape = false;
				StartBattle();
				Debug.Log("ESCAPED");
			}
			else if (state == 2) {
				PauseGame();
			}
		}

		if (state == 0 && currentTime >= startupTime-1.0f) {
			AlmostStart();
			state = 1;
		}
		else if (state == 1 && currentTime >= startupTime) {
			BattleStart();
			state = 2;
			currentTime = 0;
		}

		if (state == 2 && enemyController.CheckAllEnemiesDead()) {
			state = 3;
			winText.text = "YOU WIN!";
			EndBattle();
			StartCoroutine(WonBattle(3f));
		}
	}

	private void SetupBackgrounds() {
		if (bv.backgroundHintRight != -1 || bv.backgroundHintLeft != -1) {
			if (bv.backgroundHintRight != -1) {
				backchange.tutorialNormal.sprite = backchange.tutorialBackgrounds[bv.backgroundHintRight];
				bv.backgroundHintRight = -1;
			}
			if (bv.backgroundHintLeft != -1) {
				backchange.tutorialSpirit.sprite = backchange.tutorialBackgrounds[bv.backgroundHintLeft];
				bv.backgroundHintLeft = -1;
			}
			state = -1;
			return;
		}

		if (bv.healthEnabled) {
			invincible.value = false;
		}
		
		if (bv.removeSide == BattleValues.RemoveSide.RIGHT) {
			backchange.tutorialNormal.sprite = backchange.tutorialBackgrounds[bv.backgroundRight];
		}
		else {
			backchange.tutorialNormal.enabled = false;
			backchange.cameraNormal.enabled = true;
			backchange.transformNormal.sprite = backchange.normalBackgrounds[bv.backgroundRight];
		}
		if (bv.removeSide == BattleValues.RemoveSide.LEFT)
			backchange.tutorialSpirit.sprite = backchange.tutorialBackgrounds[bv.backgroundLeft];
		else {
			backchange.tutorialSpirit.enabled = false;
			backchange.cameraSpirit.enabled = true;
			backchange.transformSpirit.sprite = backchange.spiritBackgrounds[bv.backgroundLeft];
			backchange.gridController.enabled = true;
		}
		winText.text = "GET READY!";
		state = 0;
	}

	private void StartBattle() {
		SetupBackgrounds();
	}

	private void AlmostStart() {
		winText.text = "FIGHT!";
	}
		
	public void PauseGame() {
		paused.value = !paused.value;
		pauseEvent.Invoke();
		audioController.PauseBackgroundMusic();
	}

	private void BattleStart() {
		winText.text = "";
		paused.value = false;
	}

	public void EndBattle(){
		paused.value = true;
		spiritController.grid.CancelGrid();
	}

	public void EscapeBattleButton(float time){
		StartCoroutine(EscapedBattle(time));
	}

	public IEnumerator WonBattle(float time){
		ScoreScreenValues values = GetComponent<ScoreScreenValues>();
		values.wonBattleState.value = "win";
		values.time.value = currentTime;
		values.noEnemies.value = enemyController.numberOfEnemies;
		// values.enemiesDefeated = enemyController.GetEnemiesDefeated();
		values.exp.value = enemyController.GetTotalExp();
		values.money.value = enemyController.GetTotalMoney();
		// values.treasures = enemyController.GetTreasures();

		if (storyValues.battleType == StoryValues.BattleType.SPECIFIC || storyValues.battleType == StoryValues.BattleType.TOWER)
			SaveController.instance.Save();

		Debug.Log("Won");

		yield return new WaitForSeconds(time);
		SceneManager.LoadScene((int)BattleConstants.SCENE_INDEXES.SCORE);
	}

	public IEnumerator EscapedBattle(float time){
		ScoreScreenValues values = GetComponent<ScoreScreenValues>();
		values.wonBattleState.value = "escape";
		values.time.value = currentTime;
		values.noEnemies.value = enemyController.numberOfEnemies;

		Debug.Log("Escaped battle");

		winText.text = "ESCAPED!";
		yield return new WaitForSeconds(time);
		SceneManager.LoadScene((int)BattleConstants.SCENE_INDEXES.SCORE);
		yield return 0;
	}

	public void GameOverTrigger() {
		StartCoroutine(LostBattle(3f));
		paused.value = true;
	}

	public IEnumerator LostBattle(float time) {
		winText.text = "YOU DIED";

		ScoreScreenValues values = GetComponent<ScoreScreenValues>();
		values.wonBattleState.value = "lose";
		values.time.value = currentTime;

		yield return new WaitForSeconds(time);
		SceneManager.LoadScene((int)BattleConstants.SCENE_INDEXES.SCORE);
		yield return 0;
	}


	public static IEnumerator EndGame(float time){
		yield return new WaitForSeconds(time);
		AppHelper.Quit();
	}
}
