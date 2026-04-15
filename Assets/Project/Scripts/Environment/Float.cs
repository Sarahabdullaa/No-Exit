using UnityEngine;

public class Float : MonoBehaviour
{
    public float amplitude = 0.2f; // How high it floats
    public float frequency = 0.5f; // How fast it bobs
    void Update()
    {
        transform.localPosition += new Vector3(0, Mathf.Sin(Time.time * frequency) * amplitude * Time.deltaTime, 0);
    }
}
