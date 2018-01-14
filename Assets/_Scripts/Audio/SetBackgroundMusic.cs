using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Class which sets the background music to one of the songs in the list.
/// </summary>
public class SetBackgroundMusic : MonoBehaviour {

	public bool randomSong = true;
	public AudioVariable backgroundMusic;
	public AudioClip[] audioClip;
	public UnityEvent backgroundMusicChanged;

	void Start() {
		if (audioClip == null)
			Debug.Log("No audio clip defined! Intended?");

		int index = SelectedMusic();
		backgroundMusic.value = audioClip[index];
		backgroundMusicChanged.Invoke();
	}

	/// <summary>
	/// Selects the first song or a random one from the list.
	/// </summary>
	/// <returns></returns>
	int SelectedMusic(){
		if (!randomSong)
			return 0;
		
		return Random.Range(0,audioClip.Length);
	}
}
