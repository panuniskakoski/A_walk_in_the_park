using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Script for all player related actions
public class player : MonoBehaviour
{
    // Audio clips
    public AudioSource audioS;
    public AudioClip[] sounds;

    // Meters that player needs to maintain
    public Slider scMeter;
    public Slider loveMeter;

    // Heads of the player
    public Animator manHead;
    public Animator womanHead;

    public SpriteRenderer manHeadSprite;
    public SpriteRenderer womanHeadSprite;
    public SpriteRenderer torsos;

    public GameObject manPunchScDepleted;
    public GameObject manPunchNoLove;
    public SpriteRenderer womanPunch;

    public Animator gameOverText;
    public Animator youSurvivedText;

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

    public GameObject mainCamera;
    public GameObject goal;

    public bool gameOverCheck;
    public bool gameWonCheck;

    public ParticleSystem hearts;

    // Start is called before the first frame update
    void Start()
    {
        // Winning conditions
        mainCamera = GameObject.Find("Main Camera");
        goal = GameObject.Find("Goal");

        // We get all the essential game objects ready
        scMeter = GameObject.Find("scBar").GetComponent<Slider>();
        loveMeter = GameObject.Find("loveBar").GetComponent<Slider>();
        manHead = GameObject.Find("manHead").GetComponent<Animator>();
        womanHead = GameObject.Find("womanHead").GetComponent<Animator>();

        manHeadSprite = GameObject.Find("manHead").GetComponent<SpriteRenderer>();
        womanHeadSprite = GameObject.Find("womanHead").GetComponent<SpriteRenderer>();
        torsos = GameObject.Find("walk").GetComponent<SpriteRenderer>();
        manPunchScDepleted = GameObject.Find("manPunchScDepleted");
        manPunchNoLove = GameObject.Find("manPunchNoLove");
        womanPunch = GameObject.Find("womanPunch").GetComponent<SpriteRenderer>();

        playerCouple = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        questionPopup = GameObject.Find("questionPopup").GetComponent<SpriteRenderer>();

        gameOverText = GameObject.Find("gameOverText").GetComponent<Animator>();
        youSurvivedText = GameObject.Find("youSurvivedText").GetComponent<Animator>();

        hearts = GameObject.Find("Hearts").GetComponent<ParticleSystem>();


        // Set helper variables
        girlsInSight = false;
        gfHasAQuestion = false;
        isWalking = true;
        walkSpeed = 1.5F;

        scDecrease = 0.12F;
        loveDecrease = 0.15F;

        gameOverCheck = false;
        gameWonCheck = false;

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
            // Couple is moving
            playerCouple.velocity = transform.right * walkSpeed;
            // Loop walk audio
            // TODO
        }
        else if (scMeter.value < 0.01F || loveMeter.value < 0.01F)
        {
            // Meters stop decreasing
            scDecrease = 0;
            loveDecrease = 0;

            // Stop walk audio
            // TODO

            // Make sure that meter cannot be manipulated anymore
            if (!gameOverCheck)
            {
                // Couple stops walking
                playerCouple.velocity = transform.right * 0;
                gameOverCheck = true;

                // Hides the unnecessary sprites
                manHeadSprite.enabled = false;
                womanHeadSprite.enabled = false;
                questionPopup.enabled = false;
                torsos.enabled = false;

                // And enables the game over animation
                if (scMeter.value < 0.01F) manPunchScDepleted.GetComponent<SpriteRenderer>().enabled = true;
                if (loveMeter.value < 0.01F) manPunchNoLove.GetComponent<SpriteRenderer>().enabled = true;
                womanPunch.enabled = true;
                // Play womans anger audio
                // TODO
            }
            // Timer goes off
            timer += Time.deltaTime;
            seconds = (int)(timer % 60);

            if (scMeter.value < 0.01F)
            {
                // Play mans drool audio
                // TODO läähpuuh

                // Timing animation transition
                if (seconds == 1)
                {
                    manPunchScDepleted.GetComponent<Animator>().SetTrigger("punched");
                    // TODO punch audio
                }
                if (seconds == 2)
                {
                    manPunchScDepleted.GetComponent<Animator>().SetBool("powed", true);
                    gameOverText.SetTrigger("Float");
                }
                if (seconds == 3)
                {
                    gameOverText.SetTrigger("Stay");
                }
            }
            else if (loveMeter.value < 0.1F)
            {
                // Play man whimpering audio
                // TODO nononon

                // Timing animation transition
                if (seconds == 1)
                {
                    manPunchNoLove.GetComponent<Animator>().SetTrigger("punched");
                    // TODO punch audio
                }
                if (seconds == 2)
                {
                    manPunchNoLove.GetComponent<Animator>().SetBool("powed", true);
                    gameOverText.SetTrigger("Float");
                }
                if (seconds == 3)
                {
                    gameOverText.SetTrigger("Stay");
                }
            }
        }
        
        // If player reaches end
        if (gameWonCheck)
        {
            // Timer goes off
            timer += Time.deltaTime;
            seconds = (int)(timer % 60);
            // Timing text animation transition
            if (seconds == 1)
            {
                youSurvivedText.SetTrigger("Float");
                hearts.Play();
                // Play love harps audio
                // TODO
            }
            if (seconds == 2)
            {
                youSurvivedText.SetTrigger("Stay");
                // Fade out walk audio
                // TODO AudioFadeOut.FadeOut(audioS, 2.0F);
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
        else scDecrease = 0;

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
    // Goal reached, camera stops here
    void OnTriggerEnter2D(Collider2D other)
    {
        // Camera stops
        if (other.gameObject.tag == "Goal")
        {
            mainCamera.transform.parent = goal.transform;
            // Meters stop decreasing
            scDecrease = 0;
            loveDecrease = 0;
            loveDecreaseAnswer = 0;
            scIncrease = 0;
            loveIncreaseAcknowledge = 0;
            gameWonCheck = true;
        }
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

    // Method for fading out audio
    public static class AudioFadeOut
    {
        public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
        {
            float startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
                yield return null;
            }
            audioSource.Stop();
            audioSource.volume = startVolume;
        }
    }
}
