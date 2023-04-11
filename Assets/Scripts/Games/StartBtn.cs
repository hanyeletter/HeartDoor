using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartBtn : MonoBehaviour
{
    SmallGameManager smallGameManager = new SmallGameManager();
    public Button closeBtn;
    public string gameName;

    public void StartTheGame()
    {
        smallGameManager.StartASmallGame(gameName);
        closeBtn.gameObject.SetActive(true);
    }
}
