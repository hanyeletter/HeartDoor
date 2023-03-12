using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private ItemName currentItem;
    private Vector3 mouseWorldPos =>
        Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

    private bool canClick;
    
    private void Update()
    {
        canClick = ObjectAtMousePosition();

        if (canClick && Input.GetMouseButtonDown(0))
        {
            //检测鼠标互动情况
            ClickAction(ObjectAtMousePosition().gameObject);
        }
    }

    private void ClickAction(GameObject clickObject)
    {
        switch (clickObject.tag)
        {
            case "Teleport":
                var teleport = clickObject.GetComponent<Teleport>();
                teleport?.TeleportToScene();
                break;
            case "Interactive":
                var interactive = clickObject.GetComponent<Interactive>();
                interactive?.CheckItem(currentItem);
                break;
        }
    }

    /// <summary>
    /// 检测鼠标点击范围的碰撞体
    /// </summary>
    /// <returns></returns>
    private Collider2D ObjectAtMousePosition()
    {
        return Physics2D.OverlapPoint(mouseWorldPos);
    }
}