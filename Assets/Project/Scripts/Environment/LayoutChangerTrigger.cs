using UnityEngine;

public class LayoutChangerTrigger : MonoBehaviour
{
    public GameObject crowdedHallwayAssets;

    
    public bool shouldEnableAssets = false; 

    
    public bool playOnlyOnce = true;
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playOnlyOnce && hasTriggered) return;

            if (crowdedHallwayAssets != null)
            {
                
                crowdedHallwayAssets.SetActive(shouldEnableAssets);
                hasTriggered = true;

               
            }
        }
    }
}