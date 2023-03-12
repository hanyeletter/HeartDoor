using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DialogueSystemDebugger
{
    [MenuItem("对话系统/Runtime下测试角色对话")]
    static void DebugRoleDialogue()
    {
        DialogueData dialogueData = null;
        GameObject dialogueDebugger = new GameObject("Dialogue Debugger");
        DialogueController dialogueController = dialogueDebugger.AddComponent<DialogueController>();
        dialogueData = Resources.Load<DialogueData>("DialogueDatas/第一章_x_2");
        dialogueController.dialogueData = dialogueData;
        dialogueController.curScreenKey = dialogueData.screenKey;
        dialogueController.ShowDialogue();
        GameObject.DestroyImmediate(dialogueDebugger);
    }
}