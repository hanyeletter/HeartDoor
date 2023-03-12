using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public ItemName requireItem;
    public bool isDone;

    public void CheckItem(ItemName itemName)
    {
        if (itemName == requireItem && !isDone)
        {
            isDone = true;
            //使用这个物品，移除物品
            OnClickedAction();
        }
    }

    /// <summary>
    /// 默认是真确的物品的情况执行
    /// </summary>
    protected virtual void OnClickedAction()
    {
        
    }

    public virtual void EmptyClicked()
    {
        Debug.Log("空点");
    }
}
