using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneSoundManager : MonoBehaviour
{

    public AudioSource efxSource;
    public AudioSource musicSource;
    public AudioClip startSceneMusic;
   
    public static StartSceneSoundManager instance = null;

    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        playStartSceneMusic();
    }

    public void pauseMusic()
    {
        musicSource.Stop();
    }

    public void playStartSceneMusic()
    {
        musicSource.Stop();
        musicSource.clip = startSceneMusic;
        musicSource.Play();
    }

    
}
