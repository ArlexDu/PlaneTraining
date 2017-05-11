// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/halfLambert" {
	Properties {
		_MainTex("Main Texture",2D) = "white"{}
		_Diffuse ("Color", Color) = (1,1,1,1)
	}
	SubShader {
		pass{
			tags{"LightModel" = "ForwardBase"}
			CGPROGRAM
			#include "Lighting.cginc"
			#pragma vertex vert
			#pragma fragment frag

			//fixed4 _Diffuse;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			struct a2f{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;
			};

			struct v2f{
				float4 pos : SV_POSITION;
				float3 worldNormal : TEXCOORD0;
				float2 uv: TEXCOORD1;
			};

			v2f vert(a2f v){
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP,v.vertex);
				o.worldNormal = mul(v.normal,(float3x3)unity_WorldToObject);
				o.uv = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}

			fixed4 frag(v2f i):SV_Target{
				fixed3 albedo = tex2D(_MainTex,i.uv).rgb;
				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo;
				float3 worldNormal = normalize(i.worldNormal);
				float3 worldLight = _WorldSpaceLightPos0.xyz;
				fixed half_lambert = dot(worldNormal,worldLight)*0.5+0.5;
				//fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * half_lambert;
				fixed3 diffuse = _LightColor0.rgb * half_lambert;
				fixed3 color = ambient + diffuse;
				return fixed4(color,1.0);
			}	

			ENDCG
		}
	}
	FallBack "Diffuse"
}
