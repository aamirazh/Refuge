using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioSource efxSource;
	public AudioSource musicSource;
	public AudioClip introMusic;
	public AudioClip mainMusic;
	public AudioClip alternateMusic;
	public AudioClip deathMusic;
	public static SoundManager instance = null;

	public float lowPitchRange = 0.95f;
	public float highPitchRange = 1.05f;

	// Use this for initialization
	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
		playIntroMusic ();
	}

	public void playIntroMusic ()
	{
		musicSource.Stop ();
		musicSource.clip = introMusic;
		musicSource.Play ();
	}

	public void playMainMusic ()
	{
		musicSource.Stop ();
		musicSource.clip = mainMusic;
		musicSource.Play ();
	}
		
	public void playAlternateMusic ()
	{
		musicSource.Stop ();
		musicSource.clip = alternateMusic;
		musicSource.Play ();
	}

	public void playDeathMusic ()
	{
		musicSource.Stop ();
		musicSource.clip = deathMusic;
		musicSource.Play ();
	}

	public void PlaySingle (AudioClip clip)
	{
		musicSource.Stop ();
		efxSource.clip = clip;
		efxSource.Play ();
	}

	public void RandomizeSfx (params AudioClip [] clips)
	{
		int randomIndex = Random.Range (0, clips.Length);
		float randomPitch = Random.Range (lowPitchRange, highPitchRange);

		efxSource.pitch = randomPitch;
		efxSource.clip = clips[randomIndex];
		efxSource.Play ();
	}
}
