Shader "Custom/lava" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Sprite Texture", 2D) = "white" {}
		_NoirTex("Lava", 2D) = "white" {}
		_Range("Range", Range(0,1)) = 0.0
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200
		//Cull off

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
#pragma target 3.0

	sampler2D _MainTex;
	sampler2D _NoirTex;

	struct Input {
		float2 uv_MainTex;
	};

	fixed4 _Color;
	float _Range;

	void surf(Input IN, inout SurfaceOutputStandard o) {
		// Albedo comes from a texture tinted by color
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		fixed4 lava = tex2D(_NoirTex, IN.uv_MainTex) * _Color;
		_Range = _Time * 40.0f;
		o.Albedo = lerp(c.rgb, lava.rgb, sin(_Range));
		o.Alpha = c.a;
	}
	ENDCG
	}
		FallBack "Diffuse"
}
