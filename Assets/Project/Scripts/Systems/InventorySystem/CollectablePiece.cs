using UnityEngine;

public class CollectiblePiece : MonoBehaviour
{
    public string pieceName = "Strange Gear";
    public AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add to inventory
            if (InventoryManager.Instance != null)
                InventoryManager.Instance.AddItem(pieceName);
            else
                Debug.Log("InventoryManager missing – item not added");

            // Play sound
            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            // Destroy the visual object
            Destroy(gameObject);
        }
    }
}