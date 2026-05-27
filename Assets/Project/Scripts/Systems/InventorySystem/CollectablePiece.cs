using UnityEngine;

public class CollectiblePiece : MonoBehaviour
{
    public InventoryItem inventoryItem;
    public AudioClip pickupSound;
    public MoldSlot moldSlot;           // drag the corresponding mold slot here
    private bool isCollected = false;

    private Collider pieceCollider;
    private MeshRenderer pieceRenderer;

    void Start()
    {
        pieceCollider = GetComponent<Collider>();
        pieceRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCollected) return;
        if (!other.CompareTag("Player")) return;

        isCollected = true;

        // Add to inventory (UI)
        if (inventoryItem != null)
            inventoryItem.PickupItem();

        // Play sound
        if (pickupSound != null)
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

        // Disable visuals and collision
        if (pieceRenderer != null) pieceRenderer.enabled = false;
        if (pieceCollider != null) pieceCollider.enabled = false;

        // Notify the mold slot that this piece has been collected
        if (moldSlot != null)
            moldSlot.OnPieceCollected();
        else
            Debug.LogWarning($"{name} has no MoldSlot assigned!");
    }
}