using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Script for all player related actions
public class player : MonoBehaviour
{
    // Meters that player needs to maintain
    public Slider scMeter;
    public Slider loveMeter;

    // Heads of the player
    public Animator manHead;
    public Animator womanHead;

    public SpriteRenderer manHeadSprite;
    public SpriteRenderer womanHeadSprite;
    public SpriteRenderer torsos;

    public GameObject manPunch;
    public SpriteRenderer womanPunch;

    public Animator gameOverText;

    // UI buttons
    public Button mainMenu;
    public Button scButton1;
    public Button scButton2;
    public Button answerButton;
    public Button acknowledgeButton;

    // Helper variables
    public bool girlsInSight;
    public bool gfHasAQuestion;
    public float scDecrease;
    public float loveDecrease;

    public float scIncrease;

    public float loveIncreaseAnswer;
    public float loveDecreaseAnswer;
    public float loveIncreaseAcknowledge;

    public float answerTimer;
    public float setAnswerTimer;
    public SpriteRenderer questionPopup;

    public float timer;
    public int seconds;

    // Aka the player game object
    public Rigidbody2D playerCouple;

    // Tracks weather the bg should scroll or not
    public bool isWalking;
    public float walkSpeed;

    public bool gameOverCheck;

    // Start is called before the first frame update
    void Start()
    {
        // We get all the essential game objects ready
        scMeter = GameObject.Find("scBar").GetComponent<Slider>();
        loveMeter = GameObject.Find("loveBar").GetComponent<Slider>();
        manHead = GameObject.Find("manHead").GetComponent<Animator>();
        womanHead = GameObject.Find("womanHead").GetComponent<Animator>();

        manHeadSprite = GameObject.Find("manHead").GetComponent<SpriteRenderer>();
        womanHeadSprite = GameObject.Find("womanHead").GetComponent<SpriteRenderer>();
        torsos = GameObject.Find("walk").GetComponent<SpriteRenderer>();
        manPunch = GameObject.Find("manPunch");
        womanPunch = GameObject.Find("womanPunch").GetComponent<SpriteRenderer>();

        playerCouple = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        questionPopup = GameObject.Find("questionPopup").GetComponent<SpriteRenderer>();

        gameOverText = GameObject.Find("gameOverText").GetComponent<Animator>();

        // Set helper variables
        girlsInSight = false;
        gfHasAQuestion = false;
        isWalking = true;
        walkSpeed = 1.5F;

        scDecrease = 0.12F;
        loveDecrease = 0.15F;

        gameOverCheck = false;

        timer = 0.0F;
        seconds = 0;

        // How much self control player gets for tapping the buttons
        scIncrease = 0.01F;

        // How much love player gets from answering gfs question
        loveIncreaseAnswer = 0.05F;
        // Penalty for answering question no one asked
        loveDecreaseAnswer = 0.15F;

        // How much love player gets from acknowledging gf
        loveIncreaseAcknowledge = 0.05F;
    }

