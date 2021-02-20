using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttons : MonoBehaviour
{
    // Quits application
    public void ExitGame()
    {
        Application.Quit();
    }

    // Opens help menu
    public void Help()
    {
        SceneManager.LoadScene("HelpMenu");
    }

    // Starts the demo stage
    public void Play()
    {
        SceneManager.LoadScene("DemoStage");
    }
}
