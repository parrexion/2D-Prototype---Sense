using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveController : MonoBehaviour {

	class SaveClass {
		public int bestLevel = 0;
		public float musicVolume = 0;
		public float effectVolume = 0;
	}

	public IntVariable bestTowerLevel;
	public IntVariable currentTowerLevel;
	public FloatVariable musicVolume;
	public FloatVariable effectVolume;

	private string filePath = "";
	private SaveClass save;


#region Singleton
	public static SaveController instance;

	void Awake() {
		if (instance != null) {
			Destroy(gameObject);
		}
		else {
			instance = this;
			filePath = Application.persistentDataPath+"/playerInfo.dat";
			save = new SaveClass();
		}
	}
#endregion

	public void Save() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open(filePath,FileMode.OpenOrCreate);

		save.bestLevel = Mathf.Max(save.bestLevel,currentTowerLevel.value);
		save.musicVolume = musicVolume.value;
		save.effectVolume = effectVolume.value;

		bf.Serialize(file,save.bestLevel);
		file.Close();
		Debug.Log("Successfully saved the file!");
	}

	public void Load() {
		if (File.Exists(filePath)){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(filePath,FileMode.Open);
			save = (SaveClass)bf.Deserialize(file);
			file.Close();

			bestTowerLevel.value = save.bestLevel;
			musicVolume.value = save.musicVolume;
			effectVolume.value = save.effectVolume;
			Debug.Log("Successfully loaded the file!");
		}
		else {
			Debug.LogWarning("Could not open the file: "+filePath);
		}
	}
}
