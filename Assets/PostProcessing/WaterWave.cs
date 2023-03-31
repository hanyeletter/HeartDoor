using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaterWave : PostEffectsBase
{
    public Shader waterWaveShader;
    private Material waterWaveMaterial;

    public Material material
    {
        get
        {
            waterWaveMaterial = CheckShaderAndCreateMaterial(waterWaveShader, waterWaveMaterial);
            return waterWaveMaterial;
        }
    }

    [Header("水波频率")]
    [Range(30, 500)] public float waveFrequency = 200;
    [Header("水波波动速度")]
    [Range(0, 5)] public float waveSpeed = 5;
    [Header("折射强度")]
    [Range(0, 1f)] public float offsetStrength = 1f;
    [Header("水波半径")]
    [Range(0, 1f)] public float waveRadius = 0.2f;
    [Header("水波中心数")]
    [Range(1, 25)] public int waveCenterCount = 10;

    private List<Vector4> waveCenters;

    public List<Vector4> WaveCenters
    {
        //Halton低差异序列构建水波中心
        get
        {
            if (waveCenters == null || waveCenters.Count == 0 || waveCenters.Count != waveCenterCount)
            {
                waveCenters = new List<Vector4>(waveCenterCount);
                for (int i = 0; i < waveCenterCount; i++)
                {
                    waveCenters.Add(new Vector4(GetHaltonValue(i, 2), GetHaltonValue(i, 3), 0, 0));
                }
            }

            return waveCenters;
        }
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (material != null)
        {
            material.SetFloat("_WaveFrequency", waveFrequency);
            material.SetFloat("_WaveSpeed", waveSpeed);
            material.SetFloat("_OffsetStrength", offsetStrength);
            material.SetFloat("_WaveRadius", waveRadius);
            material.SetVectorArray("_WaveCenters", WaveCenters);
            material.SetInt("_WaveCenterCount", waveCenterCount);
            Graphics.Blit(src, dest, material);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }

    private float GetHaltonValue(int index, int baseNum)
    {
        float fraction = 1;
        float result = 0;

        while (index > 0)
        {
            fraction /= baseNum;
            result += fraction * (index % baseNum);
            index /= baseNum;
        }

        return result;
    }
}