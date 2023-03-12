using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintUIManager : MonoBehaviour
{
    public static HintUIManager instance;

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
    
    [Header("HintTip")] public GameObject hintTip;

    public Text hintTipText;

    private void OnEnable()
    {
        EventHandler.AddItemEvent += OnAddItemEvent;
    }

    private void OnDisable()
    {
        EventHandler.AddItemEvent -= OnAddItemEvent;
    }

    private void OnAddItemEvent(ItemName itemName)
    {
        ShowAddItemTip(itemName);
    }

    //目前考虑打断上一次携程
    private void ShowAddItemTip(ItemName itemName)
    {
        StartCoroutine(ShowHintTip(String.Format("【得到线索：{0}】", itemName), 3, null));
    }
        
    private IEnumerator ShowHintTip(string context, float duration, Action callBack)
    {
        hintTipText.text = context;
        hintTip.SetActive(true);
        yield return new WaitForSeconds(duration);
        hintTip.SetActive(false);
        hintTipText.text = "";
        callBack?.Invoke();
    }
}
