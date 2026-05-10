using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItem : MonoBehaviour
{
    [Header("Inventory Icons")]
    public GameObject hudIcon;
    public GameObject journalIcon;

    [Header("Preview")]
    public Image itemPreview;
    public Sprite previewSprite;

    [Header("Text")]
    public TMP_Text itemName;
    public TMP_Text itemDescription;

    [TextArea]
    public string descriptionText;

    public string itemTitle;

    private bool playerNearby = false;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PickupItem();
        }
    }

    void PickupItem()
    {
        hudIcon.SetActive(true);
        journalIcon.SetActive(true);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}