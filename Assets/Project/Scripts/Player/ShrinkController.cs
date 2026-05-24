using UnityEngine;

public class ShrinkController : MonoBehaviour
{
    [Header("Normal State")]
    public float normalSpeed = 5f;
    public float normalJumpHeight = 2f;
    public Vector3 normalScale = Vector3.one;
    public Vector3 normalCameraPos = new Vector3(0, 0.6f, 0);  // eye level

    [Header("Shrunken State")]
    public float shrinkSpeed = 3f;
    public float shrinkJumpHeight = 1.5f;
    public Vector3 shrinkScale = new Vector3(0.2f, 0.2f, 0.2f);
    public Vector3 shrinkCameraPos = new Vector3(0, 0.12f, 0);  // eye level for tiny

    [Header("Components")]
    public CharacterController controller;
    public Camera playerCamera;
    public Transform visualModel;   // the mesh that shows the player (optional)

    private PlayerMovement playerMovement;
    private Vector3 normalControllerCenter;
    private float normalControllerHeight;
    private bool isShrunken = false;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();

        // Store original collider values
        normalControllerHeight = controller.height;
        normalControllerCenter = controller.center;

        if (visualModel != null)
            visualModel.localScale = normalScale;

        SetNormal();
    }

    public void Shrink()
    {
        if (isShrunken) return;
        isShrunken = true;

        // Scale the whole player object (including collider and camera)
        transform.localScale = shrinkScale;

        // Adjust CharacterController to match new scale
        controller.height = normalControllerHeight * shrinkScale.y;
        controller.center = normalControllerCenter * shrinkScale.y;
        // Update movement speed and jump
        playerMovement.speed = shrinkSpeed;
        playerMovement.jumpHeight = shrinkJumpHeight;

        // Adjust camera local position (so eyes are at correct height)
        playerCamera.transform.localPosition = shrinkCameraPos;

        Debug.Log("Player shrunk with proper collider and camera!");
    }

    public void GrowBack()
    {
        if (!isShrunken) return;
        isShrunken = false;
        SetNormal();
    }

    void SetNormal()
    {
        transform.localScale = normalScale;
        controller.height = normalControllerHeight;
        controller.center = normalControllerCenter;
        playerMovement.speed = normalSpeed;
        playerMovement.jumpHeight = normalJumpHeight;
        playerCamera.transform.localPosition = normalCameraPos;
    }
}