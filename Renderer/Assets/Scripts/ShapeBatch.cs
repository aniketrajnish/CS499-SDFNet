using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeBatch : MonoBehaviour
{
    [SerializeField] int max_shapes, dataset_size;
    int batch_size = 1;
    [SerializeField] string save_path;
    public RaymarchRenderer.Shape[] shapes;
    public RaymarchRenderer.Operation[] operations;
    Camera _cam;
    private void Start()
    {
        _cam = Camera.main;
        StartCoroutine(RenderShapes());
    }

    private IEnumerator RenderShapes()
    {
        for (int i = 0; i < dataset_size; i += batch_size)
        {
            int shape_count = Random.Range(1, max_shapes);

            shapes = (RaymarchRenderer.Shape[])System.Enum.GetValues(typeof(RaymarchRenderer.Shape));
            operations = (RaymarchRenderer.Operation[])System.Enum.GetValues(typeof(RaymarchRenderer.Operation));

            RenderTexture rt = RenderTexture.GetTemporary(256, 256, 24);
            List<string> file_names = new List<string>();

            string out_name = "";

            for (int j = 0; j < shape_count; j++)
            {
                RaymarchRenderer.Shape shape = shapes[Random.Range(0, shapes.Length)];
                RaymarchRenderer.Operation operation = operations[0];

                GameObject go = new GameObject();
                go.AddComponent<RaymarchRenderer>();
                go.GetComponent<RaymarchRenderer>().shape = shape;
                go.GetComponent<RaymarchRenderer>().operation = operation;
                vector12 dimensions = GetRandomDimensions(shape);
                go.GetComponent<RaymarchRenderer>().SetDimensionArray(shape, dimensions);
                go.GetComponent<RaymarchRenderer>().color = Random.ColorHSV(0, 1);

                /*out_name += "_" + shape + "_" + operation + "_";
                out_name += "_" + dimensions.a;
                out_name += "_" + dimensions.b;
                out_name += "_" + dimensions.c;
                out_name += "_" + dimensions.d;
                out_name += "_" + dimensions.e;
                out_name += "_" + dimensions.f;
                out_name += "_" + dimensions.g;
                out_name += "_" + dimensions.h;
                out_name += "_" + dimensions.i;
                out_name += "_" + dimensions.j;
                out_name += "_" + dimensions.k;
                out_name += "_" + dimensions.l;*/
             

                RenderToTexture(go, rt);

                Destroy(go);
            }

            file_names.Add("shape_" + i + out_name + ".png");
            yield return StartCoroutine(SaveTexturesToFile(rt, save_path, file_names));
            RenderTexture.ReleaseTemporary(rt);
            file_names.Clear();
        }
    }
    vector12 GetRandomDimensions(RaymarchRenderer.Shape shape)
    {
        vector12 rand_dimensions = new vector12();        

        switch (shape)
        {
            case RaymarchRenderer.Shape.Cylinder:
                rand_dimensions.a = Random.Range(.1f, 5f);
                rand_dimensions.b = Random.Range(.1f, 4.5f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.CappedCone:
                rand_dimensions.a = Random.Range(.1f, 5f);
                rand_dimensions.b = Random.Range(.1f, 5f);
                rand_dimensions.c = Random.Range(.1f, 5f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Shpere:
                rand_dimensions.a = Random.Range(.1f, 3f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Torus:
                rand_dimensions.a = Random.Range(.1f, 3f);
                rand_dimensions.b = Random.Range(.1f, 3f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.CappedTorus:
                rand_dimensions.a = Random.Range(.1f, 1f);
                rand_dimensions.b = Random.Range(.1f, 1f);
                rand_dimensions.c = Random.Range(.1f, 1f);
                rand_dimensions.d = Random.Range(.1f, 1f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Link:
                rand_dimensions.a = Random.Range(.1f, 2f);
                rand_dimensions.b = Random.Range(.1f, 3f);
                rand_dimensions.c = Random.Range(.1f, 1f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Cone:
                rand_dimensions.a = Random.Range(-1f, 1f);
                rand_dimensions.b = Random.Range(-1f, 1f);
                rand_dimensions.c = Random.Range(.1f, 10f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.InfCone:
                rand_dimensions.a = Random.Range(-1f, 1f);
                rand_dimensions.b = Random.Range(-1f, 1f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Plane:
                rand_dimensions.a = Random.Range(.1f, 1f);
                rand_dimensions.b = Random.Range(.1f, 1f);
                rand_dimensions.c = Random.Range(.1f, 1f);
                rand_dimensions.d = Random.Range(.1f, 1f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.HexPrism:
                rand_dimensions.a = Random.Range(.1f, 5f);
                rand_dimensions.b = Random.Range(.1f, 5f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.TriPrism:
                rand_dimensions.a = Random.Range(.1f, 1f);
                rand_dimensions.b = Random.Range(.1f, 1f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Capsule:
                rand_dimensions.a = Random.Range(.1f, 1f);
                rand_dimensions.b = Random.Range(.1f, 1f);
                rand_dimensions.c = Random.Range(.1f, 1f);
                rand_dimensions.d = Random.Range(.1f, 1f);
                rand_dimensions.e = Random.Range(.1f, 1f);
                rand_dimensions.f = Random.Range(.1f, 1f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.InfiniteCylinder:
                rand_dimensions.a = 0;
                rand_dimensions.b = 0;
                rand_dimensions.c = Random.Range(.1f, 3f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Box:
                rand_dimensions.a = Random.Range(.1f, 4f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.RoundBox:
                rand_dimensions.a = Random.Range(.1f, 2.5f);
                rand_dimensions.b = Random.Range(.1f, 2f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.RoundedCylinder:
                rand_dimensions.a = Random.Range(.1f, 2.5f);
                rand_dimensions.b = Random.Range(.1f, 1f);
                rand_dimensions.c = Random.Range(.1f, 2f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.BoxFrame:
                rand_dimensions.a = Random.Range(.1f, 3f);
                rand_dimensions.b = Random.Range(.1f, 3f);
                rand_dimensions.c = Random.Range(.1f, 3f);
                rand_dimensions.d = Random.Range(.1f, 3f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.SolidAngle:
                rand_dimensions.a = Random.Range(-1f, 1f);
                rand_dimensions.b = Random.Range(-1f, 1f);
                rand_dimensions.c = Random.Range(.1f, 5f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.CutSphere:
                rand_dimensions.a = Random.Range(.1f, 5f);
                rand_dimensions.b = Random.Range(.1f, 5f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.CutHollowSphere:
                rand_dimensions.a = Random.Range(.1f, 5f);
                rand_dimensions.b = Random.Range(.1f, 4f);
                rand_dimensions.c = Random.Range(.1f, 1f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.DeathStar:
                rand_dimensions.a = Random.Range(.1f, 10f);
                rand_dimensions.b = Random.Range(.1f, 10f);
                rand_dimensions.c = Random.Range(.1f, 10f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.RoundCone:
                rand_dimensions.a = Random.Range(.1f, 5f);
                rand_dimensions.b = Random.Range(.1f, 5f);
                rand_dimensions.c = Random.Range(.1f, 3f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Ellipsoid:
                rand_dimensions.a = Random.Range(.1f, 3f);
                rand_dimensions.b = Random.Range(.1f, 3f);
                rand_dimensions.c = Random.Range(.1f, 3f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Rhombus:
                rand_dimensions.a = Random.Range(.1f, 1f);
                rand_dimensions.b = Random.Range(.1f, 1f);
                rand_dimensions.c = Random.Range(.1f, 1f);
                rand_dimensions.d = Random.Range(.1f, 1f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Octahedron:
                rand_dimensions.a = Random.Range(.1f, 5f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Pyramid:
                rand_dimensions.a = Random.Range(.1f, 10f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Triangle:
                rand_dimensions.a = Random.Range(.1f, 1f);
                rand_dimensions.b = Random.Range(.1f, 1f);
                rand_dimensions.c = Random.Range(.1f, 1f);
                rand_dimensions.d = Random.Range(.1f, 1f);
                rand_dimensions.e = Random.Range(.1f, 1f);
                rand_dimensions.f = Random.Range(.1f, 1f);
                rand_dimensions.g = Random.Range(.1f, 1f);
                rand_dimensions.h = Random.Range(.1f, 1f);
                rand_dimensions.i = Random.Range(.1f, 1f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Quad:
                rand_dimensions.a = Random.Range(-10f, 10f);
                rand_dimensions.b = Random.Range(-10f, 10f);
                rand_dimensions.c = Random.Range(-10f, 10f);
                rand_dimensions.d = Random.Range(-10f, 10f);
                rand_dimensions.e = Random.Range(-10f, 10f);
                rand_dimensions.f = Random.Range(-10f, 10f);
                rand_dimensions.g = Random.Range(-10f, 10f);
                rand_dimensions.h = Random.Range(-10f, 10f);
                rand_dimensions.i = Random.Range(-10f, 10f);
                rand_dimensions.j = Random.Range(-10f, 10f);
                rand_dimensions.k = Random.Range(-10f, 10f);
                rand_dimensions.l = Random.Range(-10f, 10f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Fractal:
                rand_dimensions.a = Random.Range(.1f, 1f);
                rand_dimensions.b = Random.Range(.1f, 1f);
                rand_dimensions.c = Random.Range(.1f, 1f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Tesseract:
                rand_dimensions.a = Random.Range(.1f, 1f);
                rand_dimensions.b = Random.Range(.1f, 1f);
                rand_dimensions.c = Random.Range(.1f, 1f);
                return rand_dimensions;

        }

        return rand_dimensions;
    }
    private RenderTexture RenderToTexture(GameObject go, RenderTexture rt)
    {
        RenderTexture previousTarget = _cam.targetTexture;
        _cam.targetTexture = rt;
        _cam.Render();        
        _cam.targetTexture = previousTarget;

        return rt;
    }
    private IEnumerator SaveTexturesToFile(RenderTexture rt, string savePath, List<string> fileNames)
    {
        for (int i = 0; i < fileNames.Count; i++)
        {
            Texture2D texture = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);

            RenderTexture.active = rt;
            texture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            RenderTexture.active = null;

            byte[] bytes = texture.EncodeToPNG();
            string filePath = System.IO.Path.Combine(savePath, fileNames[i]);
            System.IO.File.WriteAllBytes(filePath, bytes);

            Destroy(texture);
            yield return null;
        }
    }

}
