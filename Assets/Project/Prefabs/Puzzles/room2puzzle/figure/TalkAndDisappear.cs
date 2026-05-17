using UnityEngine;

public class TalkAndDisappear : MonoBehaviour, IInteractable
{
    [Header("Audio")]
    public AudioClip voiceLine;           // Drag your voice clip here

    [Header("Animation (Optional)")]
    public string disappearTriggerName = "Disappear"; // Animator trigger parameter

    [Header("Behaviour")]
    public float delayBeforeDestroy = 0f;  // 0 = auto use clip length, or set seconds

    private AudioSource audioSource;
    private bool isTriggered = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void Interact()
    {
        if (isTriggered) return;
        isTriggered = true;

        // Play voice line
        if (voiceLine != null)
        {
            audioSource.PlayOneShot(voiceLine);
            Debug.Log("Playing voice: " + voiceLine.name);
        }
        else
        {
            Debug.LogWarning("No voiceLine assigned to " + gameObject.name);
        }

        // Optional: trigger animation
        Animator anim = GetComponent<Animator>();
        if (anim != null && !string.IsNullOrEmpty(disappearTriggerName))
        {
            anim.SetTrigger(disappearTriggerName);
        }

        // Calculate destroy delay
        float wait = delayBeforeDestroy;
        if (wait <= 0 && voiceLine != null)
            wait = voiceLine.length;
        else if (wait <= 0)
            wait = 0.5f; // fallback

        Destroy(gameObject, wait);
        Debug.Log($"{gameObject.name} will be destroyed in {wait} seconds");
    }
}