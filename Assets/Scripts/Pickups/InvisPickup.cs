using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InvisPickup : MonoBehaviour
{
    public AudioClip InvSound;
    AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<PlaerMovementScript>().isVisible = false;
            Debug.Log ("You are invisible");
            audioSource.PlayOneShot(InvSound);
            Destroy(gameObject);
        }

    }
}
