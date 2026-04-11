using UnityEngine;
using UnityEngine.InputSystem; // This is the key change!

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;
    public float mouseSensitivity = 150f;
    public Transform playerCamera;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Enable VSync (prevents horizontal screen tearing)
        QualitySettings.vSyncCount = 1;

        // Optional: set target FPS (can use 60 or 120)
        Application.targetFrameRate = 60;
    }

    // Movement stays here
    void Update()
    {
        if (Keyboard.current != null)
        {
            float x = 0f;
            float z = 0f;

            if (Keyboard.current.dKey.isPressed) x += 1f;
            if (Keyboard.current.aKey.isPressed) x -= 1f;
            if (Keyboard.current.wKey.isPressed) z += 1f;
            if (Keyboard.current.sKey.isPressed) z -= 1f;

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
        }
    }

    // Camera rotation moves here to fix the "cutting/lag"
    void LateUpdate()
    {
        if (Mouse.current != null)
        {
            // Get the raw movement
            Vector2 delta = Mouse.current.delta.ReadValue();

            float mouseX = delta.x * mouseSensitivity * Time.deltaTime;
            float mouseY = delta.y * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // Use 'localRotation' to ensure it doesn't fight with the parent
            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }
    }
}
