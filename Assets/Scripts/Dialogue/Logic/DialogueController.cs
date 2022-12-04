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

    public static Dictionary<string, Action> screenKey2CallbackDict;
    
    [HideInInspector]
    public DialogueData dialogueData = null;
    private Action DialogueCallback = null;

    public void ShowDialogue()
    {
        EventHandler.CallShowDialogueEvent(dialogueData, DialogueCallback);
    }
        
    protected virtual void Callback()
    {
        Debug.Log("触发对话事件回调");
    }

    private void OnEnable()
    {
        DialogueCallback += Callback;
    }

    private void OnDisable()
    {
        DialogueCallback -= Callback;
    }
}