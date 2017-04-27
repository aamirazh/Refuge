using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public const int LEVEL_TRANSITION = 4;

	public float levelStartDelay = 0.01f;
    public float TurnDelay = 0.1f;
    public static GameManager instance = null;
    public BoardManager wildBoardManager;
    public BoardManager cityBoardManager;
    private BoardManager boardScript;
    public int playerHealth = 30;
	public int playerHealthPostTransition = 20;
    [HideInInspector]
    public bool playersTurn = true;

    private Text quoteText;
	private Text levelText;
	private GameObject levelImage;
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

    private Dictionary<int, string> quotes = new Dictionary<int, string>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

		SoundManager.instance.playMainMusic ();
        enemies = new List<Enemy>();
        DontDestroyOnLoad(gameObject);

        if(level < LEVEL_TRANSITION)
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
			playerHealth = playerHealthPostTransition;
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
        if(!quotes.ContainsKey(1))
        {
            quotes.Add(1, "I refuse to accept the view that mankind is so tragically bound to the starless midnight of racism and war that the bright daybreak of peace and brotherhood can never become a reality... I believe that unarmed truth and unconditional love will have the final word. Martin Luther King Jr");
            quotes.Add(2, "We must learn to live together as brothers or perish together as fools. Martin Luther King, Jr.");
            quotes.Add(3, "Preservation of one's own culture does not require contempt or disrespect for other cultures.  Cesar Chavez");
            quotes.Add(4, "We can see that immigration has become favorable terrain for the development of Islamism.  Marion Marachel-Le pen");
            quotes.Add(5, "If our focus in immigration reform is exclusively on high-skilled or STEM immigrants, where do the rest of the millions yearning to join our ranks fit in? Cedric Richmond");
            quotes.Add(6, "Illegal immigration is crisis for our country. It is an open door for drugs, criminals, and potential terrorists to enter our country. It is straining our economy, adding costs to our judicial, healthcare, and education systems. Timothy Murphy");
            quotes.Add(7, "The more you can increase fear of drugs and crime, welfare mothers, immigrants and aliens, the more you control all the people. Noam Chomsky");
            quotes.Add(8, "I had always hoped that this land might become a safe and agreeable asylum to the virtuous and persecuted part of mankind, to whatever nation they might belong.  George Washington");
            quotes.Add(9, "Everywhere immigrants have enriched and strengthened the fabric of American life.  John F. Kennedy");
            quotes.Add(10, "I don't see how the party that says it's the party of the family is going to adopt an immigration policy which destroys families that have been here a quarter century.  Newt Gingrich");
            quotes.Add(11, "The land flourished because it was fed from so many sources-because it was nourished by so many cultures and traditions and peoples.  -Lyndon B. Johnson");
            quotes.Add(12, "Remember, remember always, that all of us, and you and I especially, are descended rom immigrants and revolutionists.  Franklin D. Roosevelt");
            quotes.Add(13, "A nation that cannot control its borders is not a nation.  Ronald Reagan");
            quotes.Add(14, "No one leaves home unless home is the mouth of a shark.  Warsan Shire, Teaching My Mother How to Give Birth");
            quotes.Add(15, "The truth is, immigrants tend to be more American than people born here.  Author Chuck Palahniuk");
            quotes.Add(16, "Wilders understands that culture and demographics are our destiny. We can't restore our civilization with somebody else's babies.  Steve King");
            quotes.Add(17, "I would ask you to go back through history and figure out where are these contributions that have been made by these other categories of people that you are talking about, where did any other subgroup of people contribute more to civilization? Than — than western civilization itself that's rooted in western Europe, eastern Europe and the United States of America, and every place where Christianity settled the world. That's all of western civilization.  Steve King");
            quotes.Add(18, "When Mexico sends its people, they're not sending their best.  They're not sending you.  They're not sending you.  They're sending people that have lots of problems, and they're bringing those problems with us.  They're bringing drugs.  They're bringing crime.  They're rapists.  And some, I assume, are good people.  Donald J. Trump");
        }
    }

    void InitGame()
    {
        doingSetup = true;
        LoadQuotes();
        quoteText = GameObject.Find("QuoteText").GetComponent<Text>();
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Day " + level;
        System.Random rand = new System.Random();
        int chooseQuote = rand.Next(1, 17);
        quoteText.text = quotes[chooseQuote];
        levelImage.SetActive(true);

        StartCoroutine(DelayGameStartUntilInput());

        enemies.Clear();
        boardScript.SetupScene(level);
        
    }

    private IEnumerator DelayGameStartUntilInput()
    {
        while(!Input.anyKey)
        {
            yield return new WaitForSeconds(0.0001f);
        }
        Invoke("HideLevelImage", levelStartDelay);
    }

	private void HideLevelImage()
	{
		levelImage.SetActive(false);
		doingSetup = false;
	}

    public void GameOver()
    {
		SoundManager.instance.playDeathMusic ();
        levelText.fontSize = 16;
        string textAddition = "";
        if(IsCityPhase())
        {
			textAddition = "Unfortunately, Bob could not run his whole life.\nExhausted, starving, and alone, he gave up.\n The police caught him and began interrogating him.\nWhere he is now --we may never know.\n\nYou made it " + (level + LEVEL_TRANSITION) + " days before having to give up.";
        } else
        {
            textAddition = "Unfortunately, Bob could not complete the long journey\nto Mireaca. Exhausted and alone, he had to return\nto his home, which was now sucked dry by exploitation.\n\nYou made it " + level + " days before having to give up.";
        }
		levelText.text = textAddition;
        quoteText.gameObject.SetActive(false);
		levelImage.SetActive(true);
        enabled = false;
    }

    public bool IsCityPhase()
    {
        if(level > LEVEL_TRANSITION)
        {
            isCityPhase = true;
        }
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
