using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LibraryEntries/Dialogue")]
public class DialogueEntry : ScrObjLibraryEntry {

	public List<Frame> frames = new List<Frame>();
	public BattleEntry.NextLocation nextLocation = BattleEntry.NextLocation.DIALOGUE;
	public ScrObjLibraryEntry nextEntry = null;
	public int size { get{ return frames.Count; } }
	public List<Color> participantColors = new List<Color>();


	public override void ResetValues() {
		base.ResetValues();

		frames = new List<Frame>();
		frames.Add(new Frame());
		nextLocation = BattleEntry.NextLocation.DIALOGUE;
		nextEntry = null;
		participantColors = new List<Color>();
	}

	public override void CopyValues(ScrObjLibraryEntry other) {
		base.CopyValues(other);
		DialogueEntry de = (DialogueEntry)other;

		frames = de.frames;
		nextLocation = de.nextLocation;
		nextEntry = de.nextEntry;
		participantColors = de.participantColors;
	}

	public GUIContent[] GenerateFrameRepresentation() {
		GUIContent[] list = new GUIContent[frames.Count];
		GUIContent content;
		for (int j = 0; j < frames.Count; j++) {
			content = new GUIContent();
			content.text = uuid;
			list[j] = content;
		}
		return list;
	}
}
