using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class BattleController : MonoBehaviour {

	public ScrObjLibraryVariable battleLibrary;
	public StringVariable battleUuid;
	public IntVariable currentArea;
	private BattleEntry be;
	private BackgroundChanger backchange;

	private bool initiated = false;
	public BoolVariable paused;
	public BoolVariable invincible;
	public BoolVariable removeLeftSide;
	public BoolVariable alwaysEscapable;
	public BoolVariable useSlowTime;
	public UnityEvent pauseEvent;
	public UnityEvent saveGameEvent;

	public Text winText;
	public EnemyController enemyController;
	public PlayerController playerController;
	public SpiritGridController spiritController;
	public CharacterHealthGUI characterHealth;
	public AudioClip pauseClip;

	public float startupTime = 3.0f;
	public int state = 0;
	public bool tutorial = false;
	public bool escape = false;
	public float currentTime = 0f;


	// Use this for initialization
	void Awake () {
		be = (BattleEntry)battleLibrary.GetEntry(battleUuid.value);

		tutorial = (be.backgroundHintLeft != null || be.backgroundHintRight != null);
		escape = tutorial;
		invincible.value = true;
		removeLeftSide.value = (be.removeSide == BattleEntry.RemoveSide.LEFT);
		useSlowTime.value = be.useSlowTime;

		backchange = GameObject.Find("Background Background").GetComponent<BackgroundChanger>();
		backchange.escapeBattleButton.interactable = be.escapeButtonEnabled;
#if UNITY_EDITOR
		backchange.escapeBattleButton.interactable = be.escapeButtonEnabled || alwaysEscapable.value;
#endif
		backchange.cameraNormal.enabled = false;
		backchange.cameraSpirit.enabled = false;
		backchange.gridController.enabled = false;

		SetupBackgrounds();
		paused.value = true;
		StartCoroutine(CreateEnemies());
		Debug.Log("Start");
	}

	IEnumerator CreateEnemies(){
		while (!enemyController.initiated) {
			Debug.Log("Waiting");
			yield return null;
		}
		enemyController.CreateEnemies(be.removeSide != BattleEntry.RemoveSide.RIGHT, be.removeSide != BattleEntry.RemoveSide.LEFT);
		initiated = true;
		Debug.Log("Initiated");
	}
	
	// Update is called once per frame
	void Update () {

		if (!initiated)
			return;

		if (state != -1)
			currentTime += Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Debug.Log("Press escape");
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
		if (tutorial && be.isTutorial) {
			if (be.backgroundHintRight != null) {
				backchange.tutorialNormal.sprite = be.backgroundHintRight;
				tutorial = false;
			}
			if (be.backgroundHintLeft != null) {
				backchange.tutorialSpirit.sprite = be.backgroundHintLeft;
				tutorial = false;
			}
			state = -1;
			Debug.Log("Tutorial screens activated");
			return;
		}

		Debug.Log("Other stuff setupBackgrounds");
		invincible.value = be.playerInvincible;
		
		if (be.removeSide == BattleEntry.RemoveSide.RIGHT) {
			backchange.tutorialNormal.sprite = be.backgroundRight;
		}
		else {
			backchange.tutorialNormal.enabled = false;
			backchange.cameraNormal.enabled = true;
			backchange.transformNormal.sprite = be.backgroundRight;
		}
		if (be.removeSide == BattleEntry.RemoveSide.LEFT)
			backchange.tutorialSpirit.sprite = be.backgroundLeft;
		else {
			backchange.tutorialSpirit.enabled = false;
			backchange.cameraSpirit.enabled = true;
			backchange.transformSpirit.sprite = be.backgroundLeft;
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

		if (be.playerArea == Constants.OverworldArea.TOWER)
			saveGameEvent.Invoke();

		Debug.Log("Won");

		yield return new WaitForSeconds(time);
		currentArea.value = (int)Constants.SCENE_INDEXES.SCORE;
		SceneManager.LoadScene(currentArea.value);
	}

	public IEnumerator EscapedBattle(float time){
		ScoreScreenValues values = GetComponent<ScoreScreenValues>();
		values.wonBattleState.value = "escape";
		values.time.value = currentTime;
		values.noEnemies.value = enemyController.numberOfEnemies;

		Debug.Log("Escaped battle");

		winText.text = "ESCAPED!";
		yield return new WaitForSeconds(time);
		currentArea.value = (int)Constants.SCENE_INDEXES.SCORE;
		SceneManager.LoadScene(currentArea.value);
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
		currentArea.value = (int)Constants.SCENE_INDEXES.SCORE;
		SceneManager.LoadScene(currentArea.value);
		yield return 0;
	}


	public static IEnumerator EndGame(float time){
		yield return new WaitForSeconds(time);
		AppHelper.Quit();
	}
}
