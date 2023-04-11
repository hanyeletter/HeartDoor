using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTrigger : MonoBehaviour
{
    private SmallGameManager smallGameManager = new SmallGameManager();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Red")
        {
            smallGameManager.FinishASmallGame("Huarongdao");
        }
    }
}
