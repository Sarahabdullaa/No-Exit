using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Gun Settings")]
    public Transform gunHoldPoint;
    public float shootRange = 20f;
    public LayerMask starLayer;
    public AudioClip shootSound;
    [Range(0f, 1f)] public float shootVolume = 1f;
    public AudioClip equipMusic;      // one?shot epic sound on pickup
    public AudioClip unequipSound;

    [Header("Looping Music")]
    public AudioClip equipLoopMusic;  // drag your looping track here
    [Range(0f, 1f)] public float loopVolume = 0.5f;

    private ToyGun currentGun;
    private bool isGunEquipped = false;
    private AudioSource audioSource;   // for one?shot sounds
    private AudioSource loopSource;    // for looping music

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

        // Create a separate AudioSource for the looping music
        loopSource = gameObject.AddComponent<AudioSource>();
        loopSource.loop = true;
        loopSource.volume = loopVolume;
        loopSource.playOnAwake = false;
    }

    public void EquipGun(ToyGun gun)
    {
        if (isGunEquipped) return;

        currentGun = gun;
        currentGun.transform.SetParent(gunHoldPoint);
        currentGun.transform.localPosition = Vector3.zero;
        currentGun.transform.localRotation = Quaternion.identity;

        // Play one?shot equip sound
        if (equipMusic != null)
            audioSource.PlayOneShot(equipMusic);

        // Start looping music
        if (equipLoopMusic != null)
        {
            loopSource.clip = equipLoopMusic;
            loopSource.Play();
        }

        isGunEquipped = true;
        Debug.Log("Gun equipped!");
    }

    public void UnequipGun()
    {
        if (!isGunEquipped) return;

        // Stop looping music
        if (loopSource.isPlaying)
            loopSource.Stop();

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
                AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootVolume);

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