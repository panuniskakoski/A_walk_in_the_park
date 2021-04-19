using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttons : MonoBehaviour
{
    public bool play = false;

    // Quits application
    public void ExitGame()
    {
        StartCoroutine(PlaySoundAndWait());
        Application.Quit();
    }

    // Opens help menu
    public void Help()
    {
        StartCoroutine(PlaySoundAndWait());
        SceneManager.LoadScene("HelpMenu");
    }

    // Starts the demo stage
    public void Play()
    {
        play = true;
        StartCoroutine(PlaySoundAndWait());
    }

    IEnumerator PlaySoundAndWait()
    {
        yield return new WaitForSeconds(0.5f);
        // Some programming gum (don't know why demo stage didn't wait that time fully)
        if(play)SceneManager.LoadScene("DemoStage");
    }
}
