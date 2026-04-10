using UnityEngine;
using UnityEngine.InputSystem; // The modern system

public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 3f;

    void Update()
    {
        // Check if the 'E' key was pressed this frame using the New System
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            // Shoot a ray forward from the center of the camera
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
            {
                if (hit.collider.CompareTag("Door"))
                {
                 

                    // Look for the Door script on the object we hit
                    DoorScript.Door door = hit.collider.GetComponent<DoorScript.Door>();

                    if (door != null)
                    {
                        door.OpenDoor(); // This triggers your smooth rotation and sound
                    }
                }
            }
        }
    }
}
