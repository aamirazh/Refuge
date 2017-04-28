using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenButtons : MonoBehaviour {

	public void TransitionToOpeningScene()
    {
        StartSceneSoundManager.instance.pauseMusic();
        Destroy(StartSceneSoundManager.instance.gameObject);
        SceneManager.LoadScene("OpeningScene");
    }

    public void TransitionToCreditsScene()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void TransitionToStartScene()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void TransitionToTutorial()
    {
        SceneManager.LoadScene("ControlsScene");
    }
}
