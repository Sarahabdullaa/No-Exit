using UnityEngine;

public class HiddenPieceRevealer : MonoBehaviour
{
    [Header("Puzzle Manager (drag here)")]
    public LampPuzzleManager puzzleManager;


    [Header("Light (Optional)")]
    public Light pieceLight;             // Drag the Light component here
    public float lightIntensity = 1.5f;

    [Header("Components to hide/show")]
    public Renderer pieceRenderer;      // Drag the Renderer (MeshRenderer) here
    public Collider pieceCollider;      // Drag the Collider here (optional)

    [Header("Optional Physics")]
    public bool enablePhysicsOnReveal = true;

    private bool revealed = false;

    void Start()
    {
        // Auto-find components if not assigned
        if (pieceRenderer == null)
            pieceRenderer = GetComponent<Renderer>();
        if (pieceCollider == null)
            pieceCollider = GetComponent<Collider>();

        // Start hidden
        if (pieceRenderer != null) pieceRenderer.enabled = false;
        if (pieceCollider != null) pieceCollider.enabled = false;
    }

    void Update()
    {
        if (revealed) return;
        if (puzzleManager == null) return;

        if (puzzleManager.IsPuzzleCompleted)
        {
            Reveal();
        }
    }

    void Reveal()
    {
        revealed = true;

        // Show renderer
        if (pieceRenderer != null) pieceRenderer.enabled = true;
        // Enable collider (so player can pick it up)
        if (pieceCollider != null) pieceCollider.enabled = true;

        // Optional: make it fall
        if (enablePhysicsOnReveal)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false;
        }

        if (pieceLight != null)
        {
            pieceLight.enabled = true;
            pieceLight.intensity = lightIntensity;
        }

        Debug.Log("Hidden piece revealed!");
        enabled = false; // stop checking
    }
}