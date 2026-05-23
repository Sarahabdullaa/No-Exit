using UnityEngine;

public class Star : MonoBehaviour
{
    public AudioClip hitSound;
    public int points = 1;

    public void DestroyStar()
    {
        if (hitSound != null)
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
        // Optional: add particle effect
        Destroy(gameObject);
        // You can also update a score counter here
    }
}