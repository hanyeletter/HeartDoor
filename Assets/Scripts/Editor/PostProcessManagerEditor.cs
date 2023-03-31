using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PostProcessManager))]
public class PostProcessManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Water Wave"))
        {
            PostProcessManager.instance?.UseWaterWave();
        }
    }
}
