Shader "Custom/NebulaShader2D"
{
    Properties
    {
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _Color1 ("Color A", Color) = (0.3, 0.1, 0.6, 1)
        _Color2 ("Color B", Color) = (0.1, 0.8, 0.9, 1)
        _Color3 ("Color C", Color) = (0.9, 0.3, 0.1, 1)
        _Threshold1 ("Threshold A->B", Range(0,1)) = 0.33
        _Threshold2 ("Threshold B->C", Range(0,1)) = 0.66
        _BlendRange ("Blend Range", Range(0.01,0.5)) = 0.1
        _Intensity ("Color Intensity", Float) = 1.2
        _EdgeFade ("Edge Softness", Range(0,1)) = 0.6
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _NoiseTex;
            float4 _NoiseTex_ST;
            float4 _Color1;
            float4 _Color2;
            float4 _Color3;
            float _Threshold1;
            float _Threshold2;
            float _BlendRange;
            float _Intensity;
            float _EdgeFade;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _NoiseTex);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float2 centeredUV = i.uv * 2.0 - 1.0;
                float dist = length(centeredUV);
                float edgeAlpha = smoothstep(1.0, _EdgeFade, dist);

                float noise = tex2D(_NoiseTex, i.uv).r;

                float3 colA = _Color1.rgb;
                float3 colB = _Color2.rgb;
                float3 colC = _Color3.rgb;

                float a = smoothstep(_Threshold1 - _BlendRange, _Threshold1 + _BlendRange, noise);
                float b = smoothstep(_Threshold2 - _BlendRange, _Threshold2 + _BlendRange, noise);

                float3 abBlend = lerp(colA, colB, a);
                float3 bcBlend = lerp(colB, colC, b);

                float3 color = (noise < _Threshold1) ? colA :
                               (noise < _Threshold2) ? abBlend :
                               bcBlend;

                float alpha = saturate(noise * 1.5) * edgeAlpha;

                return float4(color * _Intensity, alpha);
            }
            ENDCG
        }
    }
}
