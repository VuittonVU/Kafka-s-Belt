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
        Tags { "RenderType" = "Opaque" }//utuh tidak transparan
        LOD 200//kualitas

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows//pakai lighting standard, bayangan tetap dihitung walau forward rendering
        #include "UnityCG.cginc"

        fixed4 _SkinColor;//rgba
        fixed4 _ShadowColor;
        float _ShadowThreshold;

        struct Input
        {
            float3 worldNormal;//normal permukaan di world space
            float3 worldPos;//posisi pixel dalam world space
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float3 N = normalize(IN.worldNormal);//arah mana wajah permukaan menghadap
            float3 L = normalize(_WorldSpaceLightPos0.xyz);//arah cahaya matahari 

            float NdotL = saturate(dot(N, L));//seberapa terang permukaan nilai antara 0-1

            float3 toonColor = (NdotL < _ShadowThreshold)
                              ? _ShadowColor.rgb
                              : _SkinColor.rgb;//jika nilai cahaya kurang dari threshold maka jadi shadow, kalau lebih terang

            o.Albedo = toonColor;

            o.Metallic = 0;
            o.Smoothness = 0.1;//sedikit refleksi 
        }
        ENDCG
    }

    FallBack "Standard"
}