    // Update is called once per frame
    void Update()
    {
        // Tracks if player is moving
        if (scMeter.value > 0 && loveMeter.value > 0)
        {
            playerCouple.velocity = transform.right * walkSpeed;
        }
        else if (scMeter.value < 0.01F || loveMeter.value < 0.01F)
        {
            // Make sure that meter cannot be manipulated anymore
            if (!gameOverCheck)
            {
                playerCouple.velocity = transform.right * 0;
                gameOverCheck = true;

                // Hides the unnecessary sprites
                manHeadSprite.enabled = false;
                womanHeadSprite.enabled = false;
                questionPopup.enabled = false;
                torsos.enabled = false;

                // And enables the game over animation
                manPunch.GetComponent<SpriteRenderer>().enabled = true;
                womanPunch.enabled = true;
            }
            // Timer goes off
            timer += Time.deltaTime;
            seconds = (int)(timer % 60);

            // Timing animation transition
            if (seconds == 1)
            {
                manPunch.GetComponent<Animator>().SetTrigger("punched");
                scMeter.value = 0.0F;
            }
            if (seconds == 2)
            {
                manPunch.GetComponent<Animator>().SetBool("powed", true);
                gameOverText.SetTrigger("Float");
            }
            if (seconds == 3)
            {
                gameOverText.SetTrigger("Stay");
            }
        }

        // If girls are in sight, self control meter (scMeter) goes down gradually
        if (girlsInSight)
        {
            scMeter.value -= scDecrease * Time.deltaTime;
        }

        // If girlfriend has a question, love meter goes down
        if (gfHasAQuestion)
        {
            loveMeter.value -= loveDecrease * Time.deltaTime;
        }


        // -----------Read player inputs----------------------------------------------------------------------------------------------

        if (Input.GetButtonDown("Return")) Return();

        if (Input.GetButtonDown("scButton1") || Input.GetButtonDown("scButton2")) SelfControlButton();

        // If player presses acknowledge button increase self control meter and run animation once
        if (Input.GetButtonDown("Acknowledge")) Acknowledge();

        // If player presses answer button when there is no question decrease love meter and run animation once
        if (Input.GetButtonDown("Answer")) AnswerUnaskedQuestion();

        // If player presses answer button to answer a question increase love meter and run animation once
        if (Input.GetButtonDown("Answer")) Answer();

        // -----------Read player inputs end here--------------------------------------------------------------------------------------


        // Checks if there is a question that nees an answer
        if (!gameOverCheck) calculateGfMind();

        // This changes the mans animator variable as things change
        manHead.SetFloat("scMeter", scMeter.value);

        // This changes the womans animator variable as things change
        womanHead.SetFloat("loveMeter", loveMeter.value);
    }

    // Function that runs each frame if gf has no questions yet
    void calculateGfMind()
    {
        // Change randomInt to change chances for questions
        // TODO: Better system for this
        int randomInt = Random.Range(0, 400);
        if (!gfHasAQuestion && (randomInt == 5))
        {
            gfHasAQuestion = true;
            questionPopup.enabled = true;
            setAnswerTimer = answerTimer;
        }
    }
    
    // If man enters a line of sight to other girls
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "girls") girlsInSight = true;
    }
    // If man no longer has a line of sight to other girls
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "girls") girlsInSight = false;
    }

    // Button press functions
    // ----------------------------------------------------------------------
    // Returns to main menu
    public void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Restart run
    public void Restart()
    {
        SceneManager.LoadScene("DemoStage");
    }

    // Adds some points to your scMeter
    public void SelfControlButton()
    {
        if (!gameOverCheck) scMeter.value += scIncrease;
    }

    // Adds some points to your loveMeter
    // TODO: Add acknowledgement cooldown so animation cannot be spammed
    public void Acknowledge()
    {
        if (!gameOverCheck)
        {
            manHead.SetTrigger("Acknowledge");
            womanHead.SetTrigger("Acknowledge");
            // TODO: play kissy sound
            loveMeter.value += loveIncreaseAcknowledge;
        }
    }

    // Answers to a unasked question question
    // TODO: Add answer cooldown so animation cannot be spammed
    public void AnswerUnaskedQuestion()
    {
        if (!gfHasAQuestion && !gameOverCheck)
        {
            manHead.SetTrigger("Answer");
            // TODO: play smug mumble sound
            loveMeter.value -= loveDecreaseAnswer;
        }
    }

    // Answer to a question
    // TODO: Add answer cooldown so animation cannot be spammed
    public void Answer()
    {
        if (gfHasAQuestion && !gameOverCheck)
        {
            manHead.SetTrigger("Answer");
            womanHead.SetTrigger("Answer");
            // TODO: play mumble sound
            loveMeter.value += loveIncreaseAnswer;
            gfHasAQuestion = false;
            questionPopup.enabled = false;
        }
    }
}
