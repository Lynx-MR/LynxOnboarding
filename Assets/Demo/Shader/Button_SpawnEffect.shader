Shader "Lynx/UI/ButtonSpawnEffect"
{
    Properties
    {
        _Color("MainColor", Color) = (1,1,1,1)
        _Spawn("EffectValue", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        LOD 100
        Blend one one // Alpha Add
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"


            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _Color;
            float _Spawn;
            float pi = 3.141592653589793238462;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float pi = 3.141592653589793238462;
                float4 col = _Color;
                float grad = distance(i.uv, 0.5)*2;
                grad = 1-grad;
                grad = clamp(grad,0,1);

                float sinWave = grad * pi;
                float spawnVal = (_Spawn * 2 * pi)-pi;
                sinWave = sin(sinWave+spawnVal);

                col.xyz = col.xyz*(grad*sinWave);

                return col;
            }
            ENDCG
        }
    }
}
