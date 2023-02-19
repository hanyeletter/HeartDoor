using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenuCloseButtonUI : MonoBehaviour
{
    public GameObject inventoryCanvas;

    public void CloseInventoryUI()
    {
        if (inventoryCanvas != null)
        {
            inventoryCanvas.SetActive(false);
        }
    }
}
