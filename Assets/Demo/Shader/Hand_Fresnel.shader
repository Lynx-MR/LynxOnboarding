Shader "Lynx/Hand/Fresnel"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ValidationColor("Validation Color", Color) = (1,1,1,1)
        _ValidationValue("Validation Value", Range(0,1)) = 0
        _Color ("Base Color", Color) = (1,1,1,1)
        _FresnelPower ("Fresnel Power", Range(0,5)) = 1
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        LOD 100
        ZWrite On
        Cull Back
        Blend SrcAlpha OneMinusSrcAlpha // Alpha blending

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog
            #include "UnityCG.cginc"

            float Unity_FresnelEffect_float(float3 nrm, float3 ViewDir, float Power)
            {
				return pow((1.0 - saturate(dot(normalize(nrm), normalize(ViewDir)))), Power);
			}


            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float3 normal : NORMAL;
                float3 objPos : TEXCOORD2;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _ValidationColor;
            float4 _MainTex_ST;
            float4 _Color;
            float _FresnelPower;
            float _ValidationValue;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.objPos = v.vertex;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                o.normal = worldNormal;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                half3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
                fixed4 col = tex2D(_MainTex, i.uv);

                //get Fresnel value
                float fnl = Unity_FresnelEffect_float(i.normal,worldViewDir,_FresnelPower);

                // apply fog
                float3 pos = i.objPos;

                // validationEffect calculation
                float grad = abs(pos.x)-_ValidationValue/2;
                float validIntensity = 1-abs(1-(_ValidationValue*2));
                float validation = sin(grad*30)*0.5 + 0.5;
                validation = pow(validation,3);
                float validationEffect = (validation*validIntensity);
                
                UNITY_APPLY_FOG(i.fogCoord, col);
                //return fresnel + validation mix with Texture alpha
                return float4(lerp(_Color.xyz,_ValidationColor,validationEffect), _Color.w*col.w*1-fnl);
            }
            ENDCG
        }
    }
}
