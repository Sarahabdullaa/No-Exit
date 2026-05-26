using UnityEngine;

public class StarPuzzleManager : MonoBehaviour
{
    [Header("Star Count")]
    public int totalStars = 5;          // set to number of stars in room
    private int starsDestroyed = 0;

    [Header("Reward")]
    public GameObject puzzleReward;      // disabled reward object

    [Header("Audio")]
    public AudioClip puzzleCompleteSound;

    [Header("Gun Reference")]
    public GunController gunController;  // drag player's GunController here

    private bool puzzleCompleted = false;

    public void StarDestroyed()
    {
        if (puzzleCompleted) return;
        starsDestroyed++;
        Debug.Log($"Stars destroyed: {starsDestroyed}/{totalStars}");

        if (starsDestroyed >= totalStars)
        {
            CompletePuzzle();
        }
    }

    void CompletePuzzle()
    {
        puzzleCompleted = true;
        Debug.Log("All stars destroyed! Puzzle complete.");
        PuzzleProgress.StarCompleted = true;
        if (puzzleCompleteSound != null)
            AudioSource.PlayClipAtPoint(puzzleCompleteSound, Camera.main.transform.position);

        // Unequip the gun
        if (gunController != null)
            gunController.UnequipGun();

        // Reveal reward
        if (puzzleReward != null)
            puzzleReward.SetActive(true);
        else
            Debug.LogWarning("No puzzleReward assigned in StarPuzzleManager!");
    }
}