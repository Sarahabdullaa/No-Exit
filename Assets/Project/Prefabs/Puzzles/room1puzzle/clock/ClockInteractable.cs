using UnityEngine;

public class ClockInteractable : MonoBehaviour, IInteractable
{
    [Header("Hands & Rotation")]
    public Transform hourHand;
    public Transform minuteHand;
    public float targetHourAngle = 30f;      // e.g., 1 o'clock = 30°
    public float targetMinuteAngle = 0f;     // e.g., 12 o'clock = 0°
    public float angleTolerance = 5f;
    public float rotationSensitivity = 100f;

    [Header("Camera Setup")]
    public Camera clockCamera;
    public Camera playerCamera;

    [Header("Audio")]
    public AudioClip successSound;
    private AudioSource audioSource;

    [Header("Player References")]
    public GameObject player;
    private MouseLook mouseLook;
    private PlayerMovement playerMovement;

    private bool isInteracting = false;
    private bool hasTriggered = false;
    private int selectedHand = 0;   // 0 = minute, 1 = hour

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Find player by tag
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        // Find mouseLook (on camera)
        if (mouseLook == null)
        {
            if (Camera.main != null)
                mouseLook = Camera.main.GetComponent<MouseLook>();
            if (mouseLook == null && player != null)
                mouseLook = player.GetComponentInChildren<MouseLook>();
        }

        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
            if (mouseLook == null) Debug.LogError("MouseLook not found!");
            if (playerMovement == null) Debug.LogError("PlayerMovement not found!");
        }
        else
        {
            Debug.LogError("Player not found! Tag it as 'Player'.");
        }

        // Auto-assign cameras
        if (playerCamera == null)
            playerCamera = Camera.main;

        if (clockCamera == null)
            clockCamera = GetComponentInChildren<Camera>();

        if (clockCamera != null)
            clockCamera.enabled = false;
    }

    void Update()
    {
        if (!isInteracting) return;

        // Rotate selected hand with mouse X
        float mouseX = Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
        if (mouseX != 0f)
        {
            Transform hand = (selectedHand == 0) ? minuteHand : hourHand;
            Vector3 rot = hand.localEulerAngles;
            rot.y += mouseX;
            hand.localEulerAngles = rot;
        }

        // Switch hand with Tab key
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            selectedHand = 1 - selectedHand; // toggle between 0 and 1
            Debug.Log("Now controlling: " + (selectedHand == 0 ? "Minute Hand" : "Hour Hand"));
        }

        // Check if both hands are within tolerance of their target angles
        if (!hasTriggered)
        {
            float minuteAngle = NormalizeAngle(minuteHand.localEulerAngles.y);
            float hourAngle = NormalizeAngle(hourHand.localEulerAngles.y);
            float targetMin = NormalizeAngle(targetMinuteAngle);
            float targetHour = NormalizeAngle(targetHourAngle);

            if (Mathf.Abs(minuteAngle - targetMin) <= angleTolerance &&
                Mathf.Abs(hourAngle - targetHour) <= angleTolerance)
            {
                hasTriggered = true;
                OnTargetReached();
            }
        }

        // Exit interaction
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
        {
            ExitInteraction();
        }
    }

    private void OnTargetReached()
    {
        if (successSound != null)
            audioSource.PlayOneShot(successSound);
        Debug.Log("Clock puzzle solved! Both hands at correct positions.");
    }

    private float NormalizeAngle(float angle)
    {
        angle = angle % 360f;
        if (angle > 180f) angle -= 360f;
        return angle;
    }

    public void Interact()
    {
        if (!isInteracting)
            EnterInteraction();
        else
            ExitInteraction();
    }

    private void EnterInteraction()
    {
        Debug.Log("EnterInteraction started");
        if (mouseLook == null) Debug.LogError("mouseLook is null");
        if (playerMovement == null) Debug.LogError("playerMovement is null");
        if (playerCamera == null) Debug.LogError("playerCamera is null");
        if (clockCamera == null) Debug.LogError("clockCamera is null");

        isInteracting = true;
        hasTriggered = false;
        selectedHand = 0; // start with minute hand

        // Disable player controls
        mouseLook.enabled = false;
        playerMovement.enabled = false;

        // Switch cameras
        if (playerCamera != null)
            playerCamera.enabled = false;
        if (clockCamera != null)
            clockCamera.enabled = true;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void ExitInteraction()
    {
        if (!isInteracting) return;
        isInteracting = false;

        if (clockCamera != null) clockCamera.enabled = false;
        if (playerCamera != null) playerCamera.enabled = true;

        mouseLook.enabled = true;
        playerMovement.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}