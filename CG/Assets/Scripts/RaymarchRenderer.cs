using System;
using UnityEngine;
[ExecuteInEditMode]
public class RaymarchRenderer : MonoBehaviour
{    
    public enum Shape
    {
        Cylinder,
        Frustrum
    };    

    public Shape shape;
    public Color color;
    public CylinderDimensions cyl = new CylinderDimensions();
    public CappedConeDimensions cap = new CappedConeDimensions();

    [Range(.1f, 100)]
    public float blendFactor;
   
    public Vector3 GetDimensionVectors(int i)
    {
        int len = Enum.GetNames(typeof(RaymarchRenderer.Shape)).Length;

        Vector3[] dimensions = new Vector3[len];

        //cylinder        
        dimensions[0] = new Vector3(cyl.r, cyl.h, 0);

        //capped cone        
        dimensions[1] = new Vector3(cap.r1, cap.r2, cap.h);

        return dimensions[i];
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
}




