using System.Collections.Generic;
using UnityEngine;
using TMPro;   // If you use TextMeshPro – otherwise remove

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;   // Singleton

    [Header("UI")]
    public GameObject journalContentPanel;     // The panel where items will be listed
    public GameObject itemUIPrefab;            // A simple Text or Button prefab for each item

    private List<string> collectedItems = new List<string>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddItem(string itemName)
    {
        if (collectedItems.Contains(itemName)) return; // no duplicates

        collectedItems.Add(itemName);
        Debug.Log($"Collected: {itemName}");

        // Update the journal UI
        if (journalContentPanel != null && itemUIPrefab != null)
        {
            GameObject newItemUI = Instantiate(itemUIPrefab, journalContentPanel.transform);
            newItemUI.GetComponentInChildren<TMP_Text>().text = itemName;
        }
    }

    // Optional: check if item is collected
    public bool HasItem(string itemName) => collectedItems.Contains(itemName);
}