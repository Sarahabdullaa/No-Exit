using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Radio : MonoBehaviour, IInteractable
{
    [Header("Audio Settings")]
    public AudioClip radioSound;   // Drag sound clip here in the Inspector
    public float volume = 1f;

    private AudioSource audioSource;
    private bool isPlaying = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
    }

    public void Interact()
    {
        if (!isPlaying)
        {
            // Play the sound once
            audioSource.PlayOneShot(radioSound);
            isPlaying = true;

            // Optional: auto-reset flag after clip length (so player can play again)
            Invoke(nameof(ResetPlayFlag), radioSound.length);
        }
        else
        {
            // If already playing, do nothing or stop 
            // For simplicity, we do nothing for now
        }
    }

    private void ResetPlayFlag()
    {
        isPlaying = false;
    }
}