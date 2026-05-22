using UnityEngine;

public class EnableHallwayDoor : MonoBehaviour
{
    public GameObject extendedHallway;

   
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (extendedHallway != null)
            {
         
                extendedHallway.SetActive(true);

          
                enabled = false;
            }
        }
    }
}