using System.Collections;
using UnityEngine;
using TMPro;

public class FinalDoorGhostTrigger : MonoBehaviour
{
    public GameObject ghostObject;
    public GameObject subtitlePanel;
    public TextMeshProUGUI captionText;
    public AudioSource typingAudioSource;

    [TextArea]
    public string ghostMessage = "He’s waiting for me there.";

    public float letterDelay = 0.06f;
    public float visibleTime = 2f;
    public float subtitleStayTime = 2.5f;

    private bool hasTriggered = false;

    void Start()
    {
        if (ghostObject != null)
            ghostObject.SetActive(false);

        if (subtitlePanel != null)
            subtitlePanel.SetActive(false);

        if (captionText != null)
            captionText.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && Room4NarrativeTrigger.room4Finished && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(ShowGhostAndNarrative());
        }
    }

    IEnumerator ShowGhostAndNarrative()
    {
        if (ghostObject != null)
            ghostObject.SetActive(true);

        if (subtitlePanel != null)
            subtitlePanel.SetActive(true);

        if (captionText != null)
            captionText.text = "";

        StartTypingSound();

        foreach (char letter in ghostMessage)
        {
            captionText.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }

        StopTypingSound();

        yield return new WaitForSeconds(visibleTime);

        if (ghostObject != null)
            ghostObject.SetActive(false);

        yield return new WaitForSeconds(subtitleStayTime);

        if (captionText != null)
            captionText.text = "";

        if (subtitlePanel != null)
            subtitlePanel.SetActive(false);
    }

    void StartTypingSound()
    {
        if (typingAudioSource != null && !typingAudioSource.isPlaying)
        {
            typingAudioSource.Play();
        }
    }

    void StopTypingSound()
    {
        if (typingAudioSource != null && typingAudioSource.isPlaying)
        {
            typingAudioSource.Stop();
        }
    }
}