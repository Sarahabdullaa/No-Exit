using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [HideInInspector]
    public bool unlocked = false;

    public Image previewImage;
    public TMP_Text itemName;
    public TMP_Text itemDescription;

    public Sprite itemSprite;

    [TextArea]
    public string descriptionText;

    public string itemTitle;

    public void SelectItem()
    {
        // STOP EMPTY SLOTS
        if (!unlocked)
            return;

        previewImage.sprite = itemSprite;
        previewImage.color = Color.white;

        itemName.text = itemTitle;
        itemDescription.text = descriptionText;
    }
}