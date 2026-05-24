using System.Collections;
using UnityEngine;

public class Room3ClosetGhostEvent : MonoBehaviour
{
    public GameObject ghostObject;
    public AudioSource eventAudioSource;
    public float ghostVisibleTime = 1.5f;

    private bool hasPlayed = false;

    public void TriggerClosetGhostEvent()
    {
        if (!hasPlayed)
        {
            hasPlayed = true;
            StartCoroutine(PlayClosetGhostEvent());
        }
    }

    IEnumerator PlayClosetGhostEvent()
    {
        if (ghostObject != null)
            ghostObject.SetActive(true);

        if (eventAudioSource != null)
            eventAudioSource.Play();

        yield return new WaitForSeconds(ghostVisibleTime);

        if (ghostObject != null)
            ghostObject.SetActive(false);
    }
}