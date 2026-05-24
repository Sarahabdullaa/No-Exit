using UnityEngine;

public class PlaySoundOnStep : MonoBehaviour
{
    public AudioSource audioSource;
    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        
        if (!hasPlayed && other.CompareTag("Player"))
        {
            if (audioSource != null)
            {
                audioSource.Play();
                hasPlayed = true;
            }

          
            Destroy(gameObject, audioSource.clip.length);
        }
    }
}