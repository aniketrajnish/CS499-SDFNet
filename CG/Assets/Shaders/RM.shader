Shader "Makra/RM"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag            

            #include "UnityCG.cginc"

            #define max_steps 100
            #define max_dist 100
            #define surf_dist 1e-3

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 ro : TEXCOORD1;
                float3 hitPos : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            //for vertices
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.ro = mul(unity_WorldToObject, float4(_WorldSpaceCameraPos, 1)); //world space
                o.hitPos = mul(unity_ObjectToWorld, v.vertex);
                //o.hitPos = v.vertex;
                return o;
            }

            
            float GetDist(float3 p) {

                //cylinder
                float h = .5f;
                //float r = .5f;
                //float2 d = abs(float2(length(p.xz), p.y)) - float2(h, r);
                //return min(max(d.x, d.y), 0.0) + length(max(d, 0.0));

                //frustrum
                float r1 = .5f;
                float r2 = .25f;                
                float2 q = float2(length(p.xz), p.y);
                float2 k1 = float2(r2, h);
                float2 k2 = float2(r2 - r1, 2.0 * h);
                float2 ca = float2(q.x - min(q.x, (q.y < 0.0) ? r1 : r2), abs(q.y) - h);
                float2 cb = q - k1 + k2 * clamp(dot(k1 - q, k2) / dot(k2, k2), 0.0, 1.0);
                float s = (cb.x < 0.0 && ca.y < 0.0) ? -1.0 : 1.0;
                return s * sqrt(min(dot(ca, ca), dot(cb, cb)));
            }

            float Raymarch(float3 ro, float3 rd) {
                float d_origin = 0;
                float d_scene = 0;

                for (int i = 0; i < max_steps; i++) {
                    float3 p = ro + d_origin * rd;
                    d_scene = GetDist(p);
                    d_origin += d_scene;

                    if (d_scene < surf_dist || d_origin > max_dist) break;
                }

                return d_origin;
            }

            float3 GetNormal(float3 p) {
                float2 e = float2(1e-2, 0);
                float3 n = GetDist(p) - float3(GetDist(p - e.xyy), GetDist(p - e.yxy), GetDist(p - e.yyx));
                return normalize(n);
            }

            //for pixels
            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                float2 uv = i.uv - .5f;
                float3 ro = i.ro;
                float3 rd = i.hitPos - ro;
                rd = normalize(rd);

                //fixed4 tex = tex2D(_MainTex, i.uv);
                fixed4 col = 0;
                //col.rgb = ray_direction;

                float d = Raymarch(ro, rd);

                float mask = dot(uv, uv);

                if (d < max_dist) {
                    //col.r = 1;
                    float3 p = ro + rd * d;
                    float3 n = GetNormal(p);
                    col.rgb = n;
                };

                return col;
            }
            ENDCG
        }
    }
}