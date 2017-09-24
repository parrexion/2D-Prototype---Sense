using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveController : MonoBehaviour {

	public int bestLevel = 0;
	private string filePath = "";


	#region Singleton
	public static SaveController instance;

	void Awake() {
		filePath = Application.persistentDataPath+"/playerInfo.dat";
		if (instance != null) {
			Destroy(gameObject);
		}
		else {
			instance = this;
		}
	}
	#endregion

	public void Save() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open(filePath,FileMode.OpenOrCreate);

		int level = MainControllerScript.instance.storyValues.towerLevel;
		bestLevel = Mathf.Max(bestLevel,level);

		bf.Serialize(file,bestLevel);
		file.Close();
		Debug.Log("Successfully saved the file!");
	}

	public void Load() {
		if (File.Exists(filePath)){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(filePath,FileMode.Open);
			int data = (int)bf.Deserialize(file);
			file.Close();

			bestLevel = data;
			Debug.Log("Successfully loaded the file!");
		}
		else {
			Debug.LogWarning("Could not open the file: "+filePath);
		}
	}
}
