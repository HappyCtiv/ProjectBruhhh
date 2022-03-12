using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void ReloadGame()
    {
        SceneManager.LoadScene(0);//Choose the scene that you want to reload/load 
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit(); //can redo to quiting to main menu
    }

    /*
    public void QuitToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(NubmerOfTheMainMenuScene);
    }

    */
}
