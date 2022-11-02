using System;
using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]
public class RaymarchRenderer : MonoBehaviour
{ 
    public enum Shape
    {
        Cylinder,
        Frustrum,
        Shpere,
        Torus,
        CappedTorus,
        Link,
        Cone,
        InfCone,
        Plane,
        HexPrism,
        TriPrism,
        Capsule,
        InfiniteCylinder,
        Box,
        RoundBox,
        RoundedCylinder,
        CappedCone,
        BoxFrame,
        SolidAngle,
        CutSphere,
        CutHollowSphere,
        DeathStar,
        RoundCone,
        Ellipsoid,
        Rhombus,
        Octahedron,
        Pyramid,
        Triangle,
        Quad,
        Fractal,
        Tesseract
    };     
    public enum Operation
    {
        Union,
        Blend,
        Subtract,
        Inrersect
    };

    public Shape shape;
    public Operation operation;
    public Color color;
    public CylinderDimensions cyl = new CylinderDimensions();
    public CappedConeDimensions cap = new CappedConeDimensions();

    [Range(.1f, 100)]
    public float blendFactor;
   
    public vector12 GetDimensionVectors(int i)
    {
        int len = Enum.GetNames(typeof(Shape)).Length;

        vector12[] dimensions = new vector12[len];

        //cylinder        
        dimensions[0] = new vector12(cyl.r, cyl.h, 0,0,0,0,0,0,0,0,0,0);

        //capped cone        
        dimensions[1] = new vector12(cap.r1, cap.r2, cap.h, 0, 0, 0, 0, 0, 0, 0, 0,0);

        //sphere
        dimensions[2] = new vector12(SphereDimensions.radius, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        //torus
        dimensions[3] = new vector12(TorusDimensions.thickness.x, TorusDimensions.thickness.y, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        //capped torus
        dimensions[4] = new vector12(CappedTorusDimensions.ro, CappedTorusDimensions.ri, CappedTorusDimensions.thickness.x, CappedTorusDimensions.thickness.y, 0, 0, 0, 0, 0, 0, 0, 0);

        //link
        dimensions[5] = new vector12(LinkDimensions.separation, LinkDimensions.radius, LinkDimensions.thickness, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        //cone
        dimensions[6] = new vector12(ConeDimensions.tan.x, ConeDimensions.tan.y, ConeDimensions.height, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        //infinite cone
        dimensions[7] = new vector12(InfiniteConeDimensions.tan.x, InfiniteConeDimensions.tan.y, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        //plane
        dimensions[8] = new vector12(PlaneDimensions.normal.x, PlaneDimensions.normal.y, PlaneDimensions.normal.z, PlaneDimensions.distance, 0, 0, 0, 0, 0, 0, 0, 0);

        //hexagonal prism
        dimensions[9] = new vector12(HexagonalPrismDimensions.h.x, HexagonalPrismDimensions.h.y, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        //triangular prism
        dimensions[10] = new vector12(TriangularPrismDimensions.h.x, TriangularPrismDimensions.h.y, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        //capsule
        dimensions[11] = new vector12(CapsuleDimensions.a.x, CapsuleDimensions.a.y, CapsuleDimensions.a.z, CapsuleDimensions.b.x, CapsuleDimensions.b.y, CapsuleDimensions.b.z, CapsuleDimensions.r, 0, 0, 0, 0, 0);

        //infinite cylinder
        dimensions[12] = new vector12(InfiniteCylinderDimensions.c.x, InfiniteCylinderDimensions.c.y, InfiniteCylinderDimensions.c.z, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        //box
        dimensions[13] = new vector12(BoxDimensions.size, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        //round box
        dimensions[14] = new vector12(RoundBoxDimensions.size, RoundBoxDimensions.roundFactor, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        //rounded cylinder
        dimensions[15] = new vector12(RoundedCylinderDimensions.ra, RoundedCylinderDimensions.rb, RoundedCylinderDimensions.h, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        //capped cone
        //dimensions[14] = new vector12(CappedConeDimensions.h, CappedConeDimensions.r1, CappedConeDimensions.r2, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        //box frame
        dimensions[16] = new vector12(BoxFrameDimensions.size.x, BoxFrameDimensions.size.y, BoxFrameDimensions.size.z, BoxFrameDimensions.cavity, 0, 0, 0, 0, 0, 0, 0, 0);

        //solid angle
        dimensions[17] = new vector12(SolidAngleDimensions.c.x, SolidAngleDimensions.c.y, SolidAngleDimensions.ra, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        //cut sphere
        dimensions[18] = new vector12(CutSphereDimensions.r, CutSphereDimensions.h, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        //hollow sphere
        dimensions[19] = new vector12(HollowSphereDimensions.r, HollowSphereDimensions.h, HollowSphereDimensions.t, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        //death star
        dimensions[20] = new vector12(DeathStarDimensions.ra, DeathStarDimensions.rb, DeathStarDimensions.d, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        //round cone
        dimensions[21] = new vector12(RoundConeDimensions.r1, RoundConeDimensions.r2, RoundConeDimensions.h, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        //ellipsoid
        dimensions[22] = new vector12(EllipsoidDimensions.Radius.x, EllipsoidDimensions.Radius.y, EllipsoidDimensions.Radius.z, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        //rhombus
        dimensions[23] = new vector12(RhombusDimensions.la, RhombusDimensions.lb, RhombusDimensions.h, RhombusDimensions.ra, 0, 0, 0, 0, 0, 0, 0, 0);

        //octahedron
        dimensions[24] = new vector12(OctahedronDimensions.size, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        //pyramid
        dimensions[25] = new vector12(PyramidDimensions.size, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        //triangle
        dimensions[26] = new vector12(TriangleDimensions.sideA.x, TriangleDimensions.sideA.y, TriangleDimensions.sideA.z, TriangleDimensions.sideB.x, TriangleDimensions.sideB.y, TriangleDimensions.sideB.z, TriangleDimensions.sideC.x, TriangleDimensions.sideC.y, TriangleDimensions.sideC.z, 0, 0, 0);

        //quad
        dimensions[27] = new vector12(QuadDimensions.sideA.x, QuadDimensions.sideA.y, QuadDimensions.sideA.z, QuadDimensions.sideB.x, QuadDimensions.sideB.y, QuadDimensions.sideB.z, QuadDimensions.sideC.x, QuadDimensions.sideC.y, QuadDimensions.sideC.z, QuadDimensions.sideD.x, QuadDimensions.sideD.y, QuadDimensions.sideD.z);

        //fractals
        dimensions[28] = new vector12(FractalDimenisons.i, FractalDimenisons.s, FractalDimenisons.o, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        //tesseract
        dimensions[29] = new vector12(TesseractDimensions.size.x, TesseractDimensions.size.y, TesseractDimensions.size.z, TesseractDimensions.size.w, 0, 0, 0, 0, 0, 0, 0, 0);

        return dimensions[i];
    }
    
}

public struct CylinderDimensions
{
    public float r;
    public float h;
};
public struct CappedConeDimensions
{
    public float h;
    public float r1;
    public float r2;
};

public struct SphereDimensions
{
    public static float radius = EditorPrefs.GetFloat("SphereRadius", .5f);
};
public struct TorusDimensions
{
    public static Vector2 thickness = new Vector2(EditorPrefs.GetFloat("TorusThicknessX", .4f), EditorPrefs.GetFloat("TorusThicknessY", .1f));
};
public struct CappedTorusDimensions
{
    public static float ro = EditorPrefs.GetFloat("CTRo", .25f);
    public static float ri = EditorPrefs.GetFloat("CTRi", .1f);
    public static Vector2 thickness = new Vector2(EditorPrefs.GetFloat("CTTx", .1f), EditorPrefs.GetFloat("CTTy", .1f));
};
public struct LinkDimensions
{
    public static float separation = EditorPrefs.GetFloat("LinkSeparation", .13f);
    public static float radius = EditorPrefs.GetFloat("LinkRadius", .2f);
    public static float thickness = EditorPrefs.GetFloat("LinkThickness", .09f);
};
public struct ConeDimensions
{
    public static Vector2 tan = new Vector2(EditorPrefs.GetFloat("ConeTanX", 1), EditorPrefs.GetFloat("ConeTanY", 2));
    public static float height = EditorPrefs.GetFloat("ConeHeight", 1);
};
public struct InfiniteConeDimensions
{
    public static Vector2 tan = new Vector2(EditorPrefs.GetFloat("ICTanX", .1f), EditorPrefs.GetFloat("ICTanY", .1f));
};
public struct PlaneDimensions
{
    public static Vector3 normal = new Vector3(EditorPrefs.GetFloat("PlaneNormalX", 0), EditorPrefs.GetFloat("PlaneNormalX", .5f), EditorPrefs.GetFloat("PlaneNormalX", .5f));
    public static float distance = EditorPrefs.GetFloat("PlaneDistance", 1);
};
public struct HexagonalPrismDimensions
{
    public static Vector2 h = new Vector2(EditorPrefs.GetFloat("HPHX", .25f), EditorPrefs.GetFloat("HPHY", .25f));
};
public struct TriangularPrismDimensions
{
    public static Vector2 h = new Vector2(EditorPrefs.GetFloat("TPHX", .25f), EditorPrefs.GetFloat("TPHY", .25f));
};
public struct CapsuleDimensions
{
    public static Vector3 a = new Vector3(EditorPrefs.GetFloat("CapsuleAX", .25f), EditorPrefs.GetFloat("CapsuleAY", .1f), EditorPrefs.GetFloat("CapsuleAZ", .25f));
    public static Vector3 b = new Vector3(EditorPrefs.GetFloat("CapsuleBX", .1f), EditorPrefs.GetFloat("CapsuleBY", .25f), EditorPrefs.GetFloat("CapsuleBZ", .25f));
    public static float r = EditorPrefs.GetFloat("CapsuleR", .25f);
};
public struct InfiniteCylinderDimensions
{
    public static Vector3 c = new Vector3(EditorPrefs.GetFloat("ICCX", 0), EditorPrefs.GetFloat("ICCY", .25f), EditorPrefs.GetFloat("ICCZ", .25f));
};
public struct BoxDimensions
{
    public static float size = EditorPrefs.GetFloat("BoxSize", .25f);
};
public struct RoundBoxDimensions
{
    public static float size = EditorPrefs.GetFloat("RoundBoxSize", .3f);
    public static float roundFactor = EditorPrefs.GetFloat("RoundBoxFactor", .1f);
};
public struct RoundedCylinderDimensions
{
    public static float ra = EditorPrefs.GetFloat("RCra", .25f);
    public static float rb = EditorPrefs.GetFloat("RCrb", .1f);
    public static float h = EditorPrefs.GetFloat("RCh", .25f);
};
public struct BoxFrameDimensions
{
    public static Vector3 size = new Vector3(EditorPrefs.GetFloat("BFSizeX", .5f), EditorPrefs.GetFloat("BFSizeY", .3f), EditorPrefs.GetFloat("BFSizeZ", .2f));
    public static float cavity = EditorPrefs.GetFloat("BFc", .1f);
};
public struct SolidAngleDimensions
{
    public static Vector2 c = new Vector2(EditorPrefs.GetFloat("SAcX", .25f), EditorPrefs.GetFloat("SAcY", .25f));
    public static float ra = EditorPrefs.GetFloat("SAcra", .5f);
};
public struct CutSphereDimensions
{
    public static float r = EditorPrefs.GetFloat("CSr", .25f);
    public static float h = EditorPrefs.GetFloat("CSh", .1f);
};
public struct HollowSphereDimensions
{
    public static float r = EditorPrefs.GetFloat("HSr", .35f);
    public static float h = EditorPrefs.GetFloat("HSh", .05f);
    public static float t = EditorPrefs.GetFloat("HSt", .05f);
};
public struct DeathStarDimensions
{
    public static float ra = EditorPrefs.GetFloat("DSra", .5f);
    public static float rb = EditorPrefs.GetFloat("DSrb", .35f);
    public static float d = EditorPrefs.GetFloat("DSd", .5f);
};
public struct RoundConeDimensions
{
    public static float r1 = EditorPrefs.GetFloat("RCr1", .1f);
    public static float r2 = EditorPrefs.GetFloat("RCr2", .25f);
    public static float h = EditorPrefs.GetFloat("RCh", .4f);
};
public struct EllipsoidDimensions
{
    public static Vector3 Radius = new Vector3(EditorPrefs.GetFloat("EDrX", .18f), EditorPrefs.GetFloat("EDrY", .3f), EditorPrefs.GetFloat("EDrZ", .1f));
};
public struct RhombusDimensions
{
    public static float la = EditorPrefs.GetFloat("RDla", .6f);
    public static float lb = EditorPrefs.GetFloat("RDlb", .2f);
    public static float h = EditorPrefs.GetFloat("RDh", .02f);
    public static float ra = EditorPrefs.GetFloat("RDra", .02f);
};
public struct OctahedronDimensions
{
    public static float size = EditorPrefs.GetFloat("OctaSize", .5f);
};
public struct PyramidDimensions
{
    public static float size = EditorPrefs.GetFloat("PryramidSize", .5f);
};
public struct TriangleDimensions
{
    public static Vector3 sideA = new Vector3(EditorPrefs.GetFloat("TAX", .3f), EditorPrefs.GetFloat("TAY", .5f), EditorPrefs.GetFloat("TAZ", .15f));
    public static Vector3 sideB = new Vector3(EditorPrefs.GetFloat("TBX", .8f), EditorPrefs.GetFloat("TBY", .2f), EditorPrefs.GetFloat("TBZ", .1f));
    public static Vector3 sideC = new Vector3(EditorPrefs.GetFloat("TCX", .7f), EditorPrefs.GetFloat("TCY", .3f), EditorPrefs.GetFloat("TCZ", .5f));
};
public struct QuadDimensions
{
    public static Vector3 sideA = new Vector3(EditorPrefs.GetFloat("QAX", .3f), EditorPrefs.GetFloat("QAY", .5f), EditorPrefs.GetFloat("QAZ", .15f));
    public static Vector3 sideB = new Vector3(EditorPrefs.GetFloat("QBX", .8f), EditorPrefs.GetFloat("QBY", .2f), EditorPrefs.GetFloat("QBZ", 0));
    public static Vector3 sideC = new Vector3(EditorPrefs.GetFloat("QCX", .9f), EditorPrefs.GetFloat("QCY", .3f), EditorPrefs.GetFloat("QCZ", .5f));
    public static Vector3 sideD = new Vector3(EditorPrefs.GetFloat("QDX", .1f), EditorPrefs.GetFloat("QDY", .2f), EditorPrefs.GetFloat("QDZ", .5f));
};

public struct FractalDimenisons
{
    public static float i = EditorPrefs.GetFloat("Fraci", 10);
    public static float s = EditorPrefs.GetFloat("Fracs", 1.25f);
    public static float o = EditorPrefs.GetFloat("Fraco", 2);
};
public struct TesseractDimensions
{
    public static Vector4 size = new Vector4(EditorPrefs.GetFloat("TessX", .25f), EditorPrefs.GetFloat("TessY", .25f), EditorPrefs.GetFloat("TessZ", .25f), EditorPrefs.GetFloat("TessW", .25f));
};