using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Gun Settings")]
    public Transform gunHoldPoint;
    public float shootRange = 20f;
    public LayerMask starLayer;
    public AudioClip shootSound;
    public AudioClip equipMusic;
    public AudioClip unequipSound;

    private ToyGun currentGun;
    private bool isGunEquipped = false;
    private AudioSource audioSource;

    void Start()
    {
        if (gunHoldPoint == null)
        {
            GameObject hold = new GameObject("GunHoldPoint");
            hold.transform.SetParent(Camera.main.transform);
            hold.transform.localPosition = new Vector3(0.4f, -0.3f, 0.5f);
            hold.transform.localRotation = Quaternion.Euler(0, -45, 0);
            gunHoldPoint = hold.transform;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void EquipGun(ToyGun gun)
    {
        if (isGunEquipped) return;

        currentGun = gun;
        // Parent the gun to the hold point and reset local transform
        currentGun.transform.SetParent(gunHoldPoint);
        currentGun.transform.localPosition = Vector3.zero;
        currentGun.transform.localRotation = Quaternion.identity;

        if (equipMusic != null)
            audioSource.PlayOneShot(equipMusic);

        isGunEquipped = true;
        Debug.Log("Gun equipped!");
    }

    public void UnequipGun()
    {
        if (!isGunEquipped) return;

        if (currentGun != null)
            currentGun.ReturnToOriginalPosition();

        if (unequipSound != null)
            audioSource.PlayOneShot(unequipSound);

        isGunEquipped = false;
        Debug.Log("Gun returned to original spot.");
    }

    void Update()
    {
        if (isGunEquipped && Input.GetKeyDown(KeyCode.E))
            Shoot();

        if (isGunEquipped && Input.GetKeyDown(KeyCode.DownArrow))
            UnequipGun();
    }

    void Shoot()
    {
        if (shootSound != null)
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position);

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, shootRange, starLayer))
        {
            Star star = hit.collider.GetComponent<Star>();
            if (star != null)
                star.DestroyStar();
        }
        else
        {
            Debug.Log("Missed!");
        }
    }
}