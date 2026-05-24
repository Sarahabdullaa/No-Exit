
using UnityEngine;

public class WhisperTriggerZone : MonoBehaviour
{
    public AudioSource audioSource;
    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            if (!hasPlayed)
            {
                PlayWhisper();
            }
        }
    }

    private void PlayWhisper()
    {
        if (audioSource != null)
        {
            audioSource.Play();
            hasPlayed = true;

            Destroy(gameObject, audioSource.clip.length);
        }
    }
}