using UnityEngine;

[RequireComponent(typeof(Light))]
[RequireComponent(typeof(AudioSource))]
public class LampInteractable : MonoBehaviour, IInteractable
{
    [Header("Lamp Settings")]
    public bool isOnByDefault = false;  // Set per lamp in Inspector
    [Header("Audio")]
    public AudioClip switchSound;

    private Light lampLight;
    private AudioSource audioSource;
    private LampPuzzleManager puzzleManager;
    private bool isOn;

    void Start()
    {
        lampLight = GetComponent<Light>();
        audioSource = GetComponent<AudioSource>();
        isOn = isOnByDefault;
        lampLight.enabled = isOn;

        // Find the puzzle manager in the scene (assume one exists)
        puzzleManager = FindObjectOfType<LampPuzzleManager>();
        if (puzzleManager == null)
            Debug.LogError("LampPuzzleManager not found in scene!");
    }

    public void Interact()
    {
        // Toggle the lamp
        isOn = !isOn;
        lampLight.enabled = isOn;

        // Play sound
        if (switchSound != null)
            audioSource.PlayOneShot(switchSound);

        // Notify the puzzle manager about this lamp's new state
        if (puzzleManager != null)
            puzzleManager.OnLampToggled(this, isOn);
    }

    public void ForceSetState(bool state)
    {
        isOn = state;
        lampLight.enabled = isOn;
    }

    public bool IsOn() => isOn;
}