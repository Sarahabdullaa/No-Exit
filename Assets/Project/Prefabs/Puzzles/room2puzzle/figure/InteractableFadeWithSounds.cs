using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class InteractableFadeWithSounds : MonoBehaviour
{
    [Header("Interaction")]
    public string playerTag = "Player";
    public KeyCode interactKey = KeyCode.E;
    public float fadeDuration = 1.5f;

    [Header("Material Fade")]
    public Renderer targetRenderer;          // Assign your character's renderer here
    public string fadePropertyName = "_visible_amount"; // or your graph property name
    public float startVisibleValue = 0f;     // visible
    public float endInvisibleValue = 1f;     // invisible

    [Header("Sounds")]
    public AudioClip spawnSound;
    public AudioClip interactSound;

    private AudioSource audioSource;
    private bool playerInRange = false;
    private bool isFading = false;
    private Material[] mats;
    private int fadePropertyID;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        // Make sure collider is a trigger (for range check)
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;

        // If not assigned, try to find a renderer on this object
        if (targetRenderer == null)
        {
            targetRenderer = GetComponentInChildren<Renderer>();
        }

        if (targetRenderer != null)
        {
            // Use instance materials so we don’t edit shared ones
            mats = targetRenderer.materials;
            fadePropertyID = Shader.PropertyToID(fadePropertyName);

            // Initialize visible
            SetFadeValue(startVisibleValue);
        }
    }

    void Start()
    {
        // Play spawn sound once on load
        if (spawnSound != null)
        {
            audioSource.PlayOneShot(spawnSound);
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey) && !isFading)
        {
            // Play interaction sound
            if (interactSound != null)
            {
                audioSource.PlayOneShot(interactSound);
            }

            // Start fade out
            StartCoroutine(FadeOutRoutine());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = false;
        }
    }

    IEnumerator FadeOutRoutine()
    {
        if (mats == null || mats.Length == 0) yield break;

        isFading = true;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;

            float current = Mathf.Lerp(startVisibleValue, endInvisibleValue, t);
            SetFadeValue(current);

            yield return null;
        }

        // Make sure it ends exactly on the final value
        SetFadeValue(endInvisibleValue);
        isFading = false;
    }

    private void SetFadeValue(float value)
    {
        foreach (var m in mats)
        {
            if (m.HasProperty(fadePropertyID))
            {
                m.SetFloat(fadePropertyID, value);
            }
        }
    }
}