Shader "Hidden/Fluvio/FluidEffectBlur" {
	Properties
	{ 
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BlurStrength ("Blur Strength", Float) = 0.125
	}
	
    CGINCLUDE
	#include "UnityCG.cginc"
	struct v2f {
		float4 pos : POSITION;
		#if SHADER_API_D3D11_9X
		half4 blurCoordinates[7] : TEXCOORD0;
		#else
		half2 screenPos : TEXCOORD0;
		half4 blurCoordinates[7] : TEXCOORD1;
		#endif
	};

	// Change this to adjust the overall max strength of the blur
	#define BLUR_MULTIPLIER 1.0

	float _BlurStrength;
    sampler2D _MainTex;
	half4 _MainTex_TexelSize;
	
    v2f vert_h( appdata_img v ) {
		v2f o; 
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		float2 screenPos = ComputeGrabScreenPos(o.pos);
        #if UNITY_UV_STARTS_AT_TOP
        if (_MainTex_TexelSize.y < 0)
            screenPos.y = 1-screenPos.y;
        #endif
		#if !SHADER_API_D3D11_9X
		o.screenPos = screenPos;
		#endif
		float blurAmount = _BlurStrength * BLUR_MULTIPLIER * (_MainTex_TexelSize.w / _MainTex_TexelSize.z);
		o.blurCoordinates[0].xy = screenPos + float2(-0.028, 0.0) * blurAmount;
		o.blurCoordinates[1].xy = screenPos + float2(-0.024, 0.0) * blurAmount;
		o.blurCoordinates[2].xy = screenPos + float2(-0.020, 0.0) * blurAmount;
		o.blurCoordinates[3].xy = screenPos + float2(-0.016, 0.0) * blurAmount;
		o.blurCoordinates[4].xy = screenPos + float2(-0.012, 0.0) * blurAmount;
		o.blurCoordinates[5].xy = screenPos + float2(-0.008, 0.0) * blurAmount;
		o.blurCoordinates[6].xy = screenPos + float2(-0.004, 0.0) * blurAmount;
		o.blurCoordinates[0].zw = screenPos + float2(0.004, 0.0) * blurAmount;
		o.blurCoordinates[1].zw = screenPos + float2(0.008, 0.0) * blurAmount;
		o.blurCoordinates[2].zw = screenPos + float2(0.012, 0.0) * blurAmount;
		o.blurCoordinates[3].zw = screenPos + float2(0.016, 0.0) * blurAmount;
		o.blurCoordinates[4].zw = screenPos + float2(0.020, 0.0) * blurAmount;
		o.blurCoordinates[5].zw = screenPos + float2(0.024, 0.0) * blurAmount;
		o.blurCoordinates[6].zw = screenPos + float2(0.028, 0.0) * blurAmount;
		return o;
	}
	v2f vert_v( appdata_img v ) {
		v2f o; 
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		float2 screenPos = ComputeGrabScreenPos(o.pos);
		#if UNITY_UV_STARTS_AT_TOP
        if (_MainTex_TexelSize.y < 0)
            screenPos.y = 1-screenPos.y;
        #endif
		#if !SHADER_API_D3D11_9X
		o.screenPos = screenPos;
		#endif
		float blurAmount = _BlurStrength * BLUR_MULTIPLIER;
		o.blurCoordinates[0].xy = screenPos + float2(0.0, -0.028) * blurAmount;
		o.blurCoordinates[1].xy = screenPos + float2(0.0, -0.024) * blurAmount;
		o.blurCoordinates[2].xy = screenPos + float2(0.0, -0.020) * blurAmount;
		o.blurCoordinates[3].xy = screenPos + float2(0.0, -0.016) * blurAmount;
		o.blurCoordinates[4].xy = screenPos + float2(0.0, -0.012) * blurAmount;
		o.blurCoordinates[5].xy = screenPos + float2(0.0, -0.008) * blurAmount;
		o.blurCoordinates[6].xy = screenPos + float2(0.0, -0.004) * blurAmount;
		o.blurCoordinates[0].zw = screenPos + float2(0.0, 0.004) * blurAmount;
		o.blurCoordinates[1].zw = screenPos + float2(0.0, 0.008) * blurAmount;
		o.blurCoordinates[2].zw = screenPos + float2(0.0, 0.012) * blurAmount;
		o.blurCoordinates[3].zw = screenPos + float2(0.0, 0.016) * blurAmount;
		o.blurCoordinates[4].zw = screenPos + float2(0.0, 0.020) * blurAmount;
		o.blurCoordinates[5].zw = screenPos + float2(0.0, 0.024) * blurAmount;
		o.blurCoordinates[6].zw = screenPos + float2(0.0, 0.028) * blurAmount;
		return o;
	}
	half4 frag(v2f i) : COLOR {
		
		half4 color = half4(0,0,0,0);
		#if SHADER_API_D3D11_9X
		// Maybe find a way to not compute screen pos in fragment shader on d3d11_9x?
		float blurAmount = _BlurStrength * BLUR_MULTIPLIER;
		half2 screenPos = i.blurCoordinates[0].xy - float2(0.0, -0.028) * blurAmount;
		half4 tex = tex2D(_MainTex, screenPos); 
		#else
		half4 tex = tex2D(_MainTex, i.screenPos);
		#endif
		color += tex2D(_MainTex, i.blurCoordinates[0].xy)*0.0044299121055113265;
		color += tex2D(_MainTex, i.blurCoordinates[1].xy)*0.00895781211794;
		color += tex2D(_MainTex, i.blurCoordinates[2].xy)*0.0215963866053;
		color += tex2D(_MainTex, i.blurCoordinates[3].xy)*0.0443683338718;
		color += tex2D(_MainTex, i.blurCoordinates[4].xy)*0.0776744219933;
		color += tex2D(_MainTex, i.blurCoordinates[5].xy)*0.115876621105;
		color += tex2D(_MainTex, i.blurCoordinates[6].xy)*0.147308056121;
		color += tex*0.159576912161;
		color += tex2D(_MainTex, i.blurCoordinates[0].zw)*0.147308056121;
		color += tex2D(_MainTex, i.blurCoordinates[1].zw)*0.115876621105;
		color += tex2D(_MainTex, i.blurCoordinates[2].zw)*0.0776744219933;
		color += tex2D(_MainTex, i.blurCoordinates[3].zw)*0.0443683338718;
		color += tex2D(_MainTex, i.blurCoordinates[4].zw)*0.0215963866053;
		color += tex2D(_MainTex, i.blurCoordinates[5].zw)*0.00895781211794;
		color += tex2D(_MainTex, i.blurCoordinates[6].zw)*0.0044299121055113265;

		// Keep alpha at edges
		color.a = tex.a;

		return color;
	}
	ENDCG
	
    SubShader {
		 Pass {
			  ZTest Always Cull Off ZWrite Off
			  Fog { Mode off }      

			  CGPROGRAM
			  #pragma fragmentoption ARB_precision_hint_fastest
			  #pragma vertex vert_h
			  #pragma fragment frag
			  ENDCG
		  }
		  Pass {
			  ZTest Always Cull Off ZWrite Off
			  Fog { Mode off }      

			  CGPROGRAM
			  #pragma fragmentoption ARB_precision_hint_fastest
			  #pragma vertex vert_v
			  #pragma fragment frag
			  ENDCG
		  }
	}
	Fallback Off
}
