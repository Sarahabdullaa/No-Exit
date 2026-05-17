using UnityEngine;
using System.Collections;

public class PuzzleSolvedUI : MonoBehaviour
{
    public GameObject panel;
    public CanvasGroup canvasGroup;

    public float fadeSpeed = 2f;
    public float displayTime = 3f;

 
    public void ShowPuzzleComplete()
    {
        StopAllCoroutines();
        StartCoroutine(ShowUI());
    }

    IEnumerator ShowUI()
    {
        panel.SetActive(true);

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

        panel.SetActive(false);
    }
}