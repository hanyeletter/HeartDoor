using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallOutside_DialogueController : DialogueController
{
    private void Awake()
    {
        curScreenKey = "序幕_HallOutside_1";
        if (!ScreenKey2DialogueDataDict.TryGetValue(curScreenKey, out dialogueData))
        {
            Debug.LogError(curScreenKey+"不存在。");
        }
    }
}
