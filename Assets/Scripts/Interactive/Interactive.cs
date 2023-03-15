using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public ItemName requireItem;
    public bool isDone;

    public void CheckItem(ItemName itemName)
    {
        //考虑这里就Check背包中有没有对应道具，有则使用，后续改这里的逻辑
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
