using System;

public static class EventHandler
{
    public static event Action<DialogueData, Action> ShowDialogueEvent;

    public static void CallShowDialogueEvent(DialogueData dialogueInfo, Action dialogueCallback = null)
    {
        ShowDialogueEvent?.Invoke(dialogueInfo, dialogueCallback);
    }

    public static event Action BeginDialogueEvent;

    public static void CallBeginDialogueEvent()
    {
        BeginDialogueEvent?.Invoke();
    }

    public static event Action EndDialogueEvent;

    public static void CallEndDialogueEvent()
    {
        EndDialogueEvent?.Invoke();
    }

    public static event Action<GameState> GameStateChangeEvent;

    public static void CallGameStateChangeEvent(GameState gameState)
    {
        GameStateChangeEvent?.Invoke(gameState);
    }

    public static event Action BeginSceneTransitionEvent;

    public static void CallBeginSceneTransitionEvent()
    {
        BeginDialogueEvent?.Invoke();
    }
    
    public static event Action EndSceneTransitionEvent;

    public static void CallEndSceneTransitionEvent()
    {
        EndSceneTransitionEvent?.Invoke();
    }
}