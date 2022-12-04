using System;

public static class EventHandler
{
    public static event Action<DialogueData, Action> ShowDialogueEvent;

    public static void CallShowDialogueEvent(DialogueData dialogueInfo, Action dialogueCallback = null)
    {
        ShowDialogueEvent?.Invoke(dialogueInfo, dialogueCallback);
    }

    public static event Action BeforeDialogueStartEvent;

    public static void CallBeforeDialogueStartEvent()
    {
        BeforeDialogueStartEvent?.Invoke();
    }

    public static event Action AfterDialogueEndEvent;

    public static void CallAfterDialogueEndEvent()
    {
        AfterDialogueEndEvent?.Invoke();
    }

    public static event Action<GameState> GameStateChangeEvent;

    public static void CallGameStateChangeEvent(GameState gameState)
    {
        GameStateChangeEvent?.Invoke(gameState);
    }
}