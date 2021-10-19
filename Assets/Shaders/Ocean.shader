// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/Ocean"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Opacity ("TransparencyWater", Range(0,1)) = 0.9

        _WaveSpeed ("Wave Speed", float) = 1
        _WaveAmplitude ("Wave Amplitude", float) = 0.1
        _WavePeriod("Wave Period",float) = 1

        _RippleAmplitude ("Amplitude Ripple",float)=0.01
        _RippleSpeed ("Speed Ripple",float)=-1.0
        _RipplePeriod ("Period Ripple",float)=50


    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "IgnoreProjector"="True" "Queue"="Transparent"}
        LOD 200
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha:fade
        // 
        #pragma vertex vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };
        

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _Opacity;



        float _WaveSpeed;
        float _WaveAmplitude;
        float _WavePeriod;
        
        float _RippleAmplitude; 
        float _RippleSpeed; 
        float _RipplePeriod;
        float _OffSetX;
        float _OffSetZ;
        float _Distance;
        float _ImpactX;
        float _ImpactZ;
        

        void vert (inout appdata_full v){
            //Ocean waves effect
            float PI = 3.1415;
            float waveX = sin( (_Time.y * _WaveSpeed) + (v.vertex.x * (_WavePeriod * 2*PI))   );
            float waveZ = sin( (_Time.y * _WaveSpeed) + (v.vertex.z * (_WavePeriod * 2*PI))   );
            v.vertex.y += waveX * waveZ * _WaveAmplitude;
            
            //Ripple Effect

            float offsetvert = ((v.vertex.x *  v.vertex.x)+(v.vertex.z *  v.vertex.z))+ ((v.vertex.x * _OffSetX) + (v.vertex.z * _OffSetZ)) ;
            float value = sin(_Time.w * _RippleSpeed  + offsetvert * _RipplePeriod );
            float3 worldPos = mul(unity_ObjectToWorld, v.vertex ).xyz;

            if( sqrt(pow(worldPos.x-_ImpactX,2)+pow(worldPos.z-_ImpactZ,2)) < _Distance){
                v.vertex.y += value * _RippleAmplitude;
            }

            
            
        }

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            c.a = _Opacity;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
