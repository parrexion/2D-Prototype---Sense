using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class SaveController : MonoBehaviour {

	/// <summary>
	/// Class containing most of the save data.
	/// </summary>
	public class SaveClass {
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
			filePath = Application.persistentDataPath+"/saveData.xml";
			save = new SaveClass();
		}
	}
#endregion

	/// <summary>
	/// Updates the save class and saves it to file.
	/// </summary>
	public void Save() {
		XmlWriterSettings xmlWriterSettings = new XmlWriterSettings() { Indent = true };
		XmlSerializer serializer = new XmlSerializer(typeof(SaveClass));
		// StreamWriter file = new StreamWriter(filePath);

		save.bestLevel = Mathf.Max(save.bestLevel,currentTowerLevel.value);
		save.musicVolume = musicVolume.value;
		save.effectVolume = effectVolume.value;

		using (XmlWriter xmlWriter = XmlWriter.Create(filePath, xmlWriterSettings)) {
			serializer.Serialize(xmlWriter, save);
		}
		// file.Close();
		Debug.Log("Successfully saved the file!");
	}

	/// <summary>
	/// Reads the save file if it exists and sets the values.
	/// </summary>
	public void Load() {
		if (File.Exists(filePath)){
			XmlSerializer serializer = new XmlSerializer(typeof(SaveClass));
			FileStream file = File.Open(filePath,FileMode.Open);
			save = serializer.Deserialize(file) as SaveClass;
			file.Close();

			bestTowerLevel.value = save.bestLevel;
			musicVolume.value = save.musicVolume;
			effectVolume.value = save.effectVolume;
			Debug.Log("Successfully loaded the file!");
		}
		else {
			Debug.LogWarning("Could not open the file: " + filePath);
			Save();
		}
	}
}
