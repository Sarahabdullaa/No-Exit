using UnityEngine;

public class MoldPuzzleManager : MonoBehaviour
{
    private MoldSlot[] allSlots;
    private bool completed = false;

    void Start()
    {
        allSlots = GetComponentsInChildren<MoldSlot>();
        Debug.Log($"Found {allSlots.Length} mold slots.");
    }

    void Update()
    {
        if (completed) return;

        int filledCount = 0;
        foreach (MoldSlot slot in allSlots)
        {
            if (slot.IsPlaced()) filledCount++;
        }

        if (filledCount >= allSlots.Length)
        {
            completed = true;
            PuzzleProgress.MoldCompleted = true;
            Debug.Log("Mold puzzle complete – door unlocked!");
            // Door will now respond to requiredPuzzle = "mold"
        }
    }
}