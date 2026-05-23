using UnityEngine;

public class TextureTrackingEye : MonoBehaviour
{
    private Transform playerTransform;
    private Material paintingMaterial;

    [Header("Adjust Strength")]
    public float movementScale = 0.02f; // Keep this very small so the whole face doesn't slide off the frame

    void Start()
    {
        // Automatically find the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerTransform = player.transform;

        // Get the material of the portrait
        paintingMaterial = GetComponent<MeshRenderer>().material;
    }

    void LateUpdate()
    {
        if (playerTransform == null || paintingMaterial == null) return;

        // Calculate the relative direction of the player to the painting
        Vector3 targetDir = playerTransform.position - transform.position;
        Vector3 localDir = transform.InverseTransformDirection(targetDir);

        // Map the player's X and Y movement to the texture's UV offset
        float offsetX = Mathf.Clamp(localDir.x * movementScale, -0.05f, 0.05f);
        float offsetY = Mathf.Clamp(localDir.y * movementScale, -0.05f, 0.05f);

        // Apply the offset directly to the material texture
        paintingMaterial.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
    }
}