Shader "Custom/Water Specular" {
	Properties {
		_WaveSpeedAlbedo("Wave Speed Albedo", Range(-1,1)) = 1
		_WaveSpeedBumpMap("Wave Speed BumpMap", Range(-1,1)) = 1

		_Color ("Color (RGBA)", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap("Bumpmap (RGB)", 2D) = "white" {}
		_Glossiness("Glosiness", Range(0,1)) = 0.5
		_Specular("Specular", Range(0,1)) = 0.5
		_Exposure("Exposure", Range(0,1)) = 0.05
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 250
		
		CGPROGRAM
		#pragma surface surf StandardSpecular exclude_path:prepass nolightmap noforwardadd halfasview noshadow

		#pragma target 3.0

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
		};

		sampler2D _MainTex;
		sampler2D _BumpMap;

		half _WaveSpeedAlbedo;
		half _WaveSpeedBumpMap;

		half _Glossiness;
		uniform half _Specular;
		fixed4 _Color;
		uniform float _Exposure;

		void surf (Input IN, inout SurfaceOutputStandardSpecular o) 
		{
			float2 albedoWavedUV = float2(IN.uv_MainTex.x, IN.uv_MainTex.y + (_WaveSpeedAlbedo * _Time.x));
			float2 normalWavedUV = float2(IN.uv_BumpMap.x, IN.uv_BumpMap.y + (_WaveSpeedBumpMap * _Time.x));

			fixed4 c = tex2D(_MainTex, albedoWavedUV) * _Color * _Exposure;
			fixed3 normal = UnpackNormal(tex2D(_BumpMap, normalWavedUV));

			o.Albedo = c.rgb;
			o.Normal = normal;
			o.Smoothness = _Glossiness;
			o.Specular = _Specular;
			o.Alpha = _Color.a;
		}
		ENDCG
	}

	FallBack "Diffuse"
}
