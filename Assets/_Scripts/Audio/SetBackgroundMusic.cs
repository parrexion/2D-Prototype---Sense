using UnityEngine;
using UnityEngine.Events;

public class SetBackgroundMusic : MonoBehaviour {

	public AudioVariable backgroundMusic;
	public AudioClip audioClip;
	public UnityEvent backgroundMusicChanged;

	void OnEnable() {
		if (audioClip == null)
			Debug.Log("No audio clip defined! Intended?");

		backgroundMusic.value = audioClip;
		backgroundMusicChanged.Invoke();
	}
}
