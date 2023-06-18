Shader "Unlit/Affine" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
    }
 
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100
 
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            #include "UnityCG.cginc"
 
            struct appdata {
                float4 vertex : POSITION;
                noperspective float2 uv : TEXCOORD0;
            };
 
            struct v2f {
                noperspective float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
 
            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
 
            fixed4 frag(v2f i) : SV_Target {
                fixed4 c = tex2D(_MainTex, i.uv);
                return c;
            }
            ENDCG
        }
    }
}