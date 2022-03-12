using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandeler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Canvas winCanvas;

    public AudioClip DeathSound;
    public AudioClip WinSound;
    AudioSource audioSource;

    private void Start()
    {
        gameOverCanvas.enabled = false;
        winCanvas.enabled = false;
        audioSource = GetComponent<AudioSource>();
    }

    public void HandleDeath()
    {
        gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        audioSource.PlayOneShot(DeathSound);
    }
    
    public void HandleWin()
    {
        winCanvas.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        audioSource.PlayOneShot(WinSound);
    }
}
