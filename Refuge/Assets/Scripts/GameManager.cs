using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public const int LEVEL_TRANSITION = 1;
	public const int LAST_LEVEL = 1;

	public float levelStartDelay = 0.01f;
	public float levelStartDelayWithQuote = 5.0f;
    public float TurnDelay = 0.1f;
    public static GameManager instance = null;
    public BoardManager wildBoardManager;
    public BoardManager cityBoardManager;
    private BoardManager boardScript;
    public int playerHealth = 30;
	public int healthGainPostTransition = 20;
    [HideInInspector]
    public bool playersTurn = true;

    private Text quoteText;
	private Text levelText;
	private GameObject levelImage;
	private GameObject quoteImage;
	private GameObject deathImage;
	private int level = 1;
    private int secondPhaseLevel = 1;
    private List<Enemy> enemies;
    private bool enemiesMoving;
	private bool doingSetup;
    private bool firstRun = true;
    private bool doingMidGameTransition = false;
    private bool midFirstRun = true;
    private bool midRunOnce = true;
    private bool isCityPhase = false;
	private bool win = false;
	private System.Random rand = new System.Random();

	private List<string> quotes = new List<string> ();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);


        playerHealth = DifficultyOptions.instance.phaseOneHp;
        healthGainPostTransition = DifficultyOptions.instance.phaseTwoHp;

        SoundManager.instance.playMainMusic ();
        enemies = new List<Enemy>();
        DontDestroyOnLoad(gameObject);

        if(level <= LEVEL_TRANSITION)
        {
            boardScript = wildBoardManager;
        } else
        {
            boardScript = cityBoardManager;
            isCityPhase = true;
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
			playerHealth = playerHealth + healthGainPostTransition;
            return;
        } else
        { 
            level++;
            InitGame();
        }
    }

    private IEnumerator MidGameTransition()
    {
		SoundManager.instance.playAlternateMusic ();
        doingMidGameTransition = true;
        PauseGame();
        SceneManager.LoadScene("MidScene");
        isCityPhase = true;
        level = 0;
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

    private void LoadQuotes()
    {
		if(quotes.Count == 0)
        {
			quotes.Add("We must learn to live together as brothers or perish together as fools.\n\n- Martin Luther King, Jr.");
			quotes.Add("Preservation of one's own culture does not require contempt or disrespect for other cultures.\n\n- Cesar Chavez");
			quotes.Add("We can see that immigration has become favorable terrain for the development of Islamism.\n\n- Marion Marachel-Le pen");
			quotes.Add("If our focus in immigration reform is exclusively on high-skilled or STEM immigrants, where do the rest of the millions yearning to join our ranks fit in?\n\n- Cedric Richmond");
			quotes.Add("Illegal immigration is crisis for our country. It is an open door for drugs, criminals, and potential terrorists to enter our country. It is straining our economy, adding costs to our judicial, healthcare, and education systems.\n\n- Timothy Murphy");
			quotes.Add("The more you can increase fear of drugs and crime, welfare mothers, immigrants and aliens, the more you control all the people.\n\n- Noam Chomsky");
			quotes.Add("I had always hoped that this land might become a safe and agreeable asylum to the virtuous and persecuted part of mankind, to whatever nation they might belong.\n\n- George Washington");
			quotes.Add("Everywhere immigrants have enriched and strengthened the fabric of American life.\n\n- John F. Kennedy");
			quotes.Add( "I don't see how the party that says it's the party of the family is going to adopt an immigration policy which destroys families that have been here a quarter century.\n\n- Newt Gingrich");
			quotes.Add( "The land flourished because it was fed from so many sources, because it was nourished by so many cultures and traditions and peoples.\n\n- Lyndon B. Johnson");
			quotes.Add( "Remember, remember always, that all of us, and you and I especially, are descended from immigrants and revolutionists.\n\n- Franklin D. Roosevelt");
			quotes.Add( "A nation that cannot control its borders is not a nation.\n\n- Ronald Reagan");
			quotes.Add( "No one leaves home unless home is the mouth of a shark.\n\n- Warsan Shire, Teaching My Mother How to Give Birth");
			quotes.Add( "The truth is, immigrants tend to be more American than people born here.\n\n- Chuck Palahniuk, Choke");
			quotes.Add( "Wilders understands that culture and demographics are our destiny. We can't restore our civilization with somebody else's babies.\n\n- Steve King");
			quotes.Add( "When Mexico sends its people, they're not sending their best... They're sending people that have lots of problems, and they're bringing those problems with us. They're bringing drugs. They're bringing crime. They're rapists. And some, I assume, are good people.\n\n- Donald J. Trump");
        }
    }

    void InitGame()
    {
        doingSetup = true;
		if (level > LAST_LEVEL) {
			win = true;
			quoteImage = GameObject.Find ("QuoteImage");
			levelImage = GameObject.Find ("LevelImage");
			deathImage = GameObject.Find ("DeathImage");
			GameOver ();
		} else {
			deathImage = GameObject.Find ("DeathImage");
			deathImage.SetActive (false);
			LoadQuotes ();
			quoteImage = GameObject.Find ("QuoteImage");
			quoteText = GameObject.Find ("QuoteText").GetComponent<Text> ();
			levelImage = GameObject.Find ("LevelImage");
			levelText = GameObject.Find ("LevelText").GetComponent<Text> ();
			levelText.text = "Day " + level;
			int chooseQuote = rand.Next (0, quotes.Count - 1);
			quoteText.text = quotes [chooseQuote];
			quotes.RemoveAt (chooseQuote);
			levelImage.SetActive (true);
			quoteImage.SetActive (true);
			quoteText.gameObject.SetActive (true);

			StartCoroutine (DelayGameStart ());

			enemies.Clear ();
			boardScript.SetupScene (level);
		}
        
    }

	private IEnumerator DelayGameStart()
	{
		yield return new WaitForSeconds(5.0f);
		Invoke ("HideLevelImage", levelStartDelay);
	}

    private IEnumerator DelayGameStartUntilInput()
    {
        while(!Input.anyKey)
        {
            yield return new WaitForSeconds(0.0001f);
        }
        Invoke("HideLevelImage", levelStartDelay);
    }

    private IEnumerator DelayGameRestartUntilInput()
    {
        yield return new WaitForSeconds(2.0f);
        while (!Input.anyKey)
        {
            yield return new WaitForSeconds(0.0001f);
        }
        SceneManager.LoadScene("StartScreen");
        Destroy(SoundManager.instance.gameObject);
        Destroy(gameObject);
    }

    private void HideLevelImage()
	{
		levelImage.SetActive(false);
		quoteImage.SetActive (false);
		doingSetup = false;
	}

	public void GameOver()
	{
		SoundManager.instance.playDeathMusic ();
		deathImage.SetActive(true);
		Text deathText = GameObject.Find ("DeathText").GetComponent<Text> ();
		if (win) deathText.text = "After weeks of running, Sal finally managed to lose the cops. He found a small town to lay low and hide in, but was forced to work unstable jobs for income as he didn't want to reveal his identity. He was finally safe, and could start working towards this Mireacan dream..\nBut at what cost?";
		else if(IsCityPhase())
		{
			deathText.text = "Unfortunately, Sal could not run his whole life.Exhausted, starving, and alone, he gave up. The police caught him and began interrogating him.\nWhere he is now --we may never know.\n\nHe made it " + (level + LEVEL_TRANSITION) + " days before having to give up.";
		} else
		{
			deathText.text = "Unfortunately, Sal could not complete the long journey to Mireaca. Exhausted and alone, he had to return to his home, which was now sucked dry by exploitation.\n\nHe made it " + level + " days before having to give up.";
		}
		levelImage.SetActive (false);
		quoteImage.SetActive (false);
		enabled = false;
		StartCoroutine(DelayGameRestartUntilInput());
	}

	public bool IsCityPhase(){
		return isCityPhase;
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
