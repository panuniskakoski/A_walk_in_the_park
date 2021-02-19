﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script for all player related actions
public class player : MonoBehaviour
{
    // Meters that player needs to maintain
    public Slider scMeter;
    public Slider loveMeter;

    // Heads of the player
    public Animator manHead;
    public Animator womanHead;

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

    // Aka the player game object
    public Rigidbody2D playerCouple;

    // Tracks weather the bg should scroll or not
    public bool isWalking;
    public float walkSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // We get all the essential game objects ready
        scMeter = GameObject.Find("scBar").GetComponent<Slider>();
        loveMeter = GameObject.Find("loveBar").GetComponent<Slider>();
        manHead = GameObject.Find("manHead").GetComponent<Animator>();
        womanHead = GameObject.Find("womanHead").GetComponent<Animator>();
        playerCouple = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        questionPopup = GameObject.Find("questionPopup").GetComponent<SpriteRenderer>();

        // Set helper variables
        girlsInSight = false;
        gfHasAQuestion = false;
        isWalking = true;
        walkSpeed = 2.0F;

        scDecrease = 0.05F;
        loveDecrease = 0.05F;

        // How much self control player gets for tapping the buttons
        scIncrease = 0.01F;

        // How much love player gets from answering gfs question
        loveIncreaseAnswer = 0.05F;
        // Penalty for answering question no one asked
        loveDecreaseAnswer = 0.15F;

        // How much love player gets from acknowledging gf
        loveIncreaseAcknowledge = 0.01F;
    }

    // Update is called once per frame
    void Update()
    {
        // Tracks if player is moving
        if (scMeter.value > 0)
        {
            playerCouple.velocity = transform.right * walkSpeed;
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

        // Read player inputs
        if (Input.GetButtonDown("scButton1")) scMeter.value += scIncrease;
        if (Input.GetButtonDown("scButton2")) scMeter.value += scIncrease;

        // If player presses acknowledge button increase self control meter and run animation once
        // TODO: Add acknowledgement cooldown so animation cannot be spammed
        if (Input.GetButtonDown("Acknowledge"))
        {
            manHead.SetTrigger("Acknowledge");
            womanHead.SetTrigger("Acknowledge");
            // TODO: play kissy sound
            loveMeter.value += loveIncreaseAcknowledge;
        }

        // If player presses answer button when there is no question decrease love meter and run animation once
        // TODO: Add answer cooldown so animation cannot be spammed
        if (Input.GetButtonDown("Answer") && !gfHasAQuestion)
        {
            manHead.SetTrigger("Answer");
            // TODO: play smug mumble sound
            loveMeter.value -= loveDecreaseAnswer;
        }

        // If player presses answer button to answer a question increase love meter and run animation once
        if (Input.GetButtonDown("Answer") && gfHasAQuestion)
        {
            manHead.SetTrigger("Answer");
            womanHead.SetTrigger("Answer");
            // TODO: play mumble sound
            loveMeter.value += loveIncreaseAnswer;
            gfHasAQuestion = false;
            questionPopup.enabled = false;
        }

        // Checks if there is a question that nees an answer
        calculateGfMind();

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
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "girls") girlsInSight = true;
    }

    // If man no longer has a line of sight to other girls
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "girls") girlsInSight = false;
    }
}
