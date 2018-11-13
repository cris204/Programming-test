Shader "Hidden/kernel4" {
	
Properties {
	_MainTex("Textura", 2D) = "white" {}
	_Factor("Factor", float) = 0.5
	_Grosor("Grosor", Range(0, 5)) = 3
}

SubShader {
	Pass{
		CGPROGRAM
		#pragma vertex vert_img
		#pragma fragment frag
		#pragma fragmentoption ARB_precision_hint_fastest
		#include "UnityCG.cginc"

		uniform sampler2D _MainTex;
		float4 _Color;
		float _Factor;
		float _Grosor;


		float4 frag(v2f_img i) : COLOR {

			float4 c1 = tex2D(_MainTex, i.uv);
			float2 paso = float2(0.0009,0.0005);
			paso *= _Grosor;
			float4 centro = tex2D(_MainTex, float2(i.uv.x + 0.0*paso.x , i.uv.y + 0.0*paso.y ));
			float4 diagIzqArriba = tex2D(_MainTex, float2(i.uv.x - 1.0*paso.x , i.uv.y - 1.0*paso.y ));
			float4 diagIzqAbajo = tex2D(_MainTex, float2(i.uv.x + 1.0*paso.x , i.uv.y - 1.0*paso.y ));
			float4 diagDerAbajo = tex2D(_MainTex, float2(i.uv.x + 1.0*paso.x , i.uv.y + 1.0*paso.y ));
			float4 diagDerArriba = tex2D(_MainTex, float2(i.uv.x + 1.0*paso.x , i.uv.y - 1.0*paso.y ));
			float4 arriba = tex2D(_MainTex, float2(i.uv.x + 0.0 * paso.x , i.uv.y - 1.0 * paso.y ));
			float4 abajo = tex2D(_MainTex, float2(i.uv.x + 0.0 *paso.x , i.uv.y + 1.0*paso.y ));
			float4 izquierda = tex2D(_MainTex, float2(i.uv.x - 1.0 * paso.x , i.uv.y + 0.0 * paso.y ));
			float4 derecha = tex2D(_MainTex, float2(i.uv.x + 1.0*paso.x , i.uv.y + 0.0*paso.y ));

			float4 bor = ((arriba * 2) + (abajo * 2) + (izquierda * -1) + (derecha * 2) + (centro * 4) + (diagIzqArriba * 1)+ (diagIzqAbajo * 1) + (diagDerArriba * 1) + (diagDerAbajo * 1))/16;
			float4 c3 = c1*(1 - _Factor) + bor *_Factor;
			return c3;


		}
		ENDCG
	}

}
FallBack off
}

