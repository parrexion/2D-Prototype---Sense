using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Container class for a list of AudioClips.
/// </summary>
public class AudioList : MonoBehaviour {

	public AudioClip[] audioClips;


	public AudioClip RandomClip() {
		return audioClips[Random.Range(0,audioClips.Length)];
	}
}
