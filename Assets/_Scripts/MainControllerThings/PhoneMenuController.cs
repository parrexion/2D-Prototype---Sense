﻿using UnityEngine;
using UnityEngine.UI;

public class PhoneMenuController : MonoBehaviour {

	public Text clockText;
	public Text dateText;

	private System.DateTime currentDate;


	void Update() {
		currentDate = System.DateTime.Now;
		clockText.text = currentDate.Hour+":"+currentDate.Minute;
		switch (currentDate.DayOfWeek) {
		case System.DayOfWeek.Monday:
			dateText.text = "Mon";
			break;
		case System.DayOfWeek.Tuesday:
			dateText.text = "Tue";
			break;
		case System.DayOfWeek.Wednesday:
			dateText.text = "Wed";
			break;
		case System.DayOfWeek.Thursday:
			dateText.text = "Thu";
			break;
		case System.DayOfWeek.Friday:
			dateText.text = "Fri";
			break;
		case System.DayOfWeek.Saturday:
			dateText.text = "Sat";
			break;
		case System.DayOfWeek.Sunday:
			dateText.text = "Sun";
			break;
		default:
			break;
		}
	}
}