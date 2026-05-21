using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource footstepSource;
    public float moveThreshold = 0.1f;

    void Update()
    {
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