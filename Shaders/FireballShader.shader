Shader "Custom/FireballRedOrangeGlow"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}

        _ColorLow("Low Heat Color", Color) = (1.0, 0.3, 0.05, 1)   
        _ColorHigh("High Heat Color", Color) = (1.0, 0.05, 0.0, 1)   

        _EmissionLow("Low Emission", Color) = (1.2, 0.5, 0.1, 1)
        _EmissionHigh("High Emission", Color) = (2.2, 0.1, 0.0, 1)

        _GlowIntensity("Glow Intensity", Range(0,10)) = 2
        _NoiseSpeed("Noise Speed", Range(0,10)) = 2
        _NoiseScale("Noise Scale", Range(1,10)) = 5
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;

            float4 _ColorLow;
            float4 _ColorHigh;

            float4 _EmissionLow;
            float4 _EmissionHigh;

            float _GlowIntensity;
            float _NoiseSpeed;
            float _NoiseScale;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
  
                float noise = sin(_Time.y * _NoiseSpeed +
                                  i.uv.x * _NoiseScale +
                                  i.uv.y * _NoiseScale);

                float t = saturate(noise * 0.5 + 0.5);

                float4 baseColor = lerp(_ColorLow, _ColorHigh, t);

                float4 emission = lerp(_EmissionLow, _EmissionHigh, t) * _GlowIntensity;

                float4 tex = tex2D(_MainTex, i.uv);
                float4 finalColor = tex * baseColor;

                finalColor.rgb += emission.rgb;

                return finalColor;
            }
            ENDCG
        }
    }
}
