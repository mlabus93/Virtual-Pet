Shader "Unlit/Unlit Decal" {
    Properties {
        _Color ("Color Tint", Color) = (1,1,1,1)
        _MainTex ("SelfIllum Color (RGB) ", 2D) = "white"
        _BlendTex ("Alpha Blended (RGBA) ", 2D) = "black" {}
    }
    Category {
       Lighting On
       ZWrite On
       Cull Back
       Blend SrcAlpha OneMinusSrcAlpha
       Tags {Queue=Transparent}
       SubShader {
            Material {
               Emission [_Color]
            }
            Pass {
               SetTexture [_MainTex] {
                      Combine Texture * Primary, Texture * Primary
                }
                 // Blend in the alpha texture using the lerp operator
            SetTexture [_BlendTex] {
                combine texture lerp (texture) previous
            }
            }
        } 
    }
}
