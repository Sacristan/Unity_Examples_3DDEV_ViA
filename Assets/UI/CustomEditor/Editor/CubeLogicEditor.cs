using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CubeLogic))]
public class CubeLogicEditor : Editor {

    CubeLogic cubeLogic;

    void OnEnable()
    {
        cubeLogic = (CubeLogic) this.target;
    }

    public override void OnInspectorGUI()
    {
        this.DrawDefaultInspector();

        if (GUILayout.Button("Destroy")){
            DestroyImmediate(cubeLogic.gameObject);
        }

    }


}
