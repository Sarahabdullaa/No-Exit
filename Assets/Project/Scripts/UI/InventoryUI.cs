using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject journalUI;
    public GameObject blurOverlay;
    public GameObject inventoryHUD;

    public AudioSource paperSound;

    // PLAYER
    public MonoBehaviour playerController;
    public MouseLook mouseLook;

    void Start()
    {
        journalUI.SetActive(false);
        blurOverlay.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            bool isOpen = !journalUI.activeSelf;

            // UI
            journalUI.SetActive(isOpen);
            blurOverlay.SetActive(isOpen);
            blurOverlay.SetActive(isOpen);

            // SOUND
            paperSound.Play();

            // FREEZE PLAYER
            playerController.enabled = !isOpen;
            mouseLook.enabled = !isOpen;

            // CURSOR
            Cursor.visible = isOpen;

            if (isOpen)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}