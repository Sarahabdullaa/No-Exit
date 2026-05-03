using UnityEngine;

public class ClockInteractable : MonoBehaviour, IInteractable
{
    [Header("Hand & Rotation")]
    public Transform handTransform;
    public float targetAngleY = 30f;
    public float angleTolerance = 5f;
    public float rotationSensitivity = 100f;

    [Header("Camera Setup")]
    public Camera clockCamera;           // Drag the ClockCamera here
    public Camera playerCamera;          // Drag the player's main camera here

    [Header("Audio")]
    public AudioClip successSound;
    private AudioSource audioSource;

    [Header("Player References")]
    public GameObject player;
    private MouseLook mouseLook;
    private PlayerMovement playerMovement;

    private bool isInteracting = false;
    private bool hasTriggered = false;
    private float currentHandY = 0f;

    void Start()
{
    audioSource = GetComponent<AudioSource>();
    if (audioSource == null)
        audioSource = gameObject.AddComponent<AudioSource>();

    // Find the player automatically by tag
    if (player == null)
        player = GameObject.FindGameObjectWithTag("Player");
        // Auto-find mouseLook if not assigned (try camera)
        if (mouseLook == null)
        {
            // Check main camera first
            if (Camera.main != null)
                mouseLook = Camera.main.GetComponent<MouseLook>();
            // If not there, search player's children
            if (mouseLook == null && player != null)
                mouseLook = player.GetComponentInChildren<MouseLook>();
        }
        if (player != null)
    {
       // mouseLook = player.GetComponent<MouseLook>();
        playerMovement = player.GetComponent<PlayerMovement>();
        if (mouseLook == null) Debug.LogError("MouseLook script not found on Player");
        if (playerMovement == null) Debug.LogError("PlayerMovement script not found on Player");
    }
    else
    {
        Debug.LogError("No GameObject with tag 'Player' found. Please assign player manually.");
    }

    // Auto‑assign cameras if missing
    if (playerCamera == null)
        playerCamera = Camera.main;
    
    if (clockCamera == null && transform != null)
        clockCamera = GetComponentInChildren<Camera>();
    
    if (clockCamera != null)
        clockCamera.enabled = false;
}

    void Update()
    {
        if (!isInteracting) return;

        // Rotate hand with mouse movement
        float mouseX = Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
        if (mouseX != 0f)
        {
            Vector3 rot = handTransform.localEulerAngles;
            rot.y += mouseX;
            handTransform.localEulerAngles = rot;
        }

        // Check target angle
        if (!hasTriggered)
        {
            currentHandY = handTransform.localEulerAngles.y;
            float normalized = NormalizeAngle(currentHandY);
            float targetNorm = NormalizeAngle(targetAngleY);
            if (Mathf.Abs(normalized - targetNorm) <= angleTolerance)
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
        Debug.Log("Target reached: " + targetAngleY);
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


        Debug.Log("EnterInteraction called");
        isInteracting = true;
        hasTriggered = false;

        // Disable player controls
        mouseLook.enabled = false;
        playerMovement.enabled = false;
        Debug.Log("Player controls disabled");

        // Switch cameras
        if (playerCamera != null)
        {
            playerCamera.enabled = false;
            Debug.Log("Player camera disabled: " + playerCamera.name);
        }
        else
        {
            Debug.LogError("playerCamera is NULL! Assign it in Inspector.");
        }

        if (clockCamera != null)
        {
            clockCamera.enabled = true;
            Debug.Log("Clock camera enabled: " + clockCamera.name);
        }
        else
        {
            Debug.LogError("clockCamera is NULL! Assign it in Inspector.");
        }

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void ExitInteraction()
    {
        Debug.Log("ExitInteraction called");
        if (!isInteracting) return;

        isInteracting = false;

        // Switch back
        if (clockCamera != null) clockCamera.enabled = false;
        if (playerCamera != null) playerCamera.enabled = true;

        // Re-enable player controls
        mouseLook.enabled = true;
        playerMovement.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}