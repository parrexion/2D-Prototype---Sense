using UnityEngine;

/// <summary>
/// Base class for all the different types of entries which are 
/// added to libraries for later access.
/// </summary>
public class ScrObjLibraryEntry : ScriptableObject {

	public string uuid;
	public string entryName;
	public Color repColor;


	/// <summary>
	/// Resets the values to default.
	/// </summary>
	public virtual void ResetValues() {
		uuid = "";
		entryName = "";
		repColor = new Color();
	}

	/// <summary>
	/// Copies the values from another entry.
	/// </summary>
	/// <param name="other"></param>
	public virtual void CopyValues(ScrObjLibraryEntry other) {
		uuid = other.uuid;
		entryName = other.entryName;
		repColor = other.repColor;
	}
}
