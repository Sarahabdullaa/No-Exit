using UnityEngine;

public class CollectiblePiece : MonoBehaviour
{
    public InventoryItem inventoryItem;

    public AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Trigger inventory
            if (inventoryItem != null)
            {
                inventoryItem.PickupItem();
            }

            // Play sound
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            // Destroy object
            Destroy(gameObject);
        }
    }
}