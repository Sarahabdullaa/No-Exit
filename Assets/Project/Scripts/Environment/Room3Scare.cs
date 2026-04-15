using UnityEngine;
using System.Collections;

public class Room3Scare : MonoBehaviour
{
    public AudioSource doorBang;       // Drag the Bang sound here
    public AudioSource parentYelling;  // Drag the Muffled Yelling here (with Low Pass Filter)

    private bool hasPlayed = false;

    void OnTriggerEnter(Collider other)
    {
        // Only trigger for the player and only once
        if (!hasPlayed && other.CompareTag("Player"))
        {
            hasPlayed = true;
            StartCoroutine(PlayScareSequence());
        }
    }

    IEnumerator PlayScareSequence()
    {
        // 1. Immediate Loud Bang
        doorBang.Play();

        // 2. Short pause for the shock to sink in (0.5 seconds)
        yield return new WaitForSeconds(0.5f);

        // 3. The muffled shouting starts and loops
        parentYelling.Play();
        
    }
}