using UnityEngine;

public class ToyGun : MonoBehaviour, IInteractable
{
    public AudioClip pickupSound;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform originalParent;
    private bool isEquipped = false;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalParent = transform.parent;
    }

    public void Interact()
    {
        if (isEquipped) return;

        GunController gunController = FindObjectOfType<GunController>();
        if (gunController != null)
        {
            gunController.EquipGun(this);
            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            isEquipped = true;
        }
    }

    public void ReturnToOriginalPosition()
    {
        transform.SetParent(originalParent);
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        isEquipped = false;
    }

    public void SetEquipped(bool equipped)
    {
        isEquipped = equipped;
    }
}