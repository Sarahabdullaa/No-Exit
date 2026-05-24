using UnityEngine;

public class EyeFollow : MonoBehaviour

{
    public Transform player;

    [Header("Eye Movement Limits")]
    public float maxMoveDistance = 0.04f; 
    public float followSpeed = 5f;      

    private Vector3 initialLocalPos;

    void Start()
    {

        initialLocalPos = transform.localPosition;
    }

    void Update()
    {
        if (player == null) return;

        Vector3 targetDir = player.position - transform.position;

        Vector3 localTargetDir = transform.parent.InverseTransformDirection(targetDir);

        localTargetDir.z = 0;

        Vector3 clampedLocalDir = Vector3.ClampMagnitude(localTargetDir, maxMoveDistance);

        Vector3 targetLocalPosition = initialLocalPos + clampedLocalDir;

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetLocalPosition,
            followSpeed * Time.deltaTime
        );
    }

}

