using System.Collections.Generic;
using UnityEngine;

public class LampPuzzleManager : MonoBehaviour
{
    [Header("Correct Sequence")]
    public LampInteractable[] correctOrder;  // Drag lamps in the correct order (e.g. short, middle, high)

    [Header("Reset Effect")]
    public float flickerDuration = 0.5f;
    public float flickerInterval = 0.1f;

    [Header("Audio")]
    public AudioClip successSound;
    public AudioClip resetSound;

    private int currentStep = 0;          // How many lamps correctly turned on so far
    private bool puzzleCompleted = false;
    private List<LampInteractable> allLamps = new List<LampInteractable>();
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Collect all lamps in the scene (or assign manually)
        allLamps.AddRange(FindObjectsOfType<LampInteractable>());

        // Optional: verify that correctOrder array matches the number of lamps
        if (correctOrder.Length == 0)
            Debug.LogError("Correct order not set in LampPuzzleManager!");
    }

    public void OnLampToggled(LampInteractable lamp, bool turnedOn)
    {
        if (puzzleCompleted) return;

        // If turning off a lamp, it's always a mistake (or we could allow? Usually puzzle requires turning on in order)
        if (!turnedOn)
        {
            ResetPuzzle();
            return;
        }

        // Check if this lamp is the expected next one in the sequence
        if (currentStep < correctOrder.Length && lamp == correctOrder[currentStep])
        {
            // Correct step
            currentStep++;
            Debug.Log($"Correct! Step {currentStep}/{correctOrder.Length}");

            if (currentStep >= correctOrder.Length)
            {
                CompletePuzzle();
            }
        }
        else
        {
            // Wrong lamp turned on – reset puzzle
            ResetPuzzle();
        }
    }

    void ResetPuzzle()
    {
        Debug.Log("Wrong order! Resetting lamps.");

        // Play reset sound
        if (resetSound != null)
            audioSource.PlayOneShot(resetSound);

        // Start flicker effect on all lamps
        StartCoroutine(FlickerAndReset());
    }

    System.Collections.IEnumerator FlickerAndReset()
    {
        // Store original states (which are currently wrong) – we'll reset to original default later
        // But the requirement: "reset to the normal status" – normal status = initial states (high on, others off)
        // We'll restore based on each lamp's isOnByDefault.

        // Flicker: rapidly toggle lights
        float endTime = Time.time + flickerDuration;
        bool flickerState = false;
        while (Time.time < endTime)
        {
            flickerState = !flickerState;
            SetAllLampsState(flickerState);
            yield return new WaitForSeconds(flickerInterval);
        }

        // Restore to default states
        foreach (LampInteractable lamp in allLamps)
        {
            lamp.ForceSetState(lamp.isOnByDefault);
        }

        // Reset step counter
        currentStep = 0;
    }

    void SetAllLampsState(bool state)
    {
        foreach (LampInteractable lamp in allLamps)
        {
            lamp.ForceSetState(state);
        }
    }

    void CompletePuzzle()
    {
        puzzleCompleted = true;
        Debug.Log("Puzzle complete!");
        if (successSound != null)
            audioSource.PlayOneShot(successSound);

        // Optionally disable further interaction on lamps
        foreach (LampInteractable lamp in allLamps)
        {
            // You can remove the interactable component or just ignore toggles via puzzleCompleted flag
            // For now, we just prevent toggles because puzzleCompleted = true
        }
    }
}