using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Gun Settings")]
    public Transform gunHoldPoint; // Empty GameObject child of camera (where gun sits)
    public float shootRange = 20f;
    public LayerMask starLayer; // Layer for stars (e.g., "Star")
    public AudioClip shootSound;
    public AudioClip noAmmoSound; // Optional

    private GameObject currentGunModel;
    private bool isGunEquipped = false;

    void Start()
    {
        // Create a hold point if not assigned (e.g., a child transform at camera's bottom right)
        if (gunHoldPoint == null)
        {
            GameObject hold = new GameObject("GunHoldPoint");
            hold.transform.SetParent(Camera.main.transform);
            hold.transform.localPosition = new Vector3(0.4f, -0.3f, 0.5f); // Adjust as needed
            hold.transform.localRotation = Quaternion.Euler(0, -45, 0);
            gunHoldPoint = hold.transform;
        }
    }

    public void EquipGun(GameObject gunModelPrefab)
    {
        if (isGunEquipped) return;

        // Instantiate gun model as child of hold point
        currentGunModel = Instantiate(gunModelPrefab, gunHoldPoint.position, gunHoldPoint.rotation);
        currentGunModel.transform.SetParent(gunHoldPoint);
        isGunEquipped = true;
        Debug.Log("Gun equipped!");
    }

    void Update()
    {
        // Only allow shooting if equipped and E key pressed
        if (isGunEquipped && Input.GetKeyDown(KeyCode.E))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Play sound
        if (shootSound != null)
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position);

        // Raycast from center of screen
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, shootRange, starLayer))
        {
            Star star = hit.collider.GetComponent<Star>();
            if (star != null)
            {
                star.DestroyStar();
                Debug.Log("Shot a star!");
            }
        }
        else
        {
            Debug.Log("Missed!");
        }
    }
}
