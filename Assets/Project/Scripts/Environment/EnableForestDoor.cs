using UnityEngine;

public class EnableForestDoor : MonoBehaviour
{
    public GameObject forestObject;

    
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (forestObject != null)
            {
                
                forestObject.SetActive(true);
            }
        }
    }
}