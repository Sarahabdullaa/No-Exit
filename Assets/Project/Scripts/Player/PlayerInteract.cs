using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 3f;

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Ray ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
            {
                // DOOR
                if (hit.collider.CompareTag("Door"))
                {
                    DoorScript.Door door = hit.collider.GetComponent<DoorScript.Door>();

                    if (door != null)
                    {
                        door.OpenDoor();
                    }
                }

                // CHEST
                if (hit.collider.CompareTag("Chest"))
                {
                    ChestOpen chest = hit.collider.GetComponent<ChestOpen>();

                    if (chest != null)
                    {
                        chest.OpenChest();
                    }
                }

                if (hit.collider.CompareTag("Drawer"))
                {
                    // Use InParent because the collider is on the child mesh!
                    DrawerOpen drawer = hit.collider.GetComponentInParent<DrawerOpen>();

                    if (drawer != null)
                    {
                        drawer.ToggleDrawer();
                    }
                }
            }
        }
    }
}