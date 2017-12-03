using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LibraryEntries/Battle")]
public class BattleEntry : ScrObjLibraryEntry {

	public enum RemoveSide {NONE,LEFT,RIGHT};

	
	// Tutorial stuff
	public bool isTutorial = false;
	public bool escapeTextReq = false;
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
	public Kanji[] equippedKanji = new Kanji[BattleConstants.MAX_EQUIPPED_KANJI];

	// Other
	public bool escapeButtonEnabled = true;


	/// <summary>
	/// Resets all values to start values.
	/// </summary>
	/// <param name="other"></param>
	public override void ResetValues() {
		base.ResetValues();

		// Tutorial stuff
		isTutorial = false;
		escapeTextReq = false;
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
		equippedKanji = new Kanji[BattleConstants.MAX_EQUIPPED_KANJI];

		// Other
		escapeButtonEnabled = true;
	}

	/// <summary>
	/// Copies all the values from another BattleEntry.
	/// </summary>
	/// <param name="battle"></param>
	public override void CopyValues(ScrObjLibraryEntry other) {
		base.CopyValues(other);
		BattleEntry be = (BattleEntry) other;

		// Tutorial stuff
		isTutorial = be.isTutorial;
		escapeTextReq = be.escapeTextReq;
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
		equippedKanji = new Kanji[BattleConstants.MAX_EQUIPPED_KANJI];
		for (int i = 0; i < be.equippedKanji.Length; i++) {
			equippedKanji[i] = be.equippedKanji[i];
		}

		// Other
		escapeButtonEnabled = be.escapeButtonEnabled;
	}
}
