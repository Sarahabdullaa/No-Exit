using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f; // How high the player can jump

    private Vector3 velocity;
    private bool isGrounded;

    // We need a small check to see if the player is touching the floor
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    void Update()
    {
        // 1. Check if we are standing on the ground
        // If your CharacterController has built-in grounding, you can use controller.isGrounded
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Resets gravity pull when standing on the floor
        }

        // 2. Standard WASD Movement Logic
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // 3. JUMPING LOGIC (The new part!)
        // Physics formula for jump velocity: v = sqrt(height * -2 * gravity)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // 4. Apply Gravity over time
        velocity.y += gravity * Time.deltaTime;

        // Move the player down based on gravity velocity
        controller.Move(velocity * Time.deltaTime);
    }
}