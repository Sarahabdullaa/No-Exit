using UnityEngine;

public class CollectibleItem : MonoBehaviour, IInteractable
{
    public string itemName;

    public void Interact()
    {
        Debug.Log(itemName + " collected!");
        // Add to inventory logic here later
        Destroy(gameObject); // Remove the item from the world
    }
}