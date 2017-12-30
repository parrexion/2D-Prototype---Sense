using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour {

	[Header("Values - left side")]
	public IntVariable ingameDay;
	public IntVariable playerLevel;
	public IntVariable totalExp;
	public IntVariable money;
	public StringVariable playTime;

	[Header("Values - right side")]
	public IntVariable maxHealth;
	public IntVariable attack;
	public IntVariable defense;
	public IntVariable sAttack;
	public IntVariable sDefense;

	[Header("References - left side")]
	public Text dayText;
	public Text levelText;
	public Text expText;
	public Text moneyText;
	public Text playtimeText;

	[Header("References - right side")]
	public Text healthText;
	public Text attackText;
	public Text defenseText;
	public Text sAttackText;
	public Text sDefenseText;

	// Use this for initialization
	void Start () {
		SetAllStats();
	}
	
	// Update is called once per frame
	void Update () {
		
		//Update time
		playtimeText.text = playTime.value.ToString();
	}

	void SetAllStats() {
		dayText.text = ingameDay.value.ToString();
		levelText.text = playerLevel.value.ToString();
		expText.text = totalExp.value.ToString();
		moneyText.text = money.value.ToString();
		playtimeText.text = playTime.value;

		healthText.text = maxHealth.value.ToString();
		attackText.text = attack.value.ToString();
		defenseText.text = defense.value.ToString();
		sAttackText.text = sAttack.value.ToString();
		sDefenseText.text = sDefense.value.ToString();
	}
}
