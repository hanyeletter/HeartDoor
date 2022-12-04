using UnityEngine;

[System.Serializable]
public class DialogueInfo
{
    public string characterName;
    private Sprite characterSprite;

    public Sprite CharacterSprite
    {
        get
        {
            if (characterSprite == null)
            {
                characterSprite = Resources.Load<Sprite>("DialogueImages/" + characterName);
            }

            return characterSprite;
        }
    }
    public string dialogueContent;

    public static DialogueInfo emptyDialogueInfo = new DialogueInfo();
}