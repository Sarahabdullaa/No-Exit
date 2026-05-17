using System.Collections.Generic;
using UnityEngine;

public class LampPuzzleManager : MonoBehaviour
{

    public bool IsPuzzleCompleted => puzzleCompleted;   // Read-only property


    public RewardRevealer rewardRevealer;   
    [Header("Correct Sequence")]
    public LampInteractable[] correctOrder;

    [Header("Reset Effect")]
    public float flickerDuration = 0.5f;
    public float flickerInterval = 0.1f;

    [Header("Audio")]
    public AudioClip successSound;
    public AudioClip resetSound;

    private List<LampInteractable> turnedOnOrder = new List<LampInteractable>();
    private bool puzzleCompleted = false;
    private List<LampInteractable> allLamps = new List<LampInteractable>();
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        allLamps.AddRange(FindObjectsOfType<LampInteractable>());
        Debug.Log($"Found {allLamps.Count} lamps.");

        if (correctOrder.Length == 0)
            Debug.LogError("Correct order not set in LampPuzzleManager!");
        else
        {
            Debug.Log("Correct order:");
            for (int i = 0; i < correctOrder.Length; i++)
                Debug.Log($"  {i + 1}: {correctOrder[i]?.name}");
        }
    }

    public void OnLampToggled(LampInteractable lamp, bool turnedOn)
    {
        if (puzzleCompleted) return;

        if (!turnedOn)
        {
            // Turning off any lamp resets the whole puzzle
            Debug.Log("Lamp turned off ? resetting puzzle");
            ResetPuzzle();
            return;
        }

        // Lamp turned on
        if (turnedOnOrder.Contains(lamp))
            return; // already on, ignore

        turnedOnOrder.Add(lamp);
        Debug.Log($"Turned on: {lamp.name}. Order: {string.Join(" -> ", turnedOnOrder.ConvertAll(l => l.name))}");

        // Check if all lamps are now on
        if (turnedOnOrder.Count == correctOrder.Length)
        {
            // Compare the sequence
            bool correct = true;
            for (int i = 0; i < correctOrder.Length; i++)
            {
                if (turnedOnOrder[i] != correctOrder[i])
                {
                    correct = false;
                    break;
                }
            }

            if (correct)
            {
                CompletePuzzle();
            }
            else
            {
                Debug.Log("Wrong sequence! Resetting puzzle.");
                ResetPuzzle();
            }
        }
    }

    void ResetPuzzle()
    {
        Debug.Log("ResetPuzzle called");
        if (resetSound != null)
            audioSource.PlayOneShot(resetSound);

        StopAllCoroutines();
        StartCoroutine(FlickerAndReset());
    }

    System.Collections.IEnumerator FlickerAndReset()
    {
        // Flicker all lamps
        float endTime = Time.time + flickerDuration;
        bool flickerState = false;
        while (Time.time < endTime)
        {
            flickerState = !flickerState;
            SetAllLampsState(flickerState);
            yield return new WaitForSeconds(flickerInterval);
        }

        // Turn all lamps off (default state)
        foreach (LampInteractable lamp in allLamps)
        {
            lamp.ForceSetState(false);
        }

        // Clear the sequence
        turnedOnOrder.Clear();
        Debug.Log("Reset complete. All lamps off, order cleared.");
    }

    void SetAllLampsState(bool state)
    {
        foreach (LampInteractable lamp in allLamps)
            lamp.ForceSetState(state);
    }

    void CompletePuzzle()
    {
        puzzleCompleted = true;
        Debug.Log("??? PUZZLE COMPLETE! ???");
        if (successSound != null)
            audioSource.PlayOneShot(successSound);
        // Optionally disable further lamp interactions
        // foreach (var lamp in allLamps) lamp.enabled = false;
           

    }
}