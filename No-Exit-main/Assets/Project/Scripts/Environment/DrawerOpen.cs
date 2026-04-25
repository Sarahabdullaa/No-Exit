using UnityEngine;

public class DrawerOpen : MonoBehaviour
{
    public Vector3 openOffset = new Vector3(0, 0, 1.5f);
    public float speed = 5f;

    public AudioClip openSound;
    public AudioClip closeSound;

    private AudioSource audioSource;

    private Vector3 closedPos;
    private Vector3 openPos;
    private bool isOpen = false;

    void Start()
    {
        closedPos = transform.localPosition;
        openPos = closedPos + openOffset;

        audioSource = GetComponent<AudioSource>();
    }

    public void ToggleDrawer()
    {
        isOpen = !isOpen;

        // Play sound
        if (isOpen)
        {
            audioSource.PlayOneShot(openSound);
        }
        else
        {
            audioSource.PlayOneShot(closeSound);
        }
    }

    void Update()
    {
        Vector3 target = isOpen ? openPos : closedPos;

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            target,
            Time.deltaTime * speed
        );
    }
}
