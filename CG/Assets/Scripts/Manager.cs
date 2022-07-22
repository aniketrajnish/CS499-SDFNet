using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Manager : MonoBehaviour
{
    void RandomizeShape()
    {
        List<RaymarchRenderer> renderers = new List<RaymarchRenderer>(FindObjectsOfType<RaymarchRenderer>());

        float rand_radius = Random.Range(.5f, 1f);
        float rand_height = Random.Range(1.5f, 2.5f);
        float rand_smol_height = Random.Range(.2f, .5f);

        renderers[0].cyl.h = rand_smol_height;
        renderers[0].cyl.r = rand_radius * .5f;
        
        renderers[1].cap.h = rand_smol_height;
        renderers[1].cap.r1 = rand_radius;
        renderers[1].cap.r2 = rand_radius * .5f;

        renderers[2].cyl.r = rand_radius;
        renderers[2].cyl.h = rand_height;
    }
    private void Start()
    {
        InvokeRepeating("RandomizeShape", .5f, .5f);
    }
}