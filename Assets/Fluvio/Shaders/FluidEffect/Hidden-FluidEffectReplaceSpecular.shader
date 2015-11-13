Shader "Hidden/Fluvio/FluidEffectReplaceSpecular"
{
    Properties
    {
        // Diffuse/alpha
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo", 2D) = "white" {}
		_Cutoff("Alpha Cutoff", Range(0.001, 1.0)) = 0.5
		_Cutoff2("Shadow/Depth Alpha Cutoff", Range(0.001, 1.0)) = 0.25

        // Specular/smoothness
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _SpecColor ("Specular", Color) = (0.2,0.2,0.2,1)
        _SpecGlossMap ("Specular", 2D) = "white" {}
        
        // Normal
        _BumpScale("Scale", Float) = 1.0
        _BumpMap ("Normal Map", 2D) = "bump" {}
        
        // Emission
        _EmissionColor("Color", Color) = (0,0,0,0)
        _EmissionMap("Emission", 2D) = "white" {}

        // Vertex colors
        [KeywordEnum(None, Albedo, Specular, Emission)] _VertexColorMode ("Vertex color mode", Float) = 1

		// Culling
		[HideInInspector] _CullFluid("Cull Fluid", Float) = -1.0
    }

	CGINCLUDE
        #define _GLOSSYENV 1
        #define FLUVIO_SETUP_BRDF_INPUT FluvioSpecularSetup
    ENDCG

    SubShader 
    {
        Tags { "RenderType"="Opaque" }
        ZWrite On
    
        Pass 
        {  
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata_t {
                    float4 vertex : POSITION;
                };

                struct v2f {
                    float4 vertex : SV_POSITION;
                };

                v2f vert (appdata_t v)
                {
                    v2f o;
                    o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                    return o;
                }
            
                fixed4 frag (v2f i) : COLOR
                {
                    return 0;
                }
            ENDCG
        }

        Pass
        { 
			Name "ShadowCaster"
			Tags { "LightMode" = "ShadowCaster" }
 
			Fog {Mode Off}
			ZWrite On ZTest Less Cull Off
			Offset 1, 1
 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma multi_compile_shadowcaster
			#include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Cutoff;
			
            struct v2f {
                V2F_SHADOW_CASTER; 
                float4 uv : TEXCOORD1;
            };
 
			v2f vert (appdata_full v)
			{
                v2f o;
				UNITY_INITIALIZE_OUTPUT(v2f, o);
				TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
			    return o;
			}
 
			float4 frag( v2f i ) : COLOR
			{
				fixed4 texcol = tex2D( _MainTex, i.uv );
				clip( texcol.a - _Cutoff );
				SHADOW_CASTER_FRAGMENT(i)
			}
			ENDCG
        }
    }
	SubShader 
    {
        Tags { "RenderType"="TransparentCutout" "IgnoreProjector"="True"}
        Fog { Mode Off }
        Lighting Off
        ZWrite On
        ZTest LEqual
    
        Pass 
        {  
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

				sampler2D _MainTex;
				float _Cutoff;

				struct appdata_t
				{
					float4 vertex   : POSITION;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					half2 texcoord  : TEXCOORD0;
				};

				v2f vert(appdata_t v)
				{
					v2f o;
					o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
					o.texcoord = v.texcoord;
					return o;
				}

				fixed4 frag(v2f i) : COLOR
				{
					fixed4 c = tex2D(_MainTex, i.texcoord);
					clip(c.a - _Cutoff);
					return 0;
				}
            ENDCG
        }
    }
    SubShader
    {
        Tags { "Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="Fluvio" "PerformanceChecks"="False" }
        
        // ------------------------------------------------------------------
        // Base forward pass (directional light, emission, lightmaps, ...)
        Pass
        {
            Name "FORWARD" 
            Tags { "LightMode" = "ForwardBase" }

            Blend SrcAlpha OneMinusSrcAlpha, One One
            ZWrite Off ZTest LEqual Cull Off
            
            CGPROGRAM
            #pragma target 3.0
            // GLES2.0 disabled to prevent errors spam on devices without textureCubeLodEXT
            #pragma exclude_renderers gles
            
            #pragma multi_compile _VERTEXCOLORMODE_NONE _VERTEXCOLORMODE_ALBEDO _VERTEXCOLORMODE_SPECULAR _VERTEXCOLORMODE_EMISSION

            #pragma shader_feature _NORMALMAP
            #pragma shader_feature _EMISSION
            // ALWAYS ON shader_feature _GLOSSYENV
            #pragma shader_feature _SPECGLOSSMAP 
            
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
                
            #pragma vertex FluvioVertForwardBase
            #pragma fragment FluvioFragForwardBase

            #include "FluidEffect.cginc"

            ENDCG
        }
		// ------------------------------------------------------------------
        //  Additive forward pass (one light per pass)
        Pass
        {
            Name "FORWARD_DELTA"
            Tags { "LightMode" = "ForwardAdd" }
            Blend SrcAlpha One
            Fog { Color (0,0,0,0) } // in additive pass fog should be black
            ZWrite Off ZTest LEqual Cull Off
           
            CGPROGRAM
            #pragma target 3.0
            // GLES2.0 disabled to prevent errors spam on devices without textureCubeLodEXT
            #pragma exclude_renderers gles
            
            #pragma multi_compile _VERTEXCOLORMODE_NONE _VERTEXCOLORMODE_ALBEDO _VERTEXCOLORMODE_SPECULAR _VERTEXCOLORMODE_EMISSION

            #pragma shader_feature _NORMALMAP
            // ALWAYS ON shader_feature _GLOSSYENV
            #pragma shader_feature _SPECGLOSSMAP
            
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            
            #pragma vertex FluvioVertForwardAdd
            #pragma fragment FluvioFragForwardAdd

            #include "FluidEffect.cginc"

            ENDCG
        }
        // ------------------------------------------------------------------
        //  Shadow rendering pass
        //Pass {
        //    Name "ShadowCaster"
        //    Tags { "LightMode" = "ShadowCaster" }
        //    
        //    ZWrite On ZTest LEqual
        //    
        //    CGPROGRAM
        //    #pragma target 3.0
        //    // TEMPORARY: GLES2.0 temporarily disabled to prevent errors spam on devices without textureCubeLodEXT
        //    #pragma exclude_renderers gles
        //    
        //    // -------------------------------------

        //    #pragma multi_compile _VERTEXCOLORMODE_NONE _VERTEXCOLORMODE_ALBEDO _VERTEXCOLORMODE_SPECULAR _VERTEXCOLORMODE_EMISSION

        //    #pragma shader_feature _ _ALPHATEST_ON _ALPHABLEND_ON
        //    #pragma multi_compile_shadowcaster

        //    #pragma vertex vertShadowCaster
        //    #pragma fragment fragShadowCaster

        //    #include "FluidEffectShadow.cginc"

        //    ENDCG
        //}
		// ------------------------------------------------------------------
        //  TODO: Deferred pass
        // Pass
        // {
        //     Name "DEFERRED"
        //     Tags { "LightMode" = "Deferred" }
		// 
        //     ZWrite On ZTest LEqual Cull Off
        //     
        //     CGPROGRAM
		// 	#pragma target 3.0
		// 	// GLES2.0 disabled to prevent errors spam on devices without textureCubeLodEXT
		// 	#pragma exclude_renderers nomrt gles
        //     
        //  #pragma multi_compile _VERTEXCOLORMODE_NONE _VERTEXCOLORMODE_ALBEDO _VERTEXCOLORMODE_SPECULAR _VERTEXCOLORMODE_EMISSION
		// 
		// 	#pragma shader_feature _NORMALMAP
		// 	#pragma shader_feature _EMISSION
		// 	// ALWAYS ON shader_feature _GLOSSYENV
		// 	#pragma shader_feature _SPECGLOSSMAP
        //     
		// 	#pragma multi_compile ___ UNITY_HDR_ON
		// 	#pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
		// 	#pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
		// 	#pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
        //     
		// 	#pragma vertex FluvioVertDeferred
		// 	#pragma fragment FluvioFragDeferred
		// 
		// 	#include "FluidEffect.cginc"
        //     ENDCG
        // }
    }
}