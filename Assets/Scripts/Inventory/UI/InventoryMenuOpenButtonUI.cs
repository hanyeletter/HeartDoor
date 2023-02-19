using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenuOpenButtonUI : MonoBehaviour
{
    public GameObject inventoryCanvas;

    public void ShowInventoryUI()
    {
        if (inventoryCanvas != null)
        {
            inventoryCanvas.SetActive(true);
            if (InventoryManager.instance.IsItemListEmpty())
            {
                EventHandler.CallUpdateInventoryUIEvent(null, -1);
            }
        }
    }
}
