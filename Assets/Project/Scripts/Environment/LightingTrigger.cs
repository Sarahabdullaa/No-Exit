using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightingTrigger : MonoBehaviour
{
    public GameObject LIT_01_Childhood;
    public GameObject LIT_02_Tension;
    public float fadeDuration = 2.0f; // How many seconds the change takes

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeLighting());
            // Disable collider so it doesn't trigger twice
            GetComponent<Collider>().enabled = false;
        }
    }

    IEnumerator FadeLighting()
    {
        // 1. Get all lights from both folders
        Light[] lights01 = LIT_01_Childhood.GetComponentsInChildren<Light>();
        Light[] lights02 = LIT_02_Tension.GetComponentsInChildren<Light>();

        // 2. Turn LIT_02 ON but set its intensity to 0 first
        LIT_02_Tension.SetActive(true);
        Dictionary<Light, float> originalIntensities01 = new Dictionary<Light, float>();
        Dictionary<Light, float> targetIntensities02 = new Dictionary<Light, float>();

        foreach (Light l in lights01) originalIntensities01[l] = l.intensity;
        foreach (Light l in lights02)
        {
            targetIntensities02[l] = l.intensity;
            l.intensity = 0;
        }

        // 3. Fade over time
        float elapsed = 0;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            // Fade OUT folder 1
            foreach (Light l in lights01)
            {
                if (l != null) l.intensity = Mathf.Lerp(originalIntensities01[l], 0, t);
            }

            // Fade IN folder 2
            foreach (Light l in lights02)
            {
                if (l != null) l.intensity = Mathf.Lerp(0, targetIntensities02[l], t);
            }

            yield return null;
        }

        // 4. Finally shut off folder 1
        LIT_01_Childhood.SetActive(false);
    }
}
