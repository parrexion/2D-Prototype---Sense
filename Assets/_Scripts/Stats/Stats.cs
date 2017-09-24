using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats {

	[SerializeField]
	private int baseValue;
	private List<int> addModifiers = new List<int>();
	private List<float> multiModifiers = new List<float>();
	private int hardcapMin = 0, hardcapMax = -1;

	public int GetValue(){
		int finalValue = baseValue;

		addModifiers.ForEach(x => finalValue += x);
		multiModifiers.ForEach(x => finalValue = (int)(finalValue * x));

		if (hardcapMax != -1)
			finalValue = Mathf.Min(hardcapMax,finalValue);
		finalValue = Mathf.Max(hardcapMin,finalValue);

		return finalValue;
	}

	public void DefineSettings(int[] values){
		if (values.Length < 2) {
			Debug.LogWarning("There are not enough values to define the settings!  Length: "+values.Length);
			return;
		}
		hardcapMin = values[0];
		hardcapMax = values[1];
	}

	public void AddAddModifier(int addMod){
		if (addMod != 0) {
			addModifiers.Add(addMod);
		}
	}

	public void RemoveAddModifier(int addMod) {
		if (addMod != 0)
			addModifiers.Remove(addMod);
	}


	public void AddMultiModifier(float multiMod){
		if (multiMod != 1) {
			multiModifiers.Add(multiMod);
		}
	}

	public void RemoveMultiModifier(float multiMod) {
		if (multiMod != 1)
			multiModifiers.Remove(multiMod);
	}
}
