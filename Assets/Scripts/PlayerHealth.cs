using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float hitPoints = 3f;
    public Text countLives;
    public AudioClip HitSound;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        LivesCountText();
    }

    public void TakeDamage (float Damage)
    {
        hitPoints -= Damage;

        if (hitPoints<=0)
        {
           GetComponent<DeathHandeler>().HandleDeath();
           LivesCountText();
        }

        if (hitPoints > 0)
        {
            audioSource.PlayOneShot(HitSound);
            LivesCountText();
        }
    } 
    
    void LivesCountText() //lives counter
    {
        countLives.text = "Lives: " + hitPoints.ToString();
    }

}
