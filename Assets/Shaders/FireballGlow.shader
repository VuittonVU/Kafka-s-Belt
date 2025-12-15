Shader "Custom/FireballGlow"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (1,0.5,0,1)
        _EmissionColor ("Emission Color", Color) = (1,0.3,0,1)
        _EmissionStrength ("Emission Strength", Range(0,5)) = 1
        _PulseSpeed ("Pulse Speed", Range(0,10)) = 3
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #include "UnityCG.cginc" 

        float4 _MainColor;
        float4 _EmissionColor;
        float _EmissionStrength;
        float _PulseSpeed;

        struct Input
        {
            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = _MainColor.rgb;

            float pulse = (sin(_Time.y * _PulseSpeed) * 0.5 + 0.5);

            float emissionStr = pulse * _EmissionStrength;

            o.Emission = _EmissionColor.rgb * emissionStr;
        }
        ENDCG
    }

    FallBack "Standard"
}
