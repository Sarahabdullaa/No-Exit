using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;

    [Header("Camera Settings")]
    public Transform playerCamera;
    public float mouseSensitivity = 0.1f; // Start very low (0.1 to 0.5)
    public float smoothing = 10f; // How "smooth" the camera feels

    private float xRotation = 0f;
    private Vector2 currentMouseDelta;
    private Vector2 appliedMouseDelta;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Helps sync frames for a smoother look
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        HandleMovement();
    }

    void LateUpdate()
    {
        HandleRotation();
    }

    void HandleMovement()
    {
        if (Keyboard.current == null) return;

        float x = (Keyboard.current.dKey.isPressed ? 1f : 0f) - (Keyboard.current.aKey.isPressed ? 1f : 0f);
        float z = (Keyboard.current.wKey.isPressed ? 1f : 0f) - (Keyboard.current.sKey.isPressed ? 1f : 0f);

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    void HandleRotation()
    {
        if (Mouse.current == null) return;

        // 1. Get raw delta
        Vector2 targetMouseDelta = Mouse.current.delta.ReadValue();

        // 2. Smooth the delta (This removes the "jitter")
        currentMouseDelta = Vector2.Lerp(currentMouseDelta, targetMouseDelta, Time.deltaTime * smoothing);

        float mouseX = currentMouseDelta.x * mouseSensitivity;
        float mouseY = currentMouseDelta.y * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // 3. Apply rotation
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}