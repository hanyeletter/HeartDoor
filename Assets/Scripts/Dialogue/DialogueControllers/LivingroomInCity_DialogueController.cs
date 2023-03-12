using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LivingroomInCity_DialogueController : DialogueController
{
    public PlayableDirector ahead;
    public PlayableDirector behind;
    
    private void Awake()
    {
        curScreenKey = "序幕_HallOutside_1";
        if (!ScreenKey2DialogueDataDict.TryGetValue(curScreenKey, out dialogueData))
        {
            Debug.LogError(curScreenKey+"不存在。");
        }

        screenKey2CallbackDict.Add("第一章_LivingroomInCity_1", Callback_1);
        screenKey2CallbackDict.Add("第一章_LivingroomInCity_2", Callback_2);
        screenKey2CallbackDict.Add("第一章_LivingroomInCity_3", Callback_2);
    }
    
    private void SetCurScreenKey(string str)
    {
        curScreenKey = str;
        if (!ScreenKey2DialogueDataDict.TryGetValue(curScreenKey, out dialogueData))
        {
            Debug.LogError(curScreenKey + "不存在。");
        }
    }

    private void Callback_1()
    {
        
    }
    

    private void Callback_2()
    {

    }
    
    private void Callback_3()
    {
        
    }
}
