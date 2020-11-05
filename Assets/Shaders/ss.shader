Shader "Custom/ss"
{
    Properties
    {
        _Color ("Color Tint", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags{
            "RenderType" = "Opaque"
        }

        CGPROGRAM
        
        #pragma surface surf Lambert

        fixed4 _Color;

        struct Input{
            float3 worldPos;
        };

        void surf(Input IN, inout SurfaceOutput o){
            o.Albedo = _Color;
        }

        ENDCG
    }
    FallBack "Diffuse"
}
