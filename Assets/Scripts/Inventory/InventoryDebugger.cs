using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class InventoryDebugger
{
    [MenuItem("运行时调试/背包系统/增加笔记本道具")]
    static void AddNotebook()
    {
        AddItem(ItemName.Notebook);
    }

    [MenuItem("运行时调试/背包系统/使用笔记本")]
    static void UseNotebook()
    {
        UseItem(ItemName.Notebook);
    }
    
    [MenuItem("运行时调试/背包系统/增加Test道具")]
    static void AddTest()
    {
        AddItem(ItemName.Test);
    }
    
    [MenuItem("运行时调试/背包系统/使用Test")]
    static void UseTest()
    {
        UseItem(ItemName.Test);
    }

    private static void AddItem(ItemName itemName)
    {
        InventoryManager.instance.AddItem(itemName);
    }

    private static void UseItem(ItemName itemName)
    {
        EventHandler.CallItemUsedEvent(itemName);
    }
}