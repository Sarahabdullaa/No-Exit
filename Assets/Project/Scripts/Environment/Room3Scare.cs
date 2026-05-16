using UnityEngine;
using System.Collections;

public class Room3Scare : MonoBehaviour
{
    public AudioSource arguingAudio;

    private bool hasPlayed = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            if (arguingAudio != null)
                arguingAudio.Play();

            hasPlayed = true;
        }
    }

}