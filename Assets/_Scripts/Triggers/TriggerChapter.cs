using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChapter : MonoBehaviour {

	List<string> chapterIDs = new List<string>();
	List<Transform> containers = new List<Transform>();


	// Use this for initialization
	void Awake () {
		SetupTriggers();
	}

	public void SetupTriggers() {
		Transform child;
		for (int i = 0; i < transform.childCount; i++) {
			child = transform.GetChild(i);
			chapterIDs.Add(child.name);
			containers.Add(child);
		}
		if (chapterIDs.Count > 0 && chapterIDs[0] != "ChangeMap")
			Debug.LogError("This section does not contain a ChangeMap object at the top!");
	}

	/// <summary>
	/// Activates the chapter with the given id and deactivates the rest.
	/// </summary>
	/// <param name="id"></param>
	public void ActivateSection(string id, bool state) {

		if (containers.Count == 0){
			Debug.LogWarning("Empty area");
			return;
		}
		containers[0].gameObject.SetActive(state);
		for (int i = 1; i < containers.Count; i++) {
			containers[i].gameObject.SetActive(state && id == chapterIDs[i]);
		}
	}
}
