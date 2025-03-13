using static UnityEditor.ShaderData;
using Unity.Mathematics;
using UnityEngine.Profiling;
using UnityEngine;

Shader "Custom/FlowingEffectShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FlowSpeed("Flow Speed", Float) = 1.0
        _VoronoiScale("Voronoi Scale", Float) = 5.0
        _GlowIntensity("Glow Intensity", Float) = 2.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha // For transparency

        Pass
        {
            CGPROGRAM
            pragma vertex vert
#pragma fragment frag
# include "UnityCG.cginc"

            struct appdata_t
{
    float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

struct v2f
{
    float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

// sampler2D _MainTex;
float _FlowSpeed;
float _VoronoiScale;
float _GlowIntensity;

// Voronoi Noise Function
float voronoi(float2 p)
{
    float2 g = floor(p);
    float2 f = frac(p);
    float res = 8.0;

    for (int y = -1; y <= 1; y++)
    {
        for (int x = -1; x <= 1; x++)
        {
            float2 lattice = float2(x, y);
            float2 offset = float2(sin(dot(lattice, float2(12.9898, 78.233))),
                                   cos(dot(lattice, float2(12.9898, 78.233)))) * 0.5 + 0.5;
            float2 delta = lattice + offset - f;
            res = min(res, dot(delta, delta));
        }
    }
    return sqrt(res);
}

v2f vert(appdata_t v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = v.uv * _VoronoiScale;
    return o;
}

fixed4 frag(v2f i) : SV_Target
            {
                float timeFlow = _Time.y * _FlowSpeed;
float2 uvFlow = i.uv + float2(timeFlow * 0.2, timeFlow * 0.1);

float noise = voronoi(uvFlow);

float3 color1 = float3(0.0, 1.0, 1.0); // Bright Blue
float3 color2 = float3(0.5, 0.0, 1.0); // Purple

float3 finalColor = lerp(color1, color2, noise) * _GlowIntensity;

return fixed4(finalColor, 1.0);
            }
            ENDCG
        }
    }
}
