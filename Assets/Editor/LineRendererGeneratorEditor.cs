using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LineRendererGenerator))]
public class LineRendererGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LineRendererGenerator generator = (LineRendererGenerator)target;

        if (GUILayout.Button("Generate Line Renderer Circle"))
        {
            generator.GenerateCircle();
        }
    }
}
