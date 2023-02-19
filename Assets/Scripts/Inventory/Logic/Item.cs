using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemName itemName;

    public void ItemClicked()
    {
        //添加到背包中，隐藏物体
        InventoryManager.instance.AddItem(itemName);
        this.gameObject.SetActive(false);
    }
}
