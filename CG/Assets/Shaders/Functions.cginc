float sdUnion(float p1, float p2) {
    return min(p1, p2);
}

float sdIntersection(float p1, float p2) {
    return max(p1, p2);
}

float sdSubtraction(float p1, float p2) {
    return max(-p1, p2);
}

float dot2(float2 v) {
    return dot(v, v);
}

float dot2(float3 v) {
    return dot(v, v);
}

float dotn(float2 a, float2 b) {
    return a.x * b.x - a.y * b.y;
}

float sdFMod(inout float p, float s) {
    float h = s * .5f;
    float c = floor((p + h) / s);
    p = fmod(p + h, s) - h;
    p = fmod(-p + h, s) - h;
    return c;
}

float sdCylinder(float3 p, float h, float r) {
    float2 d = abs(float2(length(p.xz), p.y)) - float2(h, r);
    return min(max(d.x, d.y), 0.0) + length(max(d, 0.0));
}

float sdFrustrum(float3 p, float r1, float r2, float h) {
    float2 q = float2(length(p.xz), p.y);
    float2 k1 = float2(r2, h);
    float2 k2 = float2(r2 - r1, 2.0 * h);
    float2 ca = float2(q.x - min(q.x, (q.y < 0.0) ? r1 : r2), abs(q.y) - h);
    float2 cb = q - k1 + k2 * clamp(dot(k1 - q, k2) / dot2(k2), 0.0, 1.0);
    float s = (cb.x < 0.0 && ca.y < 0.0) ? -1.0 : 1.0;
    return s * sqrt(min(dot2(ca), dot2(cb)));
}