using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class HallOutside_DialogueController : DialogueController
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

        screenKey2CallbackDict.Add("序幕_HallOutside_1", Callback_1);
        screenKey2CallbackDict.Add("序幕_HallOutside_2", Callback_2);
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
        CallBehindTimeline();
    }

    private void CallBehindTimeline()
    {
        ahead.extrapolationMode = DirectorWrapMode.Hold;    //先切换成超过持续时间后保持最后一帧的模式
        ahead.time = 1000;
        SetCurScreenKey("序幕_HallOutside_2");    //对话设置为第二段
        behind.Play();
    }

    private void Callback_2()
    {
        StartCoroutine(ICallback_2());
    }

    private IEnumerator ICallback_2()
    {
        yield return new WaitForSeconds(1);
        TransitionManager.instance.Transition("HallOutside", "UnderTheTree");
    }

}
