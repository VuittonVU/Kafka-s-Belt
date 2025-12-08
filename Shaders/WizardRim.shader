Shader "Custom/WizardRim"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (0.6, 0.6, 1, 1)
        _RimColor ("Rim Color", Color) = (0.3, 0.6, 1, 1)
        _RimPower ("Rim Power", Range(1, 8)) = 3
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #include "UnityCG.cginc"

        float4 _BaseColor;
        float4 _RimColor;
        float _RimPower;

        struct Input
        {
            float3 viewDir;     // otomatis dari Surface Shader
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = _BaseColor.rgb;

            // Rimlight calculation WITHOUT UnityObjectToWorldNormal
            float rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
            rim = pow(rim, _RimPower);

            o.Emission = _RimColor.rgb * rim;
        }
        ENDCG
    }

    FallBack "Standard"
}
