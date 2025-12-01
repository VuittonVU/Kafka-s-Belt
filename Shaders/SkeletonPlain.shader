Shader "Custom/SkeletonPlain"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        CGPROGRAM
        #pragma surface surf Standard
        #include "UnityCG.cginc"

        float4 _Color;

        struct Input
        {
            float3 worldPos;   // harus ada minimal satu field
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = _Color.rgb;
            o.Metallic = 0;
            o.Smoothness = 0.1;
        }
        ENDCG
    }

    FallBack "Standard"
}
