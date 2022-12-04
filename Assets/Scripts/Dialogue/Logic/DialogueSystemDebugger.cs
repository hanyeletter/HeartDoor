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
        dialogueData = Resources.Load<DialogueData>("DialogueDatas/Stage_1_Hall_1");
        dialogueController.dialogueData = dialogueData;
        dialogueController.ShowDialogue();
        GameObject.DestroyImmediate(dialogueDebugger);
    }
}