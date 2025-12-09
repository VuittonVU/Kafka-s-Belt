Shader "Custom/StaffBrown"
{
    Properties
    {
        _Color ("Color", Color) = (0.45, 0.30, 0.18, 1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 50

        Pass
        {
            CGPROGRAM
            #pragma vertex vert//memproses posisi vertex 
            #pragma fragment frag//menentukan warna pixel 

            float4 _Color;

            struct appdata
            {
                float4 vertex : POSITION;//Input dari mesh hanya posisi vertex yang dikirim.
            };

            struct v2f
            {
                float4 pos : SV_POSITION;//Hanya menyimpan posisi tertransformasi (clip space) agar GPU tahu di mana meletakkan pixel.
            };//digunakan supaya GPU tahu di mana vertex seharusnya muncul di layar berdasarkan posisi kamera.

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);//mengubah posisi vertex dari object space → world space → view space → clip space.
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _Color;  
            }
            ENDCG
        }
    }

    FallBack Off
}

