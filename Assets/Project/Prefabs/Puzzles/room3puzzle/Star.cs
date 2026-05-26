using UnityEngine;

public class Star : MonoBehaviour
{
    public AudioClip hitSound;
    public int points = 1;

    public StarPuzzleManager manager;

    void Start()
    {
       if(manager == null ) manager = FindObjectOfType<StarPuzzleManager>();
        if (manager == null)
            Debug.LogError("StarPuzzleManager not found in scene!");
    }

    public void DestroyStar()
    {
        if (hitSound != null)
            AudioSource.PlayClipAtPoint(hitSound, transform.position);

        if (manager != null)
            manager.StarDestroyed();

        Destroy(gameObject);
    }
}