using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour {

	public BattleValues bv;
	private BackgroundChanger backchange;
	private StoryValues storyValues;

	private bool initiated = false;
	public bool pause = false;

	public GameObject battleMenu;
	public Text winText;
	public EnemyController enemyController;
	public PlayerController playerController;
	public SpiritGridController spiritController;
	public CharacterHealthGUI characterHealth;
	public PlayerStats playerStats;

	public Transform saveValues;

	public float startupTime = 3.0f;
	public int state = 0;
	public bool escape = false;
	public float currentTime = 0f;


	// Use this for initialization
	void Start () {
		storyValues = MainControllerScript.instance.storyValues;
		playerStats = PlayerStats.instance;
		bv = storyValues.GetBattleValues();

		escape = bv.escapeTextReq;
		characterHealth.SetActive(false);
		characterHealth.SetInvulnerable(true);

		backchange = GameObject.Find("Background Background").GetComponent<BackgroundChanger>();
		backchange.escapeBattleButton.interactable = bv.escapeButtonEnabled;
		backchange.cameraNormal.enabled = false;
		backchange.cameraSpirit.enabled = false;
		backchange.weapons.SetVisible(false);
		backchange.gridController.enabled = false;
		backchange.spiritGrid.active = false;

		SetupBackgrounds();
		SetActiveGame(false);
		StartCoroutine("CreateEnemies");
	}

	IEnumerator CreateEnemies(){
		while (!enemyController.initiated) {
			Debug.Log("Waiting");
			yield return null;
		}
		enemyController.CreateEnemies(bv.removeSide != BattleValues.RemoveSide.RIGHT,
			bv.removeSide != BattleValues.RemoveSide.LEFT);
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

		}
		else {
			if (bv.healthEnabled) {
				characterHealth.SetActive(true);
				characterHealth.SetInvulnerable(false);
			}
			
			if (bv.removeSide == BattleValues.RemoveSide.RIGHT) {
				backchange.tutorialNormal.sprite = backchange.tutorialBackgrounds[bv.backgroundRight];
			}
			else {
				backchange.tutorialNormal.enabled = false;
				backchange.cameraNormal.enabled = true;
				backchange.transformNormal.sprite = backchange.normalBackgrounds[bv.backgroundRight];
				backchange.weapons.SetVisible(true);
			}
			if (bv.removeSide == BattleValues.RemoveSide.LEFT)
				backchange.tutorialSpirit.sprite = backchange.tutorialBackgrounds[bv.backgroundLeft];
			else {
				backchange.tutorialSpirit.enabled = false;
				backchange.cameraSpirit.enabled = true;
				backchange.transformSpirit.sprite = backchange.spiritBackgrounds[bv.backgroundLeft];
				backchange.gridController.enabled = true;
				backchange.spiritGrid.active = true;
			}
			winText.text = "GET READY!";
			state = 0;
		}
	}

	private void StartBattle() {
		SetupBackgrounds();
	}

	private void AlmostStart() {
		winText.text = "FIGHT!";
	}
		
	public void PauseGame() {
		if (!pause) {
			pause = true;
			battleMenu.SetActive(true);
			SetActiveGame(false);
		}
		else {
			pause = false;
			battleMenu.SetActive(false);
			SetActiveGame(true);
		}
	}

	private void BattleStart() {
		winText.text = "";
		SetActiveGame(true);
	}

	public void EndBattle(){
		SetActiveGame(false);
		spiritController.grid.CancelGrid();
	}

	private void SetActiveGame(bool state){
		if (bv.removeSide != BattleValues.RemoveSide.RIGHT)
			playerController.SetActive(state);
		if (bv.removeSide != BattleValues.RemoveSide.LEFT)
			spiritController.SetActive(state);
		enemyController.SetAllAIActive(state);
		if (bv.healthEnabled) {
			characterHealth.SetActive(state);
			characterHealth.SetInvulnerable(!state);
		}
		MainControllerScript.instance.battleGUI.SetActive(state);
	}

	public void EscapeBattleButton(float time){
		StartCoroutine(EscapedBattle(time));
	}

	public IEnumerator WonBattle(float time){
		Transform t = Instantiate(saveValues);
		t.name = "SaveValues";
		DontDestroyOnLoad(t);

		ScoreScreenValues values = t.GetComponent<ScoreScreenValues>();
		values.wonBattle = true;
		values.lostBattle = false;
		values.time = currentTime;
		if (bv.healthEnabled) {
			values.currentHealth = playerStats.currentHealth;
			values.maxHealth = playerStats.maxHealth.GetValue();
		}
		values.noEnemies = enemyController.numberOfEnemies;
		values.enemiesDefeated = enemyController.GetEnemiesDefeated();
		values.exp = enemyController.GetTotalExp();
		values.money = enemyController.GetTotalMoney();
		values.treasures = enemyController.GetTreasures();

		if (storyValues.battleType == StoryValues.BattleType.SPECIFIC)
			SaveController.instance.Save();

		Debug.Log("Won");

		yield return new WaitForSeconds(time);
		SceneManager.LoadScene(BattleConstants.SCENE_SCORE);
	}

	public IEnumerator EscapedBattle(float time){
		Transform t = Instantiate(saveValues);
		t.name = "SaveValues";
		DontDestroyOnLoad(t);

		ScoreScreenValues values = t.GetComponent<ScoreScreenValues>();
		values.wonBattle = false;
		values.lostBattle = false;
		values.time = currentTime;
		if (bv.healthEnabled) {
			values.currentHealth = playerStats.currentHealth;
			values.maxHealth = playerStats.maxHealth.GetValue();
		}
		else
			values.maxHealth = 0;
		values.noEnemies = enemyController.numberOfEnemies;

		Debug.Log("Escaped battle");

		winText.text = "ESCAPED!";
		yield return new WaitForSeconds(time);
		SceneManager.LoadScene(BattleConstants.SCENE_SCORE);
		yield return 0;
	}

	public void GameOverTrigger() {
		StartCoroutine(LostBattle(3f));
		SetActiveGame(false);
	}

	public IEnumerator LostBattle(float time) {
		Transform t = Instantiate(saveValues);
		t.name = "SaveValues";
		DontDestroyOnLoad(t);

		ScoreScreenValues values = t.GetComponent<ScoreScreenValues>();
		values.wonBattle = false;
		values.lostBattle = true;
		values.time = currentTime;

		yield return new WaitForSeconds(time);
		SceneManager.LoadScene(BattleConstants.SCENE_SCORE);
		yield return 0;
	}


	public static IEnumerator EndGame(float time){
		yield return new WaitForSeconds(time);
		AppHelper.Quit();
	}
}
