Shader "Custom/SkeletonHitFlash"
{
    Properties
    {
        _Color ("Base Color", Color) = (1,1,1,1)
        _HitColor ("Hit Flash Color", Color) = (1,0,0,1)
        _HitStrength ("Hit Strength", Range(0,1)) = 0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 150

        CGPROGRAM
        #pragma surface surf Standard
        #include "UnityCG.cginc"

        float4 _Color;
        float4 _HitColor;
        float _HitStrength;

        struct Input
        {
            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float3 baseCol = _Color.rgb;

            // lerp = blend 2 warna
            float3 finalCol = lerp(baseCol, _HitColor.rgb, _HitStrength);

            o.Albedo = finalCol;
            o.Metallic = 0;
            o.Smoothness = 0.1;
        }
        ENDCG
    }

    FallBack "Standard"
}
