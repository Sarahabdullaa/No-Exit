using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Radio : MonoBehaviour, IInteractable
{
    [Header("Audio Settings")]
    public AudioClip radioSound;        // The main radio content (music / voice)
    public AudioClip openSound;         // Click when turning ON
    public AudioClip closeSound;        // Click when turning OFF
    public float volume = 1f;

    private AudioSource audioSource;
    private bool isPlaying = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
        audioSource.loop = false;        // ensure one-shot
    }

    public void Interact()
    {
        if (!isPlaying)
        {
            // Turn ON
            if (openSound != null)
                audioSource.PlayOneShot(openSound);

            // Play main sound (can be delayed slightly for realism, but fine immediately)
            audioSource.PlayOneShot(radioSound);
            isPlaying = true;

            // Automatically reset flag after the clip finishes
            Invoke(nameof(ResetPlayFlag), radioSound.length);
        }
        else
        {
            // Turn OFF immediately
            audioSource.Stop();                // stops any ongoing playback

            if (closeSound != null)
                audioSource.PlayOneShot(closeSound);

            isPlaying = false;

            // Cancel any pending auto-reset to avoid double reset
            CancelInvoke(nameof(ResetPlayFlag));
        }
    }

    private void ResetPlayFlag()
    {
        isPlaying = false;
        // Optionally: play a "fade out" sound? Not needed.
    }
}