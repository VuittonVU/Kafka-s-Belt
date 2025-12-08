Shader "Custom/GroundDark"
{
    Properties
    {
        _Color ("Ground Color", Color) = (0.12, 0.12, 0.12, 1) // abu gelap
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 110

        CGPROGRAM
        #pragma surface surf Standard
        #include "UnityCG.cginc"

        float4 _Color;

        struct Input
        {
            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Warna gelap simple
            o.Albedo = _Color.rgb;

            // Ground matte → tidak mengkilap
            o.Metallic = 0;
            o.Smoothness = 0.05;
        }
        ENDCG
    }

    FallBack "Standard"
}
