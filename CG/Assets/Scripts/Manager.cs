using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Manager : MonoBehaviour
{
    void RandomizeShape()
    {
        List<RaymarchRenderer> renderers = new List<RaymarchRenderer>(FindObjectsOfType<RaymarchRenderer>());

        float rand_radius = UnityEngine.Random.Range(.5f, 1f);
        float rand_height = UnityEngine.Random.Range(1.5f, 2.5f);
        float rand_smol_height = UnityEngine.Random.Range(.2f, .5f);

        renderers[1].cyl.h = rand_smol_height;
        renderers[1].cyl.r = rand_radius * .5f;
        
        renderers[0].cap.h = rand_smol_height;
        renderers[0].cap.r1 = rand_radius;
        renderers[0].cap.r2 = rand_radius * .5f;

        renderers[2].cyl.r = rand_radius;
        renderers[2].cyl.h = rand_height;
        
    }
    void CreateShape(string _shape, Vector3 _pos, Vector3 _col, Vector3 _rot, Vector3 _dim)
    {
        GameObject shape = new GameObject("Shape");
        shape.AddComponent<RaymarchRenderer>();

        RaymarchRenderer rr = shape.GetComponent<RaymarchRenderer>();

        rr.shape = (RaymarchRenderer.Shape)Enum.Parse(typeof(RaymarchRenderer.Shape), _shape);
        rr.color = new Color(_col.x, _col.y, _col.z);

        shape.transform.position = _pos;
        shape.transform.eulerAngles = _rot * Mathf.Rad2Deg;

        switch ((int)rr.shape)
        {
            case 0:
                rr.cyl.r = _dim.x;
                rr.cyl.h = _dim.y;
                break;
            case 1:
                rr.cap.r1 = _dim.x;
                rr.cap.r2 = _dim.y;
                rr.cap.h=  _dim.z;
                break;
        }
    }
   
    private void Start()
    {
        //InvokeRepeating("RandomizeShape", .05f, .05f);
        CreateShape("Frustrum", Vector3.one, new Vector3(255, 255, 0), Vector3.one, new Vector3(.5f, 1.5f, .5f));
    }
    public struct Shape
    {
        public Vector3 pos;
        public Vector3 rot;
        public Vector3 col;
        public float blendFactor;
        public int shapeIndex;
        public Vector3 dimensions;
    }
}