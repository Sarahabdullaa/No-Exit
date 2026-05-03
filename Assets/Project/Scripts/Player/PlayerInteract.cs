using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 5f; // Reach distance
    public LayerMask interactableLayer; // What can we hit?
    public Transform cameraTransform;

    void Update()
    {
        // Detect the E key press
        if (Input.GetKeyDown(KeyCode.E))
        {
            PerformRaycast();
        }
    }

    void PerformRaycast()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, interactableLayer))
        {
            // Check if the object we hit has an IInteractable script
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact(); // This opens the door or picks up the item!
            }
        }
    }
}