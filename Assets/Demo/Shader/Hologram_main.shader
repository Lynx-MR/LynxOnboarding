Shader "Lynx/Unlit/Hologram_main"
{
    Properties
    {
        _OverlayColor("Overlay Color", Color) = (0.5,0.7,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _Opacity("Opacity", Range(0,1)) = 0.5
        _StrippesIntensity("Strippes Intensity", Range(0,1)) = 0.5
        _OverlayIntensity("Overlay Intensity", Range(0,1)) = 0.5
        _BigStripesSpeed("bigStripes Speed", Range(0,5)) = 2.
        _SideOpacity("Side Opacity", Range(0,1)) = 1.
        _sideRatio("Side Ratio", float) = 0.1
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
            // make fog work
            #pragma multi_compile_fog

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

            #define PI 3.1415926538
            sampler2D _MainTex;
            float4 _MainTex_ST;
            half _Opacity;
            half _StrippesIntensity;
            half _OverlayIntensity;
            half _BigStripesSpeed;
            float4 _OverlayColor;
            float _SideOpacity;
            

            float randomCurve(float x)
            {
                float xA = sin(x * 3.423);
                float xB = sin(x * 5.506);
                float xC = sin(x * 13.1943);
                float xD = sin(x * 19.8432);
                return(xA + xB + xC + xD) * 0.25;
            }

            float makeStripes(float position, fixed2 uv, float size, float thickness)
            {
                float stripes = ((uv.y - position) + uv.x * 0.01) * size % 1.;
                stripes = sin(stripes * PI);
                stripes = smoothstep(1.- thickness, 1., abs(stripes));
                return stripes;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture & multiply with the input opacity 
                fixed4 col = tex2D(_MainTex, i.uv);
                col.w = col.w * _Opacity;

                //make small stripes that go up with a slight angle for a nicer effect on the sides 
                float smallStripes = makeStripes(_Time, i.uv, 40. ,0.5);

                // 2 Big stripes added to the smalls for a scan effect that go up&down in a random speed
                float bigStripesA = makeStripes(randomCurve(_Time * _BigStripesSpeed), i.uv, 0.75, 0.1);
                float bigStripesB = makeStripes(randomCurve(64.+_Time * _BigStripesSpeed), i.uv, 0.6, 0.1);
                float bigStripes = max(bigStripesA, bigStripesB);

                //add small stripes with big strips and tint them with the overlay color
                float4 holoStripes = _OverlayColor * (bigStripes + smallStripes);

                // set the alpha to 0 to not modify the output alpha
                holoStripes.w = 0.; 
                _OverlayColor.w = 0.;

                //mix the different effects & clamp the result to avoid color glitch
                col += _OverlayColor * _OverlayIntensity;
                col += holoStripes * _StrippesIntensity;
                col = clamp(col, 0., 1.);
                
                return col;
            }
            ENDCG
        }
    }
}

