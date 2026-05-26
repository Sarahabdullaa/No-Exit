using UnityEngine;

public class ClockInteractable : MonoBehaviour, IInteractable
{
    [Header("Hands & Rotation")]
    public Transform hourHand;
    public Transform minuteHand;
    public float targetHourAngle = 30f;
    public float targetMinuteAngle = 0f;
    public float angleTolerance = 5f;
    public float rotationSpeed = 50f;

    [Header("Camera Setup")]
    public Camera clockCamera;

    [Header("Audio")]
    public AudioClip successSound;
    public AudioClip tickSound;                // drag a short tick sound
    public float tickInterval = 0.15f;
    public GameObject puzzleReward;   // drag the reward object here (disabled)
    private AudioSource audioSource;
    private float lastTickTime = 0f;

    private GameObject player;
    private MouseLook mouseLook;
    private PlayerMovement playerMovement;
    private PlayerFootsteps playerFootsteps;
    private AudioSource footstepAudioSource;   // to mute/stop footsteps
    private Camera playerCamera;

    private bool isInteracting = false;
    private bool hasTriggered = false;
    private int selectedHand = 0;
    private bool puzzleCompleted = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        if (clockCamera == null) clockCamera = GetComponentInChildren<Camera>();
        if (clockCamera != null) clockCamera.enabled = false;
    }

    void Update()
    {
        if (!isInteracting) return;

        float rotate = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) rotate = -rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.RightArrow)) rotate = rotationSpeed * Time.deltaTime;

        if (rotate != 0f)
        {
            Transform hand = (selectedHand == 0) ? minuteHand : hourHand;
            Vector3 rot = hand.localEulerAngles;
            rot.y += rotate;
            hand.localEulerAngles = rot;

            // Play tick sound at intervals
            if (tickSound != null && Time.time - lastTickTime >= tickInterval)
            {
                audioSource.PlayOneShot(tickSound);
                lastTickTime = Time.time;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            selectedHand = 1 - selectedHand;
            Debug.Log("Now controlling: " + (selectedHand == 0 ? "Minute Hand" : "Hour Hand"));
        }

        if (!hasTriggered)
        {
            float minuteAngle = NormalizeAngle(minuteHand.localEulerAngles.y);
            float hourAngle = NormalizeAngle(hourHand.localEulerAngles.y);
            if (Mathf.Abs(minuteAngle - targetMinuteAngle) <= angleTolerance &&
                Mathf.Abs(hourAngle - targetHourAngle) <= angleTolerance)
            {
                hasTriggered = true;
                OnTargetReached();
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                ExitInteraction();
    }

    private void OnTargetReached()
    {
        if (successSound != null) audioSource.PlayOneShot(successSound);
        Debug.Log("Clock puzzle solved!");
        puzzleCompleted = true;          // FIX: mark as completed
        ExitInteraction();
        if (puzzleReward != null)
        {
          
            // Optional: play a sound to signal reward appears
            if (successSound != null)
                AudioSource.PlayClipAtPoint(successSound, puzzleReward.transform.position);
            if (puzzleReward != null)
            {
                puzzleReward.SetActive(true);
                Debug.Log("Reward activated at: " + puzzleReward.transform.position);
            }
            else
            {
                Debug.LogWarning("puzzleReward not assigned in ClockInteractable!");
            }
        }
        PuzzleProgress.ClockCompleted = true;
    }

    private float NormalizeAngle(float angle)
    {
        angle = angle % 360f;
        if (angle > 180f) angle -= 360f;
        return angle;
    }

    public void Interact()
    {
        if (puzzleCompleted)           
        {
            Debug.Log("Puzzle already solved!");
            return;
        }

        if (!isInteracting) EnterInteraction();
        else ExitInteraction();
    }

    private void FindPlayerReferences()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        if (mouseLook == null && player != null) mouseLook = player.GetComponentInChildren<MouseLook>();
        if (playerMovement == null && player != null) playerMovement = player.GetComponent<PlayerMovement>();
        if (playerFootsteps == null && player != null) playerFootsteps = player.GetComponent<PlayerFootsteps>();
        if (footstepAudioSource == null && playerFootsteps != null) footstepAudioSource = playerFootsteps.GetComponent<AudioSource>();
        if (playerCamera == null) playerCamera = Camera.main;
    }

    private void EnterInteraction()
    {

        if (puzzleCompleted) return;
        FindPlayerReferences();
        if (mouseLook == null || playerMovement == null || playerCamera == null || clockCamera == null)
        {
            Debug.LogError("ClockInteractable: missing references – cannot enter");
            return;
        }
        if (playerFootsteps != null) playerFootsteps.canPlay = false;
        isInteracting = true;
        hasTriggered = false;
        selectedHand = 0;

        // Disable player scripts
        mouseLook.enabled = false;
        playerMovement.enabled = false;
        if (playerFootsteps != null) playerFootsteps.enabled = false;

        // Stop and mute footstep audio to silence them completely
        if (footstepAudioSource != null)
        {
            if (footstepAudioSource.isPlaying) footstepAudioSource.Stop();
            footstepAudioSource.mute = true;
        }

        // Switch cameras
        playerCamera.enabled = false;
        clockCamera.enabled = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("Entered clock interaction mode");
    }

    private void ExitInteraction()
    {
        if (!isInteracting) return;
        isInteracting = false;
        if (playerFootsteps != null) playerFootsteps.canPlay = true;
        // Restore camera
        if (clockCamera != null) clockCamera.enabled = false;
        if (playerCamera != null) playerCamera.enabled = true;

        // Re-enable player scripts
        if (mouseLook != null) mouseLook.enabled = true;
        if (playerMovement != null) playerMovement.enabled = true;
        if (playerFootsteps != null)
        {
            playerFootsteps.enabled = true;
            if (footstepAudioSource != null) footstepAudioSource.mute = false;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("Exited clock interaction mode");
    }
}