Shader "Lynx/Demo/Intro_Effect"
{
    Properties
    {
        _OverlayColor("Overlay Color", Color) = (0.,0.,0.,1.)
        _Spawn("Spawn", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha // Alpha blending

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag alpha

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float easeExpo(float x)
            {
                return x*x*x*x;
                if(x == 0) return 0;
                return pow(2,10*x-10);
            }


            float4 _OverlayColor;
            float _Spawn;
            float TileSpawn;

            v2f vert (appdata v)
            {
                v2f o;
                TileSpawn = (v.uv.x + v.uv.y)/4.0 + (_Spawn);
                TileSpawn = clamp(TileSpawn,0.5,1.0);
                TileSpawn = (TileSpawn-0.5)*2;
                TileSpawn = easeExpo(TileSpawn);
                float3 vert = v.vertex;
                vert += v.normal * -TileSpawn;
                o.vertex = UnityObjectToClipPos(vert);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = _OverlayColor;
                col.w = clamp(3 - _Spawn*4,0,1);
                return col;
            }
            ENDCG
        }
    }
}
