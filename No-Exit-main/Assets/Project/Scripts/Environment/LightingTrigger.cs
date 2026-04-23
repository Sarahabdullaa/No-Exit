using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightingTrigger : MonoBehaviour
{
    // CHANGE: Use an Array [] so you can drop Room 1 AND the Hallway lights here
    public GameObject[] lightGroupsToFadeOut;
    public GameObject lightsToFadeIn;
    public float fadeDuration = 0.1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource triggerSound = GetComponent<AudioSource>();
            if (triggerSound != null)
            {
                triggerSound.spatialBlend = 0f;
                triggerSound.Play();
            }

            // Check if we have lights to work with
            if (lightsToFadeIn != null && lightGroupsToFadeOut.Length > 0)
            {
                StartCoroutine(FadeLighting());
            }

            GetComponent<Collider>().enabled = false;
        }
    }

    IEnumerator FadeLighting()
    {
        // 1. COLLECT ALL LIGHTS from all groups in the array
        List<Light> allLightsOff = new List<Light>();
        Dictionary<Light, float> originalIntensitiesOff = new Dictionary<Light, float>();

        foreach (GameObject group in lightGroupsToFadeOut)
        {
            if (group != null)
            {
                Light[] lightsInGroup = group.GetComponentsInChildren<Light>();
                foreach (Light l in lightsInGroup)
                {
                    allLightsOff.Add(l);
                    originalIntensitiesOff[l] = l.intensity;
                }
            }
        }

        // 2. SETUP LIGHTS TO FADE IN
        lightsToFadeIn.SetActive(true);
        Light[] lightsOn = lightsToFadeIn.GetComponentsInChildren<Light>();
        Dictionary<Light, float> targetIntensitiesOn = new Dictionary<Light, float>();

        foreach (Light l in lightsOn)
        {
            targetIntensitiesOn[l] = l.intensity;
            l.intensity = 0;
        }

        // 3. THE FADE LOOP
        float elapsed = 0;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            // Fade everyone in the 'Off' list
            foreach (Light l in allLightsOff)
            {
                if (l != null) l.intensity = Mathf.Lerp(originalIntensitiesOff[l], 0, t);
            }

            // Fade everyone in the 'On' list
            foreach (Light l in lightsOn)
            {
                if (l != null) l.intensity = Mathf.Lerp(0, targetIntensitiesOn[l], t);
            }

            yield return null;
        }

        // 4. CLEAN UP
        foreach (GameObject group in lightGroupsToFadeOut)
        {
            if (group != null) group.SetActive(false);
        }
    }
}