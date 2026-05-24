using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class EndingManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject endingPanel;
    public TMP_Text endingNarration;
    public GameObject theEndText;

    [Header("Ending Texts")]
    [TextArea]
    public string acceptEnding;

    [TextArea]
    public string followEnding;

    [Header("Typing Settings")]
    public float typingSpeed = 0.06f;
    public float endingDelay = 6f;


    // ACCEPT REALITY BUTTON
    public void AcceptReality()
    {
        StartCoroutine(ShowEnding(acceptEnding));
    }

    // FOLLOW HIM BUTTON
    public void FollowHim()
    {
        StartCoroutine(ShowEnding(followEnding));
    }

    IEnumerator ShowEnding(string message)
    {
        // Show black screen panel
        endingPanel.SetActive(true);

        // Hide THE END at start
        theEndText.SetActive(false);

        // Clear previous text
        endingNarration.text = "";

        // Small pause before typing
        yield return new WaitForSeconds(1f);

        // Typewriter effect
        foreach (char c in message)
        {
            endingNarration.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Wait after narration finishes
        yield return new WaitForSeconds(endingDelay);

        // Clear narration
        endingNarration.text = "";

        // Small pause before THE END
        yield return new WaitForSeconds(1.5f);

        // Show THE END
        theEndText.SetActive(true);

        // Back to Main
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("MainMenu");
    }
}