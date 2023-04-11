using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SmallGameManager : MonoBehaviour
{
    public List<string> gameNameList = new List<string>();
    public List<Button> openButtons = new List<Button>();
    public List<Button> closeButtons = new List<Button>();
    public static Dictionary<string, bool> gameEndStatusDic = new Dictionary<string, bool>();
    public static Dictionary<string, Button> openButtonsDic = new Dictionary<string, Button>();
    public static Dictionary<string, Button> closeButtonsDic = new Dictionary<string, Button>();
    public static SmallGameManager instance;
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
        SmallGamesInit();
    }

    private void SmallGamesInit()
    {
        for (int i = 0; i < gameNameList.Count; i++)
            gameEndStatusDic.Add(gameNameList[i], false);
        for (int i = 0; i < openButtons.Count; i++)
            openButtonsDic.Add(gameNameList[i], openButtons[i]);
        for (int i = 0; i < closeButtons.Count; i++)
            closeButtonsDic.Add(gameNameList[i], closeButtons[i]);
    }

    public void StartASmallGame(string gameName)
    {
        if (gameEndStatusDic[gameName] == false)
        {
            if (!SceneManager.GetSceneByName(gameName).isLoaded)
            {
                SetTheCloseBtn(gameName, true);
                SetTheOpenBtn(gameName, false);
                SceneManager.LoadSceneAsync(gameName, LoadSceneMode.Additive);
            }
        }  
        else
            Debug.Log("This game has been finished!");
    }

    public void FinishASmallGame(string gameName)
    {
        SetTheCloseBtn(gameName, false);
        SetTheOpenBtn(gameName, true);
        SceneManager.UnloadSceneAsync(gameName);
        gameEndStatusDic[gameName] = true;
    }

    public void CloseASmallGame(string gameName)
    {
        SetTheCloseBtn(gameName, false);
        SetTheOpenBtn(gameName, true);
        SceneManager.UnloadSceneAsync(gameName);  
    }

    public void SetTheCloseBtn(string gameName, bool status)
    {
        closeButtonsDic[gameName].gameObject.SetActive(status);
    }

    public void SetTheOpenBtn(string gameName, bool status)
    {
        openButtonsDic[gameName].gameObject.SetActive(status);
    }

}
