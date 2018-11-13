﻿Shader "Custom/bordes" {
	Properties{
	_Color("Color", Color) = (1,1,1,1)
	_ColorTex("Color Textura", Color) = (1,1,1,1)
	_MainTex("Textura", 2D) = "white" {}
	
	_Factor("_Factor",Range(0,1)) = 0

	}
		SubShader{

		Tags
	{

		"RenderType" = "Opaque"  "Queue" = "Geometry+0" "IgnoreProjector" = "True" "IsEmissive" = "true"
	}

		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
#pragma surface surf Standard fullforwardshadows alpha:fade
#pragma target 3.0


	sampler2D _MainTex;
	

	struct Input {
		float2 uv_MainTex;

		float3 worldNormal;
		float3 viewDir;
	};


	float4 _Color;

	float4 _ColorTex;

	float _Factor;

	void surf(Input IN, inout SurfaceOutputStandard o) {
		// Albedo comes from a texture tinted by color
		float4 c1 = tex2D(_MainTex, IN.uv_MainTex)*_ColorTex;

		float bordes = 1-abs(dot(IN.worldNormal, IN.viewDir));
		float4 c = float4(1, 1, 1, 1);

		float4 c2 = float4(bordes, bordes, bordes, 1)*_Color;

		c = c1+(c2*(bordes));

	
		o.Albedo = c.rgb;
		o.Alpha = 1;

	}
	ENDCG
	}
		FallBack "Diffuse"
}
