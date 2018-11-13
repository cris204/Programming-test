
Shader "Custom/Vertices/Dilatacion" {

	Properties{
		_ColorPrincipal("Color principal", Color) = (1,1,1,1)
		_Textura("Textura", 2D) = "white" {}
	_Mascara("mascara", 2D) = "white" {}
	_Dilatacion("Dilatacion", Range(-1, 1)) = 0
	}


		SubShader{

		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		//#pragma surface surf Standard fullforwardshadows
#pragma surface surf Lambert vertex:vert
#pragma target 3.0

		sampler2D _Textura;
		sampler2D _Mascara;
	struct Input {
		float2 uv_Textura;
		float4 vertColor;
	};

	float4 _ColorPrincipal;
	float _Dilatacion;

	void surf(Input IN, inout SurfaceOutput o) {

		float4 c = tex2D(_Textura, IN.uv_Textura)*_ColorPrincipal;
		float4 mas = tex2D(_Mascara, IN.uv_Textura);
		float4 final=c+mas;
		o.Albedo = final.rgb;
	}

	void vert(inout appdata_full v, out Input o) {
		UNITY_INITIALIZE_OUTPUT(Input, o);

		float4 c = tex2Dlod(_Mascara, float4(v.texcoord.xy, 0, 0));
		
			v.vertex.xyz += v.normal * _Dilatacion*c.rgb;
			v.normal = normalize(float3(v.normal.x, v.normal.y, v.normal.z));
			o.vertColor = v.color;
	

	}

	ENDCG
	}
		FallBack "Diffuse"
}