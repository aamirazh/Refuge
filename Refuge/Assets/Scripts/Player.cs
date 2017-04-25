using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MovingObject {

    public int wallDamage = 2;
    public int pointsPerFood = 10;
    // public int pointsPerSoda = 20;
    public float restartLevelDelay = 1f;
	public Text foodText;
	public AudioClip moveSound1;
	public AudioClip moveSound2;
	public AudioClip eatSound1;
	public AudioClip eatSound2;
	// public AudioClip drinkSound1;
	// public AudioClip drinkSound2;

    private Animator animator;
    private int food;
    private bool firstRun = true;
	private string healthText = "Health: ";

	// Use this for initialization
	protected override void Start () {
        animator = GetComponent<Animator>();

        food = GameManager.instance.playerHealth;

		foodText.text = healthText + food;
        base.Start();
	}

    private void OnDisable()
    {
        GameManager.instance.playerHealth = food;
    }

    private void OnEnable()
    {

        food = GameManager.instance.playerHealth;
        
    }

    // Update is called once per frame
    void Update () {
        if (!GameManager.instance.playersTurn) return;
 
        int horizontal = 0;
        int vertical = 0;

        horizontal = (int) Input.GetAxisRaw("Horizontal");
        vertical = (int) Input.GetAxisRaw("Vertical");

        if (horizontal != 0)
            vertical = 0;

        if (horizontal != 0 || vertical != 0)
            AttemptMove<Wall>(horizontal, vertical);
	}

    protected override void AttemptMove<T> (int xDir, int yDir)
    {
        food--;
		foodText.text = healthText + food;

        if(xDir < 0)
        {
            animator.SetTrigger("playerTurnLeft");
            animator.ResetTrigger("playerTurnRight");
        } else if(xDir > 0)
        {
            animator.SetTrigger("playerTurnRight");
            animator.ResetTrigger("playerTurnLeft");
        }

        base.AttemptMove <T> (xDir, yDir);

        RaycastHit2D hit;
		if (Move (xDir, yDir, out hit)) {
			SoundManager.instance.RandomizeSfx (moveSound1, moveSound2);
		}

        CheckIfGameOver();

        GameManager.instance.playersTurn = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (other.tag == "Food")
        {
            food += pointsPerFood;
			foodText.text = "+" + pointsPerFood + " Food: " + food;
			SoundManager.instance.RandomizeSfx (eatSound1, eatSound2);
            other.gameObject.SetActive(false);
        }
        // else if (other.tag == "Soda")
    }
    protected override void OnCantMove <T> (T component)
    {
        Wall hitWall = component as Wall;
        hitWall.DamageWall(wallDamage);
        animator.SetTrigger("playerAttack");
    }

    private void Restart()
    {
        GameManager.instance.playerHealth = food;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        food = GameManager.instance.playerHealth;
    }

    public void LoseFood (int loss)
    {
        animator.SetTrigger("playerHit");
        food -= loss;
		foodText.text = "-" + loss + " Food: " + food;
        CheckIfGameOver();
    }

    private void CheckIfGameOver()
    {
		if (food <= 0) {
			GameManager.instance.GameOver ();
		}
    }
}
