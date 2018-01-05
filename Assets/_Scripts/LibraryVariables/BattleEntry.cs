using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LibraryEntries/Battle")]
public class BattleEntry : ScrObjLibraryEntry {

	public enum RemoveSide {NONE,LEFT,RIGHT};
	public enum NextLocation {OVERWORLD,DIALOGUE,BATTLE};


	// General battle things
	public bool escapeButtonEnabled = true;
	
	// Tutorial stuff
	public bool isTutorial = false;
	public Sprite backgroundHintLeft = null;
	public Sprite backgroundHintRight = null;
	public RemoveSide removeSide = RemoveSide.NONE;
	public bool playerInvincible = false;

	// Background
	public Sprite backgroundLeft = null;
	public Sprite backgroundRight = null;

	// Enemies 
	public bool randomizeEnemies = false;
	public int numberOfEnemies = 1;
	public List<EnemyEntry> enemyTypes = new List<EnemyEntry>();

	// Player stuff
	public bool useSpecificKanji = false;
	public Kanji[] equippedKanji = new Kanji[Constants.MAX_EQUIPPED_KANJI];

	// After match values
	public NextLocation nextLocation = NextLocation.OVERWORLD;
	public bool changePosition = false;
	public Constants.OverworldArea playerArea = Constants.OverworldArea.DEFAULT;
	public Vector2 playerPosition = new Vector2();
	public DialogueEntry nextDialogue = null;
	public BattleEntry nextBattle = null;


	/// <summary>
	/// Resets all values to start values.
	/// </summary>
	/// <param name="other"></param>
	public override void ResetValues() {
		base.ResetValues();

		// General battle things
		escapeButtonEnabled = true;

		// Tutorial stuff
		isTutorial = false;
		backgroundHintLeft = null;
		backgroundHintRight = null;
		removeSide = RemoveSide.NONE;
		playerInvincible = false;

		// Background
		backgroundLeft = null;
		backgroundRight = null;

		// Enemies 
		randomizeEnemies = false;
		numberOfEnemies = 1;
		enemyTypes = new List<EnemyEntry>();

		// Player stuff
		useSpecificKanji = true;
		equippedKanji = new Kanji[Constants.MAX_EQUIPPED_KANJI];

		// After match values
		nextLocation = NextLocation.OVERWORLD;
		changePosition = false;
		playerArea = Constants.OverworldArea.DEFAULT;
		playerPosition = new Vector2();
		nextDialogue = null;
		nextBattle = null;
	}

	/// <summary>
	/// Copies all the values from another BattleEntry.
	/// </summary>
	/// <param name="battle"></param>
	public override void CopyValues(ScrObjLibraryEntry other) {
		base.CopyValues(other);
		BattleEntry be = (BattleEntry) other;

		// General battle things
		escapeButtonEnabled = be.escapeButtonEnabled;

		// Tutorial stuff
		isTutorial = be.isTutorial;
		backgroundHintLeft = be.backgroundHintLeft;
		backgroundHintRight = be.backgroundHintRight;
		removeSide =  be.removeSide;
		playerInvincible = be.playerInvincible;

		// Background
		backgroundLeft = be.backgroundLeft;
		backgroundRight = be.backgroundRight;

		// Enemies 
		randomizeEnemies = be.randomizeEnemies;
		numberOfEnemies = be.numberOfEnemies;
		enemyTypes = new List<EnemyEntry>();
		for (int i = 0; i < be.enemyTypes.Count; i++) {
			enemyTypes.Add(be.enemyTypes[i]);
		}

		// Player stuff
		useSpecificKanji = be.useSpecificKanji;
		equippedKanji = new Kanji[Constants.MAX_EQUIPPED_KANJI];
		for (int i = 0; i < be.equippedKanji.Length; i++) {
			equippedKanji[i] = be.equippedKanji[i];
		}
		
		// After match values
		nextLocation = be.nextLocation;
		changePosition = be.changePosition;
		playerArea = be.playerArea;
		playerPosition = be.playerPosition;
		nextDialogue = be.nextDialogue;
		nextBattle = be.nextBattle;
	}
}
