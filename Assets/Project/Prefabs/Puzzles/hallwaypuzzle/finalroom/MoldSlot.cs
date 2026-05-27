using UnityEngine;
using System.Collections;

public class MoldSlot : MonoBehaviour, IInteractable
{
    [Header("Visual Piece")]
    public GameObject visualPiece;     // child object that will be enabled when piece is placed
    public AudioClip placeSound;

    [Header("Puzzle Solved UI")]
    public GameObject puzzleSolvedPanel;
    public float panelDuration = 2f;

    private bool isPlaced = false;
    private AudioSource audioSource;

    void Start()
    {
        if (visualPiece != null)
            visualPiece.SetActive(false);

        if (puzzleSolvedPanel != null)
            puzzleSolvedPanel.SetActive(false);


        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && placeSound != null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Called by the collectible piece when it is collected (triggered)
    public void OnPieceCollected()
    {
        if (isPlaced) return;

        // Enable the visual piece
        if (visualPiece != null)
            visualPiece.SetActive(true);

        isPlaced = true;

        if (placeSound != null && audioSource != null)
            audioSource.PlayOneShot(placeSound);

        // Show puzzle solved panel
        if (puzzleSolvedPanel != null)
            StartCoroutine(ShowPuzzleSolved());


        Debug.Log($"Piece placed in slot {name}");







    }
    IEnumerator ShowPuzzleSolved()
    {
        puzzleSolvedPanel.SetActive(true);

        yield return new WaitForSeconds(panelDuration);

        puzzleSolvedPanel.SetActive(false);
    }

    // Optional: direct interaction (press E on the slot itself) – not strictly needed,
    // but kept for compatibility. You can remove if not needed.
    public void Interact()
    {
        Debug.Log($"Slot {name} is not interactive – pieces are placed automatically when collected.");
    }
    public bool IsPlaced()
    {
        return isPlaced;
    }


}