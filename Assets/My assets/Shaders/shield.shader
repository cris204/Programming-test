Shader "Custom/shield" {
	Properties{
	_Color("Color", Color) = (1,1,1,1)
	_MainTex("Textura", 2D) = "white" {}
	_MainTex2("Textura", 2D) = "white" {}
	_VelocidadX("Velocidad X", Range(0,10)) = 2
		_VelocidadY("Velocidad Y", Range(0,10)) = 2
	
	_Factor("_Factor",Range(0,1)) = 0

	}
		SubShader{

		Tags
	{
		"Queue" = "Transparent"
		"IgnoreProjector" = "True"
		"RenderType" = "TransparentCutout"
	}

		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
#pragma surface surf Standard fullforwardshadows alpha:fade
#pragma target 3.0


	sampler2D _MainTex;
	sampler2D _MainTex2;
	

	struct Input {
		float2 uv_MainTex;

		float3 worldNormal;
		float3 viewDir;
	};

	float _VelocidadX;
	float _VelocidadY;

	float4 _Color;

	float4 _ColorTex;

	float _Factor;

	void surf(Input IN, inout SurfaceOutputStandard o) {
		// Albedo comes from a texture tinted by color
		float2 UVNuevo = IN.uv_MainTex ;

		float distanciaX = _VelocidadX * _Time;
		float distanciaY = _VelocidadY * _Time;
		UVNuevo += float2(distanciaX, distanciaY);

		float4 c1 = tex2D(_MainTex, IN.uv_MainTex);
		float4 Mascara = tex2D(_MainTex2, UVNuevo)*_Color;

		float bordes = 1-abs(dot(IN.worldNormal, IN.viewDir));
		float4 c = float4(1, 1, 1, 1);

		float4 c2 = float4(bordes, bordes, bordes, 1)*_Color;

		c = (c1*Mascara)+(c2);

	
		o.Albedo = c.rgb;
		o.Alpha = c;

	}
	ENDCG
	}
		FallBack "Diffuse"
}
