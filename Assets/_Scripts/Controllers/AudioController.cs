using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Audio controller class which plays the background music and sfx.
/// </summary>
public class AudioController : MonoBehaviour {
	
	public AudioVariable backgroundMusic;
	public AudioVariable sfxClip;
	public FloatVariable musicVolume;
	public AudioSource efxSource;
	public AudioSource musicSource;

	[MinMaxRange(0.75f,1.5f)]
	public RangedFloat pitchRange = new RangedFloat(0.95f,1.05f);
	
	private bool playingBkg = false;

	public void OnEnable() {
		musicSource.volume = Mathf.Clamp01(musicVolume.value);
	}

	/// <summary>
	/// Playes the background music or stops the music if clip is null.
	/// </summary>
	/// <param name="clip">Clip.</param>
	public void PlayBackgroundMusic() {
		Debug.Log("MUSIC!!!");
		if (backgroundMusic.value == null) {
			musicSource.Stop();
			playingBkg = false;
		}
		else {
			musicSource.clip = backgroundMusic.value;
			musicSource.Play();
			playingBkg = true;
		}
	}

	/// <summary>
	/// Playes the background music or stops the music if clip is null.
	/// </summary>
	/// <param name="clip">Clip.</param>
	public void PauseBackgroundMusic() {
		if (musicSource.clip == null)
			return;
		if( playingBkg) {
			musicSource.Pause();
			playingBkg = false;
		}
		else {
			musicSource.Play();
			playingBkg = true;
		}
	}

	/// <summary>
	/// Plays a single audio clip.
	/// </summary>
	/// <param name="clip">Clip.</param>
	private void PlaySingle(AudioClip clip) {
		efxSource.clip = clip;
		efxSource.Play();
	}

	/// <summary>
	/// Plays the next sfx clip.
	/// </summary>
	/// <param name="clip">Clip.</param>
	public void PlaySfx() {
		if (sfxClip.value != null) {
			efxSource.clip = sfxClip.value;
			efxSource.Play();
		}
	}

	/// <summary>
	/// Plays a random sfx from the list with a random pitch
	/// </summary>
	/// <param name="clips">Clips.</param>
	public void RandomizeSfx(AudioClip[] clips) {
		int randomIndex = Random.Range(0,clips.Length);
		float randomPitch = Random.Range(pitchRange.minValue,pitchRange.maxValue);

		efxSource.pitch = randomPitch;
		efxSource.clip = clips[randomIndex];

		efxSource.Play();
	}
}
