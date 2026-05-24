using UnityEngine;

public class ToyGun : MonoBehaviour, IInteractable
{
    public GameObject gunModel;           // The 3D model of the gun (child or reference)
    public AudioClip pickupSound;

    public void Interact()
    {
        // Find player's GunController
        GunController gunController = FindObjectOfType<GunController>();
        if (gunController != null)
        {
            gunController.EquipGun(gunModel);
            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            Destroy(gameObject);          // Remove the world gun
        }
        else
        {
            Debug.LogError("GunController not found on player!");
        }
    }
}