Shader "Custom/BlackHole2D_Animated_Color"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _HoleRadius ("Hole Radius", Float) = 0.3
        _DistortionStrength ("Distortion Strength", Float) = 0.2
        _PulseSpeed ("Pulse Speed", Float) = 2.0
        _WarpSpeed ("Warp Speed", Float) = 4.0
        _Color ("Tint Color", Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _HoleRadius;
            float _DistortionStrength;
            float _PulseSpeed;
            float _WarpSpeed;
            float4 _Color;
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
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            float2 distortUV(float2 uv, float2 center, float radius, float strength, float time, float warpSpeed)
            {
                float2 offset = uv - center;
                float dist = length(offset);
                if (dist < radius)
                {
                    float wave = sin((dist * warpSpeed - time) * 6.2831);
                    float distortion = (1.0 - dist / radius) * strength * (1.0 + wave * 0.2);
                    offset *= 1.0 + distortion;
                }
                return center + offset;
            }
            fixed4 frag (v2f i) : SV_Target
            {
                float time = _Time.y;
                float pulse = sin(time * _PulseSpeed) * 0.05;
                float animatedRadius = _HoleRadius + pulse;
                float2 center = float2(0.5, 0.5);
                float2 uv = distortUV(i.uv, center, animatedRadius, _DistortionStrength, time, _WarpSpeed);
                fixed4 texColor = tex2D(_MainTex, uv);
                return texColor * _Color;
            }
            ENDCG
        }
    }
}
