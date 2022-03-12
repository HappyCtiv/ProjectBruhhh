using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu2 : MonoBehaviour
{
    public static bool gameIsPaused = false;
  
	public KeyCode PauseKey = KeyCode.P;
    [SerializeField] Canvas pauseMenuUI;

    private void Start()
    {
        pauseMenuUI.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(PauseKey))
        {
            Debug.LogError("SUKABLYAT");
            if (gameIsPaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume() //Resumes the game
    {
        pauseMenuUI.enabled = false;
        Time.timeScale = 1;
        gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause() //Pauses the game(no button)
    {
        pauseMenuUI.enabled = true;
        Time.timeScale = 0;
        gameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMenu() //Load the menu (Menu button)
    {
        SceneManager.LoadScene(0);
    }


    public void QuitGame() // Quits the game (Quit button)
    {
        Application.Quit();
    }
}
