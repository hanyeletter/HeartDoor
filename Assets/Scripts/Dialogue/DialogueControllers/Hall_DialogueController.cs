using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hall_DialogueController : DialogueController
{
    private void Awake()
    {
        curScreenKey = "序幕_Hall_1";
        if (!ScreenKey2DialogueDataDict.TryGetValue(curScreenKey, out dialogueData))
        {
            Debug.LogError(curScreenKey+"不存在。");
        }
        
        //注册回调1
        screenKey2CallbackDict.Add("序幕_Hall_1",Callback_1);
    }
    
    private void Callback_1()
    {
        StartCoroutine(ICallback_1());
    }

    private IEnumerator ICallback_1()
    {
        yield return new WaitForSeconds(1);
        TransitionManager.instance.Transition("Hall", "HallOutside");
    }
}
