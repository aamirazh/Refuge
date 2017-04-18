using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public const int LEVEL_TRANSITION = 2;

	public float levelStartDelay = 2f;
    public float TurnDelay = 0.1f;
    public static GameManager instance = null;
    public BoardManager wildBoardManager;
    public BoardManager cityBoardManager;
    private BoardManager boardScript;
    public int playerFoodPoints = 100;
    [HideInInspector]
    public bool playersTurn = true;

	private Text levelText;
	private GameObject levelImage;
    private int level = 1;
    private List<Enemy> enemies;
    private bool enemiesMoving;
	private bool doingSetup;
    private bool firstRun = true;
    private bool doingMidGameTransition = false;
    private bool midFirstRun = true;
    private bool midRunOnce = true;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        enemies = new List<Enemy>();
        DontDestroyOnLoad(gameObject);

        if(level < LEVEL_TRANSITION)
        {
            boardScript = wildBoardManager;
        } else
        {
            boardScript = cityBoardManager;
        }
        if(!doingMidGameTransition)
        {
            InitGame();
        }
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (firstRun)
        {
            firstRun = false;
            return;
        }

        if(doingMidGameTransition)
        {
            return;
        }

        if (level >= LEVEL_TRANSITION && midRunOnce)
        {
            midRunOnce = false;
            boardScript = cityBoardManager;
            StartCoroutine(MidGameTransition());
        }
        else if (midFirstRun && !midRunOnce)
        {
            midFirstRun = false;
            return;
        } else
        { 
            level++;
            InitGame();
        }
    }

    private IEnumerator MidGameTransition()
    {
        doingMidGameTransition = true;
        PauseGame();
        SceneManager.LoadScene("MidScene");
        while (SceneManager.GetActiveScene().name.Equals("MidScene"))
        {
            yield return new WaitForSeconds(0.1f);
        }
        UnpauseGame();
        doingMidGameTransition = false;
        InitGame();
    }

    void Update()
    {
        if(playersTurn || enemiesMoving || doingSetup || doingMidGameTransition)
        {
            return;
        }
        StartCoroutine(MoveEnemies());
    }

    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(TurnDelay);
        if(enemies.Count == 0)
        {
            yield return new WaitForSeconds(TurnDelay);
        }
        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }

        playersTurn = true;
        enemiesMoving = false;
    }

    void InitGame()
    {
        doingSetup = true;

        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Day " + level;
        levelImage.SetActive(true);
        Invoke("HideLevelImage", levelStartDelay);

        enemies.Clear();
        boardScript.SetupScene(level);
        
    }

	private void HideLevelImage()
	{
		levelImage.SetActive(false);
		doingSetup = false;
	}

    public void GameOver()
    {
		levelText.text = "After " + level + " days, you starved.";
		levelImage.SetActive(true);
        enabled = false;
    }

    public bool IsCityPhase()
    {
        return level > LEVEL_TRANSITION;
    }

    public bool DoingMidTransition()
    {
        return doingMidGameTransition;
    }

    private void PauseGame()
    {

        enabled = false;
        instance.enabled = false;
        instance.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    private void UnpauseGame()
    {
        enabled = true;
        this.gameObject.SetActive(true);
        instance.enabled = true;
        instance.gameObject.SetActive(true);
    }
}
