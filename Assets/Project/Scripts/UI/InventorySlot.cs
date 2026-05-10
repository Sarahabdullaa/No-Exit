using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image previewImage;
    public TMP_Text itemName;
    public TMP_Text itemDescription;

    public Sprite itemSprite;

    [TextArea]
    public string descriptionText;

    public string itemTitle;

    public void SelectItem()
    {
        previewImage.sprite = itemSprite;

        previewImage.color = new Color(1, 1, 1, 1);

        itemName.SetText(itemTitle);

        itemDescription.SetText(descriptionText);
    }
}