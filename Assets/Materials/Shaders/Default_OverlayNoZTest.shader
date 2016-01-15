Shader "UI/Default_OverlayNoZTest"
{
	Properties
	{
		_MainTex("Sprite Texture", 2D) = "white" {}
		_NoirTex("Sprite Texture", 2D) = "white" {}
		_ColorBlack("Rlack", Color) = (1,1,1,1)
		_Color("Tint", Color) = (1,1,1,1)

		_ColorMask("Color Mask", Float) = 15
	}

		SubShader
	{
		Tags
	{
		"Queue" = "Overlay"
		"IgnoreProjector" = "True"
		"RenderType" = "Transparent"
		"PreviewType" = "Plane"
		"CanUseSpriteAtlas" = "True"
	}

		Stencil
	{
		Ref[_Stencil]
		Comp[_StencilComp]
		Pass[_StencilOp]
		ReadMask[_StencilReadMask]
		WriteMask[_StencilWriteMask]
	}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest Off
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask[_ColorMask]

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

	struct appdata_t
	{
		float4 vertex   : POSITION;
		float4 color    : COLOR;
		float2 texcoord : TEXCOORD0;
	};

	struct v2f
	{
		float4 vertex   : SV_POSITION;
		fixed4 color : COLOR;
		half2 texcoord  : TEXCOORD0;
	};

	fixed4 _Color;

	v2f vert(appdata_t IN)
	{
		v2f OUT;
		OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
		OUT.texcoord = IN.texcoord;
#ifdef UNITY_HALF_TEXEL_OFFSET
		OUT.vertex.xy += (_ScreenParams.zw - 1.0)*float2(-1,1);
#endif
		OUT.color = IN.color * _Color;
		return OUT;
	}

	struct Input {
		float2 uv_MainTex;
	};

	sampler2D _MainTex;
	sampler2D _NoirTex;
	fixed4 _ColorBlack;
	float _Range;

	fixed4 frag(v2f IN) : SV_Target
	{
		half4 color = tex2D(_MainTex, IN.texcoord) * IN.color;
		half4 color2= tex2D(_NoirTex, IN.texcoord) * IN.color;
		_Range = _Time * 40.0f;
		color = lerp(color.rgba, color2.rgba, sin(_Range));
		return color;
	}
		ENDCG
	}
	}
}