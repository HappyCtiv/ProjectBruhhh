using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivasionPick : MonoBehaviour
{
    public AudioClip ActivasionSound;
    AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<EnemyAI>().RelicPicked = true;
            audioSource.PlayOneShot(ActivasionSound);
            Destroy(gameObject);
        }

    }
}
