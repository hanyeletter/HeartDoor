using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    public GameObject dialogueCanvas;
    public Text characterNameText;
    public Image characterImage;
    public Text dialogueContentText;

    private int currentIndex;
    private DialogueData dialogueData;
    private bool isScrolling;
    private Action dialogueCallback;

    [Header("Text Speed")] [SerializeField]
    private float textSpeed = 0.02f;

    private void OnEnable()
    {
        EventHandler.ShowDialogueEvent += ShowDialogue;
        EventHandler.BeginDialogueEvent += OnBeginDialogue;
        EventHandler.EndDialogueEvent += OnEndDialogue;
    }

    private void OnDisable()
    {
        EventHandler.ShowDialogueEvent -= ShowDialogue;
        EventHandler.BeginDialogueEvent -= OnBeginDialogue;
        EventHandler.EndDialogueEvent -= OnEndDialogue;
    }

    private void Start()
    {
        dialogueCanvas.SetActive(false);
    }

    private void Update()
    {
        if (dialogueCanvas.activeInHierarchy)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (!isScrolling)
                {
                    currentIndex++;
                    if (currentIndex < dialogueData.dialogueInfos.Count)
                    {
                        characterNameText.text = dialogueData.dialogueInfos[currentIndex].characterName;
                        characterImage.sprite = dialogueData.dialogueInfos[currentIndex].CharacterSprite;
                        StartCoroutine("ScrollingText");
                    }
                    else
                    {
                        dialogueCanvas.SetActive(false);
                        //对话结束后触发事件
                        EventHandler.CallEndDialogueEvent();
                    }
                }
                else
                {
                    StopCoroutine("ScrollingText");
                    isScrolling = false;
                    if (currentIndex < dialogueData.dialogueInfos.Count)
                    {
                        characterNameText.text = dialogueData.dialogueInfos[currentIndex].characterName;
                        characterImage.sprite = dialogueData.dialogueInfos[currentIndex].CharacterSprite;
                        dialogueContentText.text = dialogueData.dialogueInfos[currentIndex].dialogueContent;
                    }
                    else
                    {
                        dialogueCanvas.SetActive(false);
                        //对话结束后触发事件
                        EventHandler.CallEndDialogueEvent();
                    }
                }
            }
        }
    }

    private void ShowDialogue(DialogueData dialogueData, Action dialogueCallback)
    {
        if (dialogueCanvas.activeInHierarchy)
        {
            Debug.LogError("当前正在对话中，无法执行新对话。");
            return;
        }

        this.dialogueData = dialogueData;
        if (this.dialogueData == null || this.dialogueData.dialogueInfos.Count == 0)
        {
            Debug.LogError("当前对话无任何内容，无法执行对话。");
            return;
        }

        //开始对话前，触发对话前事件
        EventHandler.CallBeginDialogueEvent();
        //设置回调函数
        this.dialogueCallback = dialogueCallback;
        currentIndex = 0;
        characterImage.sprite = dialogueData.dialogueInfos[currentIndex].CharacterSprite;
        characterNameText.text = dialogueData.dialogueInfos[currentIndex].characterName;
        StartCoroutine("ScrollingText");
        dialogueCanvas.SetActive(true);
    }

    private IEnumerator ScrollingText()
    {
        isScrolling = true;
        dialogueContentText.text = "";
        foreach (var letter in dialogueData.dialogueInfos[currentIndex].dialogueContent.ToCharArray())
        {
            dialogueContentText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        isScrolling = false;
    }

    private void OnBeginDialogue()
    {
        Debug.Log("对话开始");
        //对话开始前，触发游戏状态进入暂停的事件
        EventHandler.CallGameStateChangeEvent(GameState.PAUSE);
    }

    private void OnEndDialogue()
    {
        Debug.Log("对话结束");
        //对话结束后，触发游戏状态进入GamePlay的事件
        EventHandler.CallGameStateChangeEvent(GameState.GAME_PLAY);
        //对话结束后，触发回调
        dialogueCallback?.Invoke();
    }
}