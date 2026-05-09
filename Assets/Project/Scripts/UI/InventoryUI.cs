using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject journalUI;

    void Start()
    {
        journalUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            journalUI.SetActive(!journalUI.activeSelf);
        }
    }
}