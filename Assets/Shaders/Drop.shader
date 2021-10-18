Shader "Unlit/Drop"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DisplacementMap("Displacement", 2D) = "white" {}
        _DispStrength("DisplacementStrength", float) = 1
        _WaveSpeed("Displacement Speed", float) = 1
        _Opacity ("TransparencyWater", Range(0,1)) = 0.9

    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "IgnoreProjector"="True" "Queue"="Transparent" }
        LOD 100
        ZWrite Off
        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag alpha:fade
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Opacity;
            
            sampler2D _DisplacementMap;
            float _DispStrength;
            float _WaveSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                float2 tuv = o.uv + (_Time.y * _WaveSpeed);
                float height = tex2Dlod( _DisplacementMap, float4( tuv, 0, 0 ) ).x;
                v.vertex.xyz += v.normal * ( height * _DispStrength );

                o.vertex = UnityObjectToClipPos(v.vertex);
                
                


                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                col.a = _Opacity;
                return col;
            }
            ENDCG
        }
    }
}
