using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterCaption : MonoBehaviour
{
    public GameObject subtitlePanel;
    public TextMeshProUGUI captionText;
    public TextMeshProUGUI objectiveText;
    public Transform player;

    public AudioSource typingAudioSource;

    [TextArea(2, 4)]
    public string[] narrativeMessages;

    [TextArea]
    public string objectiveMessage = "Objective: Enter the room on your right.";

    public float delayBeforeShow = 2f;
    public float letterDelay = 0.06f;
    public float delayBetweenMessages = 1f;
    public float stayAfterNarrative = 1.5f;
    public float moveThreshold = 0.3f;

    private bool waitingForMovement = false;
    private bool objectiveShown = false;
    private Vector3 movementCheckStartPosition;

    void Start()
    {
        subtitlePanel.SetActive(false);
        captionText.text = "";
        objectiveText.text = "";

        StartCoroutine(PlayNarratives());
    }

    IEnumerator PlayNarratives()
    {
        yield return new WaitForSeconds(delayBeforeShow);

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

        movementCheckStartPosition = player.position;
        waitingForMovement = true;
    }

    void Update()
    {
        if (waitingForMovement && !objectiveShown)
        {
            float distanceMoved = Vector3.Distance(player.position, movementCheckStartPosition);

            if (distanceMoved > moveThreshold)
            {
                waitingForMovement = false;
                objectiveShown = true;
                StartCoroutine(ShowObjective());
            }
        }
    }

    IEnumerator ShowObjective()
    {
        objectiveText.text = "";

        StartTypingSound();

        foreach (char letter in objectiveMessage)
        {
            objectiveText.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }

        StopTypingSound();

        yield return new WaitForSeconds(3f);

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