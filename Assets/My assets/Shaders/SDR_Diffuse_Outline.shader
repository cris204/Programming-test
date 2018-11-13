// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/Diffuse_Outline"
{
	Properties
	{
		_ASEOutlineColor( "Outline Color", Color ) = (0.2588235,0.1980332,0.02174118,1)
		_ASEOutlineWidth( "Outline Width", Float ) = 0.015
		_Tint("Tint", Color) = (1,1,1,1)
		_MainTex("MainTex", 2D) = "white" {}
		_Switch2DFlat("Switch 2D Flat", Range( 0 , 1)) = 0.2
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ }
		Cull Front
		CGPROGRAM
		#pragma target 3.0
		#pragma surface outlineSurf Outline nofog  keepalpha noshadow noambient novertexlights nolightmap nodynlightmap nodirlightmap nometa noforwardadd vertex:outlineVertexDataFunc 
		
		
		
		struct Input {
			half filler;
		};
		uniform half4 _ASEOutlineColor;
		uniform half _ASEOutlineWidth;

		void outlineVertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			v.vertex.xyz += ( v.normal * _ASEOutlineWidth );
		}
		inline half4 LightingOutline( SurfaceOutput s, half3 lightDir, half atten ) { return half4 ( 0,0,0, s.Alpha); }
		void outlineSurf( Input i, inout SurfaceOutput o )
		{
			o.Emission = _ASEOutlineColor.rgb;
			o.Alpha = 1;
		}
		ENDCG
		

		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma exclude_renderers xbox360 xboxone ps4 psp2 n3ds wiiu 
		#pragma surface surf Lambert keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform float4 _Tint;
		uniform float _Switch2DFlat;

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float4 temp_output_63_0 = ( tex2D( _MainTex, uv_MainTex ) * float4( 1,1,1,1 ) * _Tint );
			float4 lerpResult126 = lerp( temp_output_63_0 , float4( 0,0,0,0 ) , _Switch2DFlat);
			o.Albedo = (float4( 0,0,0,0 ) + (lerpResult126 - float4( 0,0,0,0 )) * (float4( 0.85,0.85,0.85,0 ) - float4( 0,0,0,0 )) / (float4( 1,1,1,0 ) - float4( 0,0,0,0 ))).rgb;
			float4 lerpResult129 = lerp( float4( 0,0,0,0 ) , temp_output_63_0 , _Switch2DFlat);
			float4 clampResult135 = clamp( lerpResult129 , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			o.Emission = clampResult135.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}