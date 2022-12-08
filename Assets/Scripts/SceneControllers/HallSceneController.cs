using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallSceneController : MonoBehaviour
{
    public SpriteRenderer Fupei;
    public Sprite FupeiSit;

    public void Sit()
    {
        Fupei.sprite = FupeiSit;
        //Fupei.transform.localPosition += new Vector3(0, 0.8f, 0);
        Fupei.transform.localScale *= 0.75f;
    }
}
