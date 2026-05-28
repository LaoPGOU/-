Shader "Unlit/OceanURP_Final"
{
    Properties
    {
        _Color ("水颜色", Color) = (0.15, 0.4, 0.6, 0.8)
        _WaveHeight ("浪高", Range(0, 0.3)) = 0.1
        _WaveSpeed ("速度", Range(0.1, 3)) = 1.2
        _WaveDensity ("密度", Range(0.5, 3)) = 1.5
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }
        LOD 100
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            float4 _Color;
            float _WaveHeight;
            float _WaveSpeed;
            float _WaveDensity;

            v2f vert (appdata v)
            {
                v2f o;

                // 真正的波浪：顶点上下动
                float wave = sin(v.vertex.x * _WaveDensity + _Time.y * _WaveSpeed) * _WaveHeight
                           + cos(v.vertex.z * _WaveDensity + _Time.y * _WaveSpeed * 0.8f) * _WaveHeight * 0.5f;

                v.vertex.y += wave;
                o.vertex = UnityObjectToClipPos(v.vertex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
}