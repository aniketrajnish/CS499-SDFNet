using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Manager : MonoBehaviour
{
    int count = 5;
    string[] shapes;
    Vector3[] positions, colors, rotations, dimensions;
    public TextAsset inputCSV;
    private char space = ',';
    private char lineBreak = '\n';
    private void OnEnable()
    {
        DrawShape(2);
        //InvokeRepeating("RandomizeShape", .05f, .05f);
        shapes = new string[] { "Cylinder", "Frustrum", "Cylinder", "Frustrum", "Cylinder"};
        positions = new Vector3[count];
        colors = new Vector3[count];
        rotations = new Vector3[count];
        dimensions = new Vector3[count];

        /*for (int i = 0; i < count; i++)
        {
            Vector3 rand = new Vector3(UnityEngine.Random.Range(-1f, 1f),
                UnityEngine.Random.Range(-1f, 1f),
                UnityEngine.Random.Range(-1f, 1f));

            Vector3 rand_abs = new Vector3(Mathf.Abs(rand.x),
                Mathf.Abs(rand.y),
                Mathf.Abs(rand.z));

            shapes[i] = shapes[UnityEngine.Random.Range(0, shapes.Length)];
            positions[i] = 2f * rand;
            colors[i] = rand_abs;
            rotations[i] = 2f * rand;
            dimensions[i] = 2f * rand_abs;

            CreateShape(shapes[i], positions[i], colors[i], rotations[i], dimensions[i]);
        }*/
    }
    string[] readInput(int index)
    {
        string[] datasets = inputCSV.text.Split(lineBreak);

        for (int i = 0; i < datasets.Length; i++)
        {
            string[] data = datasets[i].Split(space);

            if (i == index)            
                return data;            
        }
        return null;
    }
    private void DrawShape(int index)
    {
        string[] shapeData = readInput(index);
        //print(shapeData[shapeData.Length-1]);
        List<string> shapes  = new List<string>();
        List<Vector3> colors = new List<Vector3>();
        List<Vector3> dimensions = new List<Vector3>();
        List<Vector3> rotations = new List<Vector3>();
        List<Vector3> positions = new List<Vector3>();
      
        string[] first_row = readInput(0);

        for (int j = 0; j < first_row.Length; j++)
        {
            string inp = first_row[j];

            if (inp == "r1" || inp == "r2" || inp == "r3")
                colors.Add(new Vector3(float.Parse(shapeData[j]),
                    float.Parse(shapeData[j + 1]),
                    float.Parse(shapeData[j + 2])));            

            if (inp == "Shape")                            
                shapes.Add(char.ToUpper(shapeData[j][0]) + shapeData[j].Substring(1));

            if (inp == "rBC" || inp == "rTC" || inp == "r1F")
            {
                dimensions.Add(new Vector3(float.Parse(shapeData[j]),
                    float.Parse(shapeData[j + 1]),
                    float.Parse(shapeData[j + 2])));
            }
        }

        for (int i = 0; i < 3; i++)
        {
            rotations.Add(Vector3.zero);
            positions.Add(Vector3.zero);
            CreateShape(shapes[i], positions[i], colors[i], rotations[i], dimensions[i]);
        }        
    }
    private void OnDisable()
    {
        List<RaymarchRenderer> renderers = new List<RaymarchRenderer>(FindObjectsOfType<RaymarchRenderer>());

        foreach (RaymarchRenderer rr in renderers)
            DestroyImmediate(rr.gameObject);
        /*foreach (RaymarchRenderer rr in renderers)        
            Destroy(rr.gameObject);*/        
    }
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
                rr.cap.h =  _dim.z;
                break;
        }
    }       
}