using UnityEngine;

public class ShrinkTrigger : MonoBehaviour, IInteractable
{
    public AudioClip shrinkSound;

    public void Interact()
    {
        ShrinkController shrinker = FindObjectOfType<ShrinkController>();
        if (shrinker != null)
        {
            shrinker.Shrink();
            if (shrinkSound != null)
                AudioSource.PlayClipAtPoint(shrinkSound, transform.position);

            // Optional: disable this trigger or destroy it so player can't shrink again
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("ShrinkController not found on player!");
        }
    }
}