using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DialogueSystemDebugger
{
    // [MenuItem("对话系统/Runtime下测试旁白对话")]
    // static void DebugNarratorDialogue()
    // {
    //     DialogueData dialogueData = new DialogueData() { dialogueInfos = new List<DialogueInfo>() };
    //     dialogueData.dialogueInfos.Add(new DialogueInfo()
    //         { characterName = "杰哥", characterSprite = null, dialogueContent = "你看这个彬彬啊，才喝几罐就醉了，真的太逊了。" });
    //     dialogueData.dialogueInfos.Add(new DialogueInfo()
    //         { characterName = "阿伟", characterSprite = null, dialogueContent = "这个彬彬就是逊呀！" });
    //     dialogueData.dialogueInfos.Add(new DialogueInfo()
    //         { characterName = "杰哥", characterSprite = null, dialogueContent = "听你这么说，你很勇哦？" });
    //     dialogueData.dialogueInfos.Add(new DialogueInfo()
    //         { characterName = "阿伟", characterSprite = null, dialogueContent = "开玩笑，我超勇的，超会喝的啦。" });
    //     dialogueData.dialogueInfos.Add(new DialogueInfo()
    //         { characterName = "阿伟", characterSprite = null, dialogueContent = "哎，杰哥，你干嘛啊。" });
    //
    //     EventHandler.CallShowDialogueEvent(dialogueData);
    // }

    [MenuItem("对话系统/Runtime下测试角色对话")]
    static void DebugRoleDialogue()
    {
        DialogueData dialogueData = new DialogueData() { dialogueInfos = new List<DialogueInfo>() };
        dialogueData.dialogueInfos.Add(new DialogueInfo()
            { characterName = "杰哥", characterSprite = null, dialogueContent = "你看这个彬彬啊，才喝几罐就醉了，真的太逊了。" });
        dialogueData.dialogueInfos.Add(new DialogueInfo()
            { characterName = "阿伟", characterSprite = null, dialogueContent = "这个彬彬就是逊呀！" });
        dialogueData.dialogueInfos.Add(new DialogueInfo()
            { characterName = "杰哥", characterSprite = null, dialogueContent = "听你这么说，你很勇哦？" });
        dialogueData.dialogueInfos.Add(new DialogueInfo()
            { characterName = "阿伟", characterSprite = null, dialogueContent = "开玩笑，我超勇的，超会喝的啦。开玩笑，我超勇的，超会喝的啦。开玩笑，我超勇的，超会喝的啦。开玩笑，我超勇的，超会喝的啦。开玩笑，我超勇的，超会喝的啦。开玩笑，我超勇的，超会喝的啦。开玩笑，我超勇的，超会喝的啦。" });
        dialogueData.dialogueInfos.Add(new DialogueInfo()
            { characterName = "阿伟", characterSprite = null, dialogueContent = "哎，杰哥，你干嘛啊。" });

        GameObject dialogueDebugger = new GameObject("Dialogue Debugger");
        DialogueController dialogueController = dialogueDebugger.AddComponent<DialogueController>();
        dialogueController.dialogueData = dialogueData;
        dialogueController.ShowDialogue();
        GameObject.DestroyImmediate(dialogueDebugger);
    }
}