Shader "Custom/WizardSkin"
{
    Properties
    {
        _SkinColor ("Skin Color", Color) = (0.9, 0.8, 0.7, 1)
        _ShadowColor ("Shadow Color", Color) = (0.5, 0.4, 0.3, 1)
        _ShadowThreshold ("Shadow Threshold", Range(0,1)) = 0.5
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #include "UnityCG.cginc"

        fixed4 _SkinColor;
        fixed4 _ShadowColor;
        float _ShadowThreshold;

        struct Input
        {
            float3 worldNormal;
            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // NORMAL & LIGHT DIR
            float3 N = normalize(IN.worldNormal);
            float3 L = normalize(_WorldSpaceLightPos0.xyz);

            // Dot product untuk shading
            float NdotL = saturate(dot(N, L));

            // Toon shading: jika cahaya di bawah threshold → shadow
            float3 toonColor = (NdotL < _ShadowThreshold)
                              ? _ShadowColor.rgb
                              : _SkinColor.rgb;

            // Set warna
            o.Albedo = toonColor;

            // Optional: tambahkan sedikit smoothness
            o.Metallic = 0;
            o.Smoothness = 0.1;
        }
        ENDCG
    }

    FallBack "Standard"
}
