Shader "Custom/WaterWave"
{
    Properties
    {
        _MainTex("Base (RGB)",2D) = "white"{}
        _WaveFrequency("Wave Frequency", Range(30,500)) = 100
        _WaveSpeed("Wave Speed", Range(0,5)) = 1
        _OffsetStrength("Offset Strength",Range(0, 1)) = 0.05
        _WaveRadius("Wave Radius",Range(0, 1)) = 0.05
    }
    SubShader
    {
        Pass
        {
            ZTest Always
            Cull Off
            Zwrite Off

            CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 4.0

            #define MAX_WAVE_CENTER_COUNT 25

            sampler2D _MainTex;
            float4 _MainTex_ST;
            uniform half4 _MainTex_TexelSize;
            float _WaveFrequency;
            float _WaveSpeed;
            float _OffsetStrength;
            float _WaveRadius;
            float4 _WaveCenters[MAX_WAVE_CENTER_COUNT];
            int _WaveCenterCount;

            struct appdata
            {
                float4 vertex:POSITION;
                float2 uv:TEXCOORD0;
            };

            struct v2f
            {
                float2 uv:TEXCOORD0;
                float4 vertex:SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            float2 GetWavedUVOffset(float2 waveCenter, float2 uv)
            {
                float2 distanceVector = uv - waveCenter;
                //distance需要按屏幕比例转换
                distanceVector.y *= (_MainTex_TexelSize.x / _MainTex_TexelSize.y);
                float distance = sqrt(distanceVector.x * distanceVector.x + distanceVector.y * distanceVector.y);
                float sinFactor = sin(distance * _WaveFrequency - _Time.y * _WaveSpeed);
                float2 direction = normalize(distanceVector);
                float2 offset = direction * sinFactor;
                
                float waveAffect = smoothstep(0, 1, _WaveRadius - distance);
                
                return offset * _OffsetStrength * waveAffect;
            }

            fixed4 frag(v2f i):SV_Target
            {
                float2 uv = i.uv;
                for (int j = 0; j < _WaveCenterCount; j++)
                {
                    uv += (GetWavedUVOffset(float2(_WaveCenters[j].x, _WaveCenters[j].y), i.uv));
                }

                return tex2D(_MainTex, uv);
            }
            ENDCG
        }
    }
}