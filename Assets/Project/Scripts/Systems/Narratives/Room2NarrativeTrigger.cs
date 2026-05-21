using System.Collections;
using UnityEngine;
using TMPro;

public class Room2NarrativeTrigger : MonoBehaviour
{
    public GameObject subtitlePanel;
    public TextMeshProUGUI captionText;
    public TextMeshProUGUI objectiveText;

    [TextArea(2, 4)]
    public string[] narrativeMessages;

    [TextArea]
    public string objectiveMessage = "Objective: Look around the room for clues.";

    public AudioSource typingAudioSource;

    public float letterDelay = 0.06f;
    public float delayBetweenMessages = 1f;
    public float stayAfterNarrative = 1.5f;
    public float objectiveStayTime = 3f;

    private bool hasTriggered = false;

    private void Start()
    {
        subtitlePanel.SetActive(false);
        captionText.text = "";
        objectiveText.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(PlayRoom2Narrative());
        }
    }

    IEnumerator PlayRoom2Narrative()
    {
        subtitlePanel.SetActive(true);

        for (int i = 0; i < narrativeMessages.Length; i++)
        {
            captionText.text = "";

            StartTypingSound();

            foreach (char letter in narrativeMessages[i])
            {
                captionText.text += letter;
                yield return new WaitForSeconds(letterDelay);
            }

            StopTypingSound();

            yield return new WaitForSeconds(delayBetweenMessages);
        }

        yield return new WaitForSeconds(stayAfterNarrative);

        captionText.text = "";
        subtitlePanel.SetActive(false);

        objectiveText.text = "";

        StartTypingSound();

        foreach (char letter in objectiveMessage)
        {
            objectiveText.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }

        StopTypingSound();

        yield return new WaitForSeconds(objectiveStayTime);

        objectiveText.text = "";
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