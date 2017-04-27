using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenButtons : MonoBehaviour {

	public void TransitionToOpeningScene()
    {
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
}
