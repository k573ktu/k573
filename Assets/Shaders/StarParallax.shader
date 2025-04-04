Shader "Custom/StarParallax"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ParallaxFactor ("Parallax Factor", Range(0, 0.1)) = 0.02
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float _ParallaxFactor;
            float3 _CameraPos;

            v2f vert (appdata_t v)
            {
                v2f o;
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                float2 parallaxOffset = (_CameraPos.xy - worldPos.xy) * _ParallaxFactor;
                o.uv = v.uv + parallaxOffset;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.uv);
                return texColor * i.color;
            }
            ENDCG
        }
    }
}
