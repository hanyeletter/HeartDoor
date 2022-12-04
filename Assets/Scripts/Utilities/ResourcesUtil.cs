using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourcesUtil
{
    public static Dictionary<string, DialogueData> GetAllDialogueDatas()
    {
        string loadPath = "DialogueDatas";
        DialogueData[] dialogueDatas = Resources.LoadAll<DialogueData>(loadPath);
        Dictionary<string, DialogueData> screenKey2DialogueDataDict = new Dictionary<string, DialogueData>();
        foreach (var dialogueData in dialogueDatas)
        {
            screenKey2DialogueDataDict.Add(dialogueData.screenKey, dialogueData);
        }
        return screenKey2DialogueDataDict;
    }
}
