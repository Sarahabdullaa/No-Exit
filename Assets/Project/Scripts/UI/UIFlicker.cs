using UnityEngine;

public class UIFlicker : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public float speed = 1.5f;
    public float minAlpha = 0.4f;
    public float maxAlpha = 1f;

    void Update()
    {
        float alpha = Mathf.Lerp(minAlpha, maxAlpha,
            (Mathf.Sin(Time.time * speed) + 1f) / 2f);

        canvasGroup.alpha = alpha;
    }
}