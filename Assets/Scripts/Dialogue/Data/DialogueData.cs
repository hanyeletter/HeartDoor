using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/DialogueData")]
public class DialogueData : ScriptableObject
{
    public string screenKey;
    public List<DialogueInfo> dialogueInfos;
}