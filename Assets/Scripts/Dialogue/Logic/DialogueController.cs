using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有角色的对话控制器继承该类，重写回调函数
/// </summary>
public class DialogueController : MonoBehaviour
{
    private static Dictionary<string, DialogueData> screenKey2DialogueDataDict;

    public static Dictionary<string, DialogueData> ScreenKey2DialogueDataDict
    {
        get
        {
            if (screenKey2DialogueDataDict == null)
            {
                screenKey2DialogueDataDict = ResourcesUtil.GetAllDialogueDatas();
            }

            return screenKey2DialogueDataDict;
        }
    }

    public static Dictionary<string, Action> screenKey2CallbackDict = new Dictionary<string, Action>();
    
    [HideInInspector]
    public DialogueData dialogueData = null;
    protected Action dialogueCallback = null;
    
    [Header("当前幕键")]
    protected string curScreenKey;

    public void ShowDialogue()
    {
        //直接覆盖订阅，不确定会不会有内存泄漏问题
        if (screenKey2CallbackDict.TryGetValue(curScreenKey, out Action cb))
        {
            dialogueCallback = cb;
        }
        else
        {
            dialogueCallback = null;
        }
        EventHandler.CallShowDialogueEvent(dialogueData, dialogueCallback);
    }
        
    protected virtual void Callback()
    {
        Debug.Log("触发对话事件回调");
    }

    private void OnEnable()
    {
        dialogueCallback += Callback;
    }

    private void OnDisable()
    {
        dialogueCallback -= Callback;
    }
}