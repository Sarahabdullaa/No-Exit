using UnityEngine;
using UnityEngine.InputSystem; // This is the key change!

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;
    public float mouseSensitivity = 20f; // Adjusted for better control
    public Transform playerCamera;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // This forces the game to run at 60 FPS in the build
        // This usually removes horizontal lines instantly
        Application.targetFrameRate = 60;
    }

    // Movement stays here
    void Update()
    {
        if (Keyboard.current != null)
        {
            float x = (Keyboard.current.dKey.isPressed ? 1 : 0) - (Keyboard.current.aKey.isPressed ? 1 : 0);
            float z = (Keyboard.current.wKey.isPressed ? 1 : 0) - (Keyboard.current.sKey.isPressed ? 1 : 0);

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

            // Apply sensitivity and a small smoothing factor
            float mouseX = delta.x * mouseSensitivity * 0.05f;
            float mouseY = delta.y * mouseSensitivity * 0.05f;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // Use 'localRotation' to ensure it doesn't fight with the parent
            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }
    }
}
