using UnityEngine;

public class DrawerOpen : MonoBehaviour, IInteractable
{
   
    public Vector3 openOffset = new Vector3(0, 0, 1.5f);
    public float speed = 5f;

    
    public AudioClip openSound;
    public AudioClip closeSound;
    public AudioClip lockedSound;    

  
    public bool isLocked = true;    

    private AudioSource audioSource;
    private Vector3 closedPos;
    private Vector3 openPos;
    private bool isOpen = false;

    void Start()
    {
        closedPos = transform.localPosition;
        openPos = closedPos + openOffset;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void UnlockDrawer()
    {
        isLocked = false;
      
    }

    public void ToggleDrawer()
    {
        if (isLocked)
        {
           
            if (lockedSound != null)
                audioSource.PlayOneShot(lockedSound);
            else
                Debug.Log("Drawer is locked!");
            return;
        }

        isOpen = !isOpen;

        if (isOpen)
        {
            if (openSound != null) audioSource.PlayOneShot(openSound);
        }
        else
        {
            if (closeSound != null) audioSource.PlayOneShot(closeSound);
        }
    }

    void Update()
    {
        Vector3 target = isOpen ? openPos : closedPos;
        transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * speed);
    }

    public void Interact()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            audioSource.PlayOneShot(openSound);
        }
        else
        {
            audioSource.PlayOneShot(closeSound);
        }
    }
}