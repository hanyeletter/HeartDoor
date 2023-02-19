using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Image itemImage;
    public Text itemNameText;
    public Text itemDescriptionText;

    public int currentIndex;

    

    public void OnInit()
    {
        EventHandler.UpdateInventoryUIEvent += OnUpdateUIEvent;
    }

    public void OnEnd()
    {
        EventHandler.UpdateInventoryUIEvent -= OnUpdateUIEvent;
    }

    private void OnUpdateUIEvent(ItemDetails itemDetails, int index)
    {
        if (itemDetails == null)
        {
            ShowEmpty();
            currentIndex = -1;
        }
        else
        {
            currentIndex = index;
            ShowItemDetails(itemDetails);
        }
    }
    
    public void ShowItemDetails(ItemName itemName)
    {
        ItemDetails itemDetails = InventoryManager.instance.itemDataList.GetItemDetails(itemName);

        if (itemDetails != null)
        {
            itemImage.sprite = itemDetails.itemSprite;
            itemNameText.text = itemDetails.itemUIName;
            itemDescriptionText.text = itemDetails.ItemDescription;
        }
    }
    
    public void ShowItemDetails(ItemDetails itemDetails)
    {
        if (itemDetails != null)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = itemDetails.itemSprite;
            itemNameText.text = itemDetails.itemUIName;
            itemDescriptionText.text = itemDetails.ItemDescription;
        }
    }
    

    public void ShowEmpty()
    {
        itemImage.sprite = null;
        itemImage.gameObject.SetActive(false);
        itemNameText.text = "";
        itemDescriptionText.text = "";
    }

    
}
