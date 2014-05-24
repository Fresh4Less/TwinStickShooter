Shader "Custom/BuildMenuShader" {
	Properties {
		_Color("Color", Color) = (1, 1, 1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		//Blend SrcAlpha OneMinusSrcAlpha
		Tags {"Queue" = "Overlay"}
		ZTest Always

		Pass
		{
			Color [_Color]
			SetTexture[_MainTex] {Combine primary * texture}
		}
	}
}
