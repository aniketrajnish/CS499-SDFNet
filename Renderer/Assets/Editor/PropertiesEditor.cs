#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RaymarchRenderer))]

public class PropertiesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Dimensions", EditorStyles.boldLabel);
        RaymarchRenderer rr = (RaymarchRenderer)target;

        switch ((int)rr.shape)
        {           
            case 0:
               rr.cyl.r = EditorGUILayout.FloatField("Radius", rr.cyl.r);
               rr.cyl.h = EditorGUILayout.FloatField("Height", rr.cyl.h);
               

                EditorPrefs.SetFloat("RCr", rr.cyl.r);
                EditorPrefs.SetFloat("RCh", rr.cyl.h);

                break;
            case 1:
                rr.cap.h = EditorGUILayout.FloatField("Height", rr.cap.h);
                rr.cap.r1 = EditorGUILayout.FloatField("Bottom Radius", rr.cap.r1);
                rr.cap.r2 = EditorGUILayout.FloatField("Top Radius", rr.cap.r2);

                EditorPrefs.SetFloat("CCh", rr.cap.h);
                EditorPrefs.SetFloat("CCr1", rr.cap.r1);
                EditorPrefs.SetFloat("CCr2", rr.cap.r2);

                break;            
        } 
        
        EditorUtility.SetDirty(rr);
    }
}
#endif