using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PostProcessManager : MonoBehaviour
{
    public static PostProcessManager instance;
    
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
    
    public float waterWaveDuration = 1;

    private const float waterWaveSpeed = 0.01f;
    WaterWave waterWave
    {
        get
        {
            return Camera.main.gameObject.GetComponent<WaterWave>();
        }
    }

    GaussianBlur gaussianBlur
    {
        get
        {
            return Camera.main.gameObject.GetComponent<GaussianBlur>();
        }
    }

    public void UseWaterWave()
    {
        StartCoroutine(WaterWave());
    }

    IEnumerator WaterWave()
    {
        waterWave.offsetStrength = 0;
        waterWave.enabled = true;
        gaussianBlur.enabled = true;

        int iter = (int)(waterWaveDuration / waterWaveSpeed);
        for (int i = 0; i < iter; i++)
        {
            waterWave.offsetStrength += waterWaveSpeed;
            yield return new WaitForSeconds(waterWaveSpeed);
        }
        
        for (int i = 0; i < iter; i++)
        {
            waterWave.offsetStrength -= waterWaveSpeed;
            yield return new WaitForSeconds(waterWaveSpeed);
        }

        waterWave.offsetStrength = 0;
        waterWave.enabled = false;
        gaussianBlur.enabled = false;
    }
}
