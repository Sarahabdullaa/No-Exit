using UnityEngine;

public class EyeFollow2DFixed : MonoBehaviour
{
    public Transform player;

    [Header("Eye Movement Limits")]
    public float maxMoveDistance = 0.03f;
    public float followSpeed = 5f;

    private Vector3 initialLocalPos;

    void Start()
    {
        // Explicitly lock down the placement you created in the editor
        initialLocalPos = transform.localPosition;
    }

    void Update()
    {
        if (player == null) return;

        // 1. Get direction from eye to player
        Vector3 targetDir = player.position - transform.position;

        // 2. Convert to local space relative to the frame container
        Vector3 localTargetDir = transform.parent.InverseTransformDirection(targetDir);

        // 3. FORCE BALANCE: If the eyes keep drifting down, it's because the 
        // forward direction of your mesh is leaking into the Y calculation.
        // We strip any depth tilting here:
        Vector2 flatMovement = new Vector2(localTargetDir.x, localTargetDir.y);

        // 4. Clamp within eye sockets
        flatMovement = Vector2.ClampMagnitude(flatMovement, maxMoveDistance);

        // 5. Build final target position, strictly preserving your original editor Z-depth
        Vector3 targetLocalPosition = new Vector3(
            initialLocalPos.x + flatMovement.x,
            initialLocalPos.y + flatMovement.y,
            initialLocalPos.z
        );

        // 6. Smoothly move the pupil
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetLocalPosition,
            followSpeed * Time.deltaTime
        );
    }
}