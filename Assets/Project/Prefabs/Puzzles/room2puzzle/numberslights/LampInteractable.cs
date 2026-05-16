using UnityEngine;

[RequireComponent(typeof(Light))]
[RequireComponent(typeof(AudioSource))]
public class LampInteractable : MonoBehaviour, IInteractable
{
    [Header("Lamp Settings")]
    public bool isOnByDefault = false;
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

        puzzleManager = FindObjectOfType<LampPuzzleManager>();
        if (puzzleManager == null)
            Debug.LogError("LampPuzzleManager not found in scene!");
        else
            Debug.Log($"{name}: Found puzzle manager.");
    }

    public void Interact()
    {
        Debug.Log($"{name}: Interact() called. Current isOn={isOn}");
        isOn = !isOn;
        lampLight.enabled = isOn;
        Debug.Log($"{name}: Toggled to {isOn}");

        if (switchSound != null)
            audioSource.PlayOneShot(switchSound);

        if (puzzleManager != null)
        {
            Debug.Log($"{name}: Notifying manager with turnedOn={isOn}");
            puzzleManager.OnLampToggled(this, isOn);
        }
        else
            Debug.LogError($"{name}: puzzleManager is null!");
    }

    public void ForceSetState(bool state)
    {
        isOn = state;
        lampLight.enabled = isOn;
        Debug.Log($"{name}: ForceSetState to {state}");
    }

    public bool IsOn() => isOn;
}