using UnityEngine;

public class Room3ClosetGhostTrigger : MonoBehaviour
{
    public Room3ClosetGhostEvent closetGhostEvent;
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;

            if (closetGhostEvent != null)
                closetGhostEvent.TriggerClosetGhostEvent();
        }
    }
}