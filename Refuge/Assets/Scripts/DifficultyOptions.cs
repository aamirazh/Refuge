using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyOptions : MonoBehaviour {

    public static DifficultyOptions instance = null;

    public int phaseOneHp = 30;
    public int phaseTwoHp = 20;

    private Text displayText;

	// Use this for initialization
	void Awake()
    {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        displayText = GameObject.Find("Display").GetComponent<Text>();
    }

    public void hideOptions()
    {
        this.gameObject.SetActive(false);
        this.enabled = false;
    }

    public void showOptions()
    {
        this.gameObject.SetActive(true);
        this.enabled = true;
    }

    public void activateEasy()
    {
        phaseOneHp = 50;
        phaseTwoHp = 20;
        displayText.text = "Current Difficulty: Easy";
    }

    public void activateNormal()
    {
        phaseOneHp = 30;
        phaseTwoHp = 10;
        displayText.text = "Current Difficulty: Normal";
    }

    public void activateHard()
    {
        phaseOneHp = 20;
        phaseTwoHp = 5;
        displayText.text = "Current Difficulty: Hard";
    }
}
