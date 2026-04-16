using UnityEngine;

public class Float : MonoBehaviour
{
    // High values make it bounce, low values keep it "ghostly"
    public float amplitude = 0.05f; // Only moves 5cm up and down
    public float frequency = 0.3f;  // Very slow speed

    private Vector3 startPos;

    void Start()
    {
        // Store the starting position so it doesn't drift away
        startPos = transform.localPosition;
    }

    void Update()
    {
        // Calculate the new Y position using a Sin wave
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;

        // Apply the position while keeping X and Z the same
        transform.localPosition = new Vector3(startPos.x, newY, startPos.z);
    }
}
