// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Edge"
{
 Properties
 {
 _MainTex ("Texture", 2D) = "white" {}
 _OffsetUV ("OffsetUV", Range(0, 1)) = 0.1
 _EdgeColor ("EdgeColor", Color) = (1, 0, 0, 1)
 _AlphaTreshold ("Treshold", Range(0, 1)) = 0.5 
 }
 SubShader
 {
 Tags { "Queue" = "Transparent" }
 Blend SrcAlpha OneMinusSrcAlpha
  
 Pass
 {
  CGPROGRAM
  #pragma vertex vert
  #pragma fragment frag
  #include "UnityCG.cginc"
  
  struct appdata
  {
  float4 vertex : POSITION;
  fixed2 uv : TEXCOORD0;
  };
  
  struct v2f
  {  
  float4 vertex : SV_POSITION;
  fixed2 uv[5] : TEXCOORD0;
  };
  
  sampler2D _MainTex;
  float4 _MainTex_ST;
  fixed _OffsetUV;
  fixed4 _EdgeColor;
  fixed _AlphaTreshold;
  
  v2f vert (appdata v)
  {
  v2f o;
  o.vertex = UnityObjectToClipPos(v.vertex);
   
  o.uv[0] = v.uv; 
        o.uv[1] = v.uv + fixed2(0, _OffsetUV); //up 
        o.uv[2] = v.uv + fixed2(-_OffsetUV, 0); //left 
        o.uv[3] = v.uv + fixed2(0, -_OffsetUV); //bottom 
        o.uv[4] = v.uv + fixed2(_OffsetUV, 0); //right 
  
  return o;
  }
   
  fixed4 frag (v2f i) : SV_Target
  {
  fixed4 original = tex2D(_MainTex, i.uv[0]); 
        fixed alpha = original.a;
  
        fixed p1 = tex2D(_MainTex, i.uv[1]).a; 
        fixed p2 = tex2D(_MainTex, i.uv[2]).a; 
        fixed p3 = tex2D(_MainTex, i.uv[3]).a; 
        fixed p4 = tex2D(_MainTex, i.uv[4]).a; 
    
        alpha = p1 + p2 + p3 + p4 + alpha; 
        alpha /= 5; 
  
        if (alpha < _AlphaTreshold) original.rgb = _EdgeColor.rgb; 
     
        return original; 
  }
  ENDCG
 }
 }
}