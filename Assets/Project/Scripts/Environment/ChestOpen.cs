using UnityEngine;

public class ChestOpen : MonoBehaviour,IInteractable
{
    public Transform lid;
    public float openAngle = -90f;
    public float speed = 2f;

    public AudioClip openSound;
    public AudioClip closeSound;

    private AudioSource audioSource;
    private bool isOpen = false;

    private Quaternion closedRotation;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        closedRotation = lid.localRotation;
    }

    void Update()
    {
        Quaternion targetRotation;

        if (isOpen)
        {
            targetRotation = Quaternion.Euler(openAngle, 0, 0);
        }
        else
        {
            targetRotation = closedRotation;
        }

        lid.localRotation = Quaternion.Slerp(
            lid.localRotation,
            targetRotation,
            Time.deltaTime * speed);
    }

    public void OpenChest()
    {
        if (!isOpen)
        {
            isOpen = true;
            audioSource.PlayOneShot(openSound);
        }
    }

    public void CloseChest()
    {
        if (isOpen)
        {
            isOpen = false;
            audioSource.PlayOneShot(closeSound);
        }
    }

    public void Interact()
    {
        if (isOpen)
        {
            CloseChest();
        }
        else
        {
            OpenChest();
        }
    }
}