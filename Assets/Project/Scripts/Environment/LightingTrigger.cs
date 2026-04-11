using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightingTrigger : MonoBehaviour
{
    public GameObject lightsToFadeOut;
    public GameObject lightsToFadeIn;
    public float fadeDuration = 0.1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Play the "Pop" sound
            AudioSource triggerSound = GetComponent<AudioSource>();
            if (triggerSound != null)
            {
                triggerSound.spatialBlend = 0f; // Force 2D for loudness
                triggerSound.Play();
            }

            if (lightsToFadeIn != null && lightsToFadeOut != null)
            {
                StartCoroutine(FadeLighting());
            }

            // Disable trigger so it only happens once
            GetComponent<Collider>().enabled = false;
        }
    }

    IEnumerator FadeLighting()
    {
        // INSTANT CUT: If duration is near 0, just swap them immediately
        if (fadeDuration <= 0.05f)
        {
            lightsToFadeIn.SetActive(true);
            lightsToFadeOut.SetActive(false);
            yield break;
        }

        // SMOOTH FADE: Used for earlier rooms (1, 2, 3)
        Light[] lightsOff = lightsToFadeOut.GetComponentsInChildren<Light>();
        Light[] lightsOn = lightsToFadeIn.GetComponentsInChildren<Light>();

        lightsToFadeIn.SetActive(true);

        Dictionary<Light, float> originalIntensitiesOff = new Dictionary<Light, float>();
        Dictionary<Light, float> targetIntensitiesOn = new Dictionary<Light, float>();

        foreach (Light l in lightsOff) originalIntensitiesOff[l] = l.intensity;
        foreach (Light l in lightsOn)
        {
            targetIntensitiesOn[l] = l.intensity;
            l.intensity = 0;
        }

        float elapsed = 0;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            foreach (Light l in lightsOff)
                if (l != null) l.intensity = Mathf.Lerp(originalIntensitiesOff[l], 0, t);

            foreach (Light l in lightsOn)
                if (l != null) l.intensity = Mathf.Lerp(0, targetIntensitiesOn[l], t);

            yield return null;
        }

        lightsToFadeOut.SetActive(false);
    }
}