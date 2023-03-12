using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public const int SlotCount = 8;
    
    public static InventoryManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
        inventoryUI.OnInit();
    }

    private void OnDisable()
    {
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
        inventoryUI.OnEnd();
    }

    private void OnItemUsedEvent(ItemName itemName)
    {
        var index = GetItemIndex(itemName);
        if (index != -1)
        {
            itemList.RemoveAt(index);
        }
        else
        {
            Debug.LogError("背包中无道具："+itemName.ToString()+"。");
        }

        if (itemList.Count == 0)
        {
            EventHandler.CallUpdateInventoryUIEvent(null, -1);
        }
    }

    //当前背包
    [SerializeField] private List<ItemName> itemList = new List<ItemName>();

    //所有道具
    public ItemDataList itemDataList;

    public InventoryUI inventoryUI;

    public void AddItem(ItemName itemName)
    {
        if (!itemList.Contains(itemName))
        {
            itemList.Add(itemName);
            //更新背包UI
            EventHandler.CallUpdateInventoryUIEvent(itemDataList.GetItemDetails(itemName), itemList.Count - 1);
            //发送“获取道具”事件
            EventHandler.CallAddItemEvent(itemName);
        }
        else
        {
            Debug.LogError("背包中已包含道具："+itemName.ToString()+"。\n无法重复获取道具。");
        }
    }

    private int GetItemIndex(ItemName itemName)
    {
        return itemList.FindIndex(x => x == itemName);
    }

    public bool IsItemListEmpty()
    {
        return itemList.Count == 0;
    }
    
    public void ShowItemDetails(int index)
    {
        if (index < itemList.Count)
        {
            EventHandler.CallUpdateInventoryUIEvent(itemDataList.GetItemDetails(itemList[index]), index);
        }
        else
        {
            EventHandler.CallUpdateInventoryUIEvent(null, -1);
        }
    }
}
