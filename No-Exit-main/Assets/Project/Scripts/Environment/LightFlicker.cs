using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour
{
    private Light lightSource;
    private AudioSource buzzSound;
    public float minTime = 0.05f;
    public float maxTime = 0.2f;

    void Start()
    {
        lightSource = GetComponent<Light>();
        buzzSound = GetComponent<AudioSource>();
        StartCoroutine(FlickerRoutine());
    }

    IEnumerator FlickerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            lightSource.enabled = !lightSource.enabled;

            if (buzzSound != null)
            {
                // Mute sound when light is off
                buzzSound.mute = !lightSource.enabled;
            }
        }
    }
}