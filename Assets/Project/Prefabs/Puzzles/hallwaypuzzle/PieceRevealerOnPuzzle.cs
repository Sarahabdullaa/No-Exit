using UnityEngine;

public class PieceRevealerOnPuzzle : MonoBehaviour
{
    [Header("Puzzle Manager (drag here)")]
    public LampPuzzleManager puzzleManager;   // Drag your LampPuzzleManager object here

    [Header("Optional")]
    public bool enablePhysicsOnReveal = true; // If the piece should fall

    private bool hasRevealed = false;

    void Start()
    {
        // If you forgot to drag the manager, try to find it automatically
        if (puzzleManager == null)
            puzzleManager = FindObjectOfType<LampPuzzleManager>();

        if (puzzleManager == null)
            Debug.LogError("PieceRevealer: No LampPuzzleManager found!");
    }

    void Update()
    {
        if (hasRevealed) return;
        if (puzzleManager == null) return;

        // Check if the puzzle is completed
        if (puzzleManager.IsPuzzleCompleted)
        {
            Reveal();
        }
    }

    void Reveal()
    {
        hasRevealed = true;

        // Enable the piece (it was probably disabled in the Inspector)
        gameObject.SetActive(true);

        // If you want it to fall
        if (enablePhysicsOnReveal)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false;
        }

        Debug.Log("Puzzle reward revealed!");
        // Optional: disable this script so it stops checking
        enabled = false;
    }
}