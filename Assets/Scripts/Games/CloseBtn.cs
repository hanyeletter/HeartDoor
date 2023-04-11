using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseBtn : MonoBehaviour
{
    SmallGameManager smallGameManager = new SmallGameManager();
    public Button closeBtn;
    public string gameName;

    public void CloseTheGame()
    {
        closeBtn.gameObject.SetActive(false);
        smallGameManager.CloseASmallGame(gameName);
    }
}
