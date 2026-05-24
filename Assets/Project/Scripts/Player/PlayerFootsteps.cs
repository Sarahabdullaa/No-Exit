using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource footstepSource;
    public float moveThreshold = 0.1f;
    public bool canPlay = true;   // new: external control

    void Update()
    {
        if (!canPlay) return;     // if false, skip all footsteps

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        bool isMoving = Mathf.Abs(horizontal) > moveThreshold || Mathf.Abs(vertical) > moveThreshold;

        if (isMoving)
        {
            if (!footstepSource.isPlaying)
            {
                footstepSource.Play();
            }
        }
        else
        {
            if (footstepSource.isPlaying)
            {
                footstepSource.Stop();
            }
        }
    }
}