using UnityEngine;

public class RewardRevealer : MonoBehaviour
{

    
    [Header("Piece to Reveal")]
    public GameObject hiddenPiece;   // The jigsaw piece (disabled at start)

    [Header("Physics")]
    public bool enablePhysicsOnReveal = true;  // Make it fall?

    [Header("Audio")]
    public AudioClip revealSound;

    public void RevealPiece()
    {
        if (hiddenPiece == null)
        {
            Debug.LogError("RewardRevealer: hiddenPiece is not assigned!");
            return;
        }

        hiddenPiece.SetActive(true);
        Debug.Log("RewardRevealer: Piece revealed!");

        if (enablePhysicsOnReveal)
        {
            Rigidbody rb = hiddenPiece.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = false;
        }

        if (revealSound != null)
            AudioSource.PlayClipAtPoint(revealSound, hiddenPiece.transform.position);
    }
}