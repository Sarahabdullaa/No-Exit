using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItem : MonoBehaviour
{
    [Header("Inventory Icons")]
    public GameObject hudIcon;
    public GameObject journalIcon;

    [Header("Slot")]
    public InventorySlot inventorySlot;

    [Header("Preview")]
    public Image itemPreview;
    public Sprite previewSprite;

    [Header("Text")]
    public TMP_Text itemName;
    public TMP_Text itemDescription;

    [TextArea]
    public string descriptionText;

    public string itemTitle;

    void Start()
    {
        // Hide HUD icon
        if (hudIcon != null)
            hudIcon.SetActive(false);

        // Hide journal item
        if (journalIcon != null)
            journalIcon.SetActive(false);
    }

    public void PickupItem()
    {

        PlayerInventory.AddPiece(itemTitle);
        // SHOW HUD ICON
        if (hudIcon != null)
            hudIcon.SetActive(true);

        // SHOW JOURNAL ITEM
        if (journalIcon != null)
            journalIcon.SetActive(true);

        // UNLOCK SLOT
        if (inventorySlot != null)
            inventorySlot.unlocked = true;

        // SET PREVIEW DATA
        if (itemPreview != null)
            itemPreview.sprite = previewSprite;

        if (itemName != null)
            itemName.text = itemTitle;

        if (itemDescription != null)
            itemDescription.text = descriptionText;
    }
}