using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataList", menuName = "Inventory/ItemDataList")]
public class ItemDataList : ScriptableObject
{
    public List<ItemDetails> itemDetailsList;

    public ItemDetails GetItemDetails(ItemName itemName)
    {
        ItemDetails itemDetails = itemDetailsList.Find(i => i.itemName == itemName);
        return itemDetails.IsValid() ? itemDetails : null;
    }
}

[System.Serializable]
public class ItemDetails
{
    public ItemName itemName;
    public Sprite itemSprite;
    public string itemUIName;
    public string itemDescription;
    public string itemKeyDescription;
    
    public string ItemDescription
    {
        get
        {
            return itemDescription + "\n\n<color=red>" + itemKeyDescription + "</color>";
        }
    }

    public bool IsValid()
    {
        return itemSprite != null && itemUIName != null && itemDescription != null;
    }
}
