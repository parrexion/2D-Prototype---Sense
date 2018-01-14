using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterTrigger : OWTrigger {

	public string chapterString;
	public StringVariable currentChapter;

	/// <summary>
	/// Sets up all blocks and hides them from sight.
	/// </summary>
	protected override void Startup(){
		if (!active)
			return;
			
		currentChapter.value = chapterString;
		Debug.Log("Chapter is now: " + currentChapter.value);
		Deactivate();
	}


    public override void Trigger() {}
}
