using UnityEngine;

public class EnableExtendHallway : MonoBehaviour
{
   
    public GameObject ExHallwayObject;


    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (ExHallwayObject != null)
            {

                ExHallwayObject.SetActive(true);
            }
        }
    }

}
