using UnityEngine;
using System.Collections;

public class Room3Scare : MonoBehaviour
{
    public AudioSource arguingAudio;
    public AudioSource bangDoorAudio;

    private bool hasPlayed = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            if (arguingAudio != null)
                arguingAudio.Play();

            if (bangDoorAudio != null)
                bangDoorAudio.Play();

            hasPlayed = true;
        }
    }

}