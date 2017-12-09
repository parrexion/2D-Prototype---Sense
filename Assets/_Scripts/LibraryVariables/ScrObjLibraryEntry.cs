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

	public virtual GUIContent GenerateRepresentation() {
		GUIContent content = new GUIContent();
		content.text = uuid;
		Texture2D tex;
		if (repColor.a != 0){
			tex = GenerateColorTexture(repColor);
		}
		else {
			tex = GenerateRandomColor();
		}
		content.image = tex;
		return content;
	}

	Texture2D GenerateRandomColor() {
		Color c = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
		return GenerateColorTexture(c);
	}

	public Texture2D GenerateColorTexture(Color c) {
		int size = 32;
		Texture2D tex = new Texture2D(size,size);
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				tex.SetPixel(i,j,c);
			}
		}
		tex.Apply();
		return tex;
	}

	public virtual bool IsEqual(ScrObjLibraryEntry other) {
		return (uuid == other.uuid);
	}
}
