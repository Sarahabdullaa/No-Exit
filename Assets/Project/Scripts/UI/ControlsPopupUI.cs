using UnityEngine;
using System.Collections;

public class ControlsPopupUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public float fadeSpeed = 2f;
    public float displayTime = 4f;

    void Start()
    {
        StartCoroutine(ShowControls());
    }

    IEnumerator ShowControls()
    {
        // Fade In
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }

        yield return new WaitForSeconds(displayTime);

        // Fade Out
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}