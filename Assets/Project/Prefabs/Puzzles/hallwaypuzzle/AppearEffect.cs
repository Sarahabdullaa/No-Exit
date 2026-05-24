using UnityEngine;
using System.Collections;

public class AppearEffect : MonoBehaviour
{
    public float popDuration = 0.3f;
    public Vector3 popScale = new Vector3(1.2f, 1.2f, 1.2f);

    void OnEnable()
    {
        StartCoroutine(AnimatePop());
    }

    IEnumerator AnimatePop()
    {
        Vector3 originalScale = transform.localScale;
        float elapsed = 0f;
        while (elapsed < popDuration)
        {
            float t = elapsed / popDuration;
            // ease out bounce effect
            float scaleFactor = 1f + (1f - t) * 0.5f;
            transform.localScale = originalScale * scaleFactor;
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;
    }
}