using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class helpMenu : MonoBehaviour
{
    public Button nextButton;
    public Button previousButton;

    public AudioClip click;
    AudioSource audioS;

    public SpriteRenderer slide1;
    public SpriteRenderer slide2;
    public SpriteRenderer slide3;
    public SpriteRenderer slide4;
    public SpriteRenderer slide5;
    public SpriteRenderer slide6;
    public SpriteRenderer slide7;
    public SpriteRenderer slide8;

    void Start()
    {
        nextButton = GameObject.Find("NextButton").GetComponent<Button>();
        previousButton = GameObject.Find("PreviousButton").GetComponent<Button>();

        // TODO: Get all slides ready. I know this is dumb, but I'm tired.
        slide1 = GameObject.Find("slide1").GetComponent<SpriteRenderer>();
        slide2 = GameObject.Find("slide2").GetComponent<SpriteRenderer>();
        slide3 = GameObject.Find("slide3").GetComponent<SpriteRenderer>();
        slide4 = GameObject.Find("slide4").GetComponent<SpriteRenderer>();
        slide5 = GameObject.Find("slide5").GetComponent<SpriteRenderer>();
        slide6 = GameObject.Find("slide6").GetComponent<SpriteRenderer>();
        slide7 = GameObject.Find("slide7").GetComponent<SpriteRenderer>();
        slide8 = GameObject.Find("slide8").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Disables the previous button if at the beginning of the child object list
        if (slide1.enabled)
        {
            previousButton.interactable = false;
        }
        else previousButton.interactable = true;

        // Disables the next button if at the end of the child object list
        if (slide8.enabled)
        {
            nextButton.interactable = false;
        }
        else nextButton.interactable = true;
    }

    // Opens main menu
    public void MainMenu()
    {
        StartCoroutine(WaitForAudio());
    }

    // Previous tutorial slide
    // I know this is dumb, but I'm tired.
    // TODO: do this more efficiently
    public void Previous()
    {
        // From slide 8 to 7
        if (slide8.enabled)
        {
            slide8.enabled = false;
            slide7.enabled = true;
        }
        // From slide 7 to 6
        else if (slide7.enabled)
        {
            slide7.enabled = false;
            slide6.enabled = true;
        }
        // From slide 6 to 5
        else if (slide6.enabled)
        {
            slide6.enabled = false;
            slide5.enabled = true;
        }
        // From slide 5 to 4
        else if (slide5.enabled)
        {
            slide5.enabled = false;
            slide4.enabled = true;
        }
        // From slide 4 to 3
        else if (slide4.enabled)
        {
            slide4.enabled = false;
            slide3.enabled = true;
        }
        // From slide 3 to 2
        else if (slide3.enabled)
        {
            slide3.enabled = false;
            slide2.enabled = true;
        }
        // From slide 2 to 1
        else if (slide2.enabled)
        {
            slide2.enabled = false;
            slide1.enabled = true;
        }
    }

    // Next tutorial slide
    // I know this is dumb, but I'm tired.
    // TODO: do this more efficiently
    public void Next()
    {
        // From slide 1 to 2
        if (slide1.enabled)
        {
            slide1.enabled = false;
            slide2.enabled = true;
        }
        // From slide 2 to 3
        else if (slide2.enabled)
        {
            slide2.enabled = false;
            slide3.enabled = true;
        }
        // From slide 3 to 4
        else if (slide3.enabled)
        {
            slide3.enabled = false;
            slide4.enabled = true;
        }
        // From slide 4 to 5
        else if (slide4.enabled)
        {
            slide4.enabled = false;
            slide5.enabled = true;
        }
        // From slide 5 to 6
        else if (slide5.enabled)
        {
            slide5.enabled = false;
            slide6.enabled = true;
        }
        // From slide 6 to 7
        else if (slide6.enabled)
        {
            slide6.enabled = false;
            slide7.enabled = true;
        }
        // From slide 7 to 8
        else if (slide7.enabled)
        {
            slide7.enabled = false;
            slide8.enabled = true;
        }
    }

    // For buttons that change scene
    public IEnumerator WaitForAudio()
    {
        yield return new WaitForSeconds(0.5F);
        SceneManager.LoadScene("MainMenu");
    }
}
