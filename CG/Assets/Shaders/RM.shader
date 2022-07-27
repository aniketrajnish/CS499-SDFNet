Shader "Makra/ImageEffectRaymarcher"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }
        SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include"UnityCG.cginc"
            #include"Functions.cginc"

            #define max_steps 225
            #define max_dist 1000
            #define surf_dist 1e-2           

            struct Shape
            {
                float3 pos;
                float3 rot;
                float3 col;
                float blendFactor;
                int shapeIndex;
                float3 dimensions;
            };

            StructuredBuffer<Shape> shapes;
            int _Rank, _Count, _Shadow;
            sampler2D _MainTex;
            uniform float4x4 _CamFrustrum, _CamToWorld;
            sampler2D _CameraDepthTexture;
            float3 _LightDir;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 ray : TEXCOORD1;
            };

            v2f vert(appdata v)
            {
                v2f o;
                half index = v.vertex.z;
                v.vertex.z = 0;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv.xy;
                o.ray = _CamFrustrum[(int)index].xyz;
                o.ray /= abs(o.ray.z);
                o.ray = mul(_CamToWorld, o.ray);
                return o;
            }      

            float GetDist(Shape shape, float3 p) {

                float d = 0;

                p -= shape.pos;

                p.xz = mul(p.xz, float2x2(cos(shape.rot.y), sin(shape.rot.y), -sin(shape.rot.y), cos(shape.rot.y)));
                p.yz = mul(p.yz, float2x2(cos(shape.rot.x), -sin(shape.rot.x), sin(shape.rot.x), cos(shape.rot.x)));
                p.xy = mul(p.xy, float2x2(cos(shape.rot.z), -sin(shape.rot.z), sin(shape.rot.z), cos(shape.rot.z)));

                switch (shape.shapeIndex) {
                case 0:
                    d = sdCylinder(p, shape.dimensions.x, shape.dimensions.y);
                    break;
                case 1:
                    d = sdFrustrum(p, shape.dimensions.x, shape.dimensions.y, shape.dimensions.z);
                    break;
                    return d;
                }
                return d;
            }

            float distanceField(float3 p) {

                float sigmaDist = max_dist;

                for (int i = 0; i < _Count; i++) {
                    Shape _shape = shapes[i];

                    float deltaDist = GetDist(_shape, p);
                    sigmaDist = sdUnion(sigmaDist, deltaDist);
                }
                return sigmaDist;
            }

            float3 sigmaColor(float3 p) {
                float3 sigmaCol = 1;
                float sigmaDist = max_dist;

                for (int i = 0; i < _Count; i++) {
                    Shape _shape = shapes[i];

                    float deltaDist = GetDist(_shape, p);
                    float3 deltaCol = _shape.col;
                    float h = clamp(0.5 + 25 * (sigmaDist - deltaDist) / _shape.blendFactor, 0.0, 1.0);
                    sigmaCol = lerp(sigmaCol, deltaCol, h);
                    sigmaDist = sdUnion(deltaDist, sigmaDist);
                }
                return sigmaCol;
            }

            float3 getNormal(float3 p)
            {
              float d = distanceField(p).x;
              const float2 e = float2(.01, 0);

              float3 n = d - float3(
                  distanceField(p - e.xyy).x,
                  distanceField(p - e.yxy).x,
                  distanceField(p - e.yyx).x);

              return normalize(n);
            }

            fixed4 raymarching(float3 ro, float3 rd, float depth)
            {
                fixed4 result;
                float dist = 0;

                for (int i = 0; i < max_steps; i++) {
                    if (dist > max_dist || dist >= depth)
                    {
                        result = fixed4(rd, 0);
                        break;
                    }

                    float3 p = ro + rd * dist;

                    float d = distanceField(p);

                    if (d < surf_dist) {
                        float3 n = getNormal(p);
                        float lightDir = dot(-_LightDir, n);
                        fixed3 rgbVal = sigmaColor(p);

                        if (_Shadow == 1)
                            rgbVal = rgbVal * lightDir;

                        result = fixed4(rgbVal, 1);
                        break;
                    }

                    dist += d;
                }

                return result;
            }

            fixed4 frag(v2f i) : SV_Target
            {
               float depth = LinearEyeDepth(tex2D(_CameraDepthTexture, i.uv).r);
               depth *= length(i.ray);
               fixed3 col = tex2D(_MainTex, i.uv);
               float3 rayDirection = normalize(i.ray.xyz);
               float3 rayOrigin = _WorldSpaceCameraPos;
               fixed4 finalRay = raymarching(rayOrigin, rayDirection, depth);
               return fixed4(col * (1.0 - finalRay.w) + finalRay.xyz * finalRay.w ,1.0);
            }
            ENDCG
        }
    }
}
