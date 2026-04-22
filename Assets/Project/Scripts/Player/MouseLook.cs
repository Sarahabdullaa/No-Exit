using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;

    void Start()
    {
        // This locks the mouse to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Getting mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Vertical rotation (Looking up and down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limits looking to 90 degrees up/down

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal rotation (Turning the whole body left/right)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}