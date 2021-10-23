using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Scripting.Python;
using UnityEditor;

[CustomEditor(typeof(python_manager))]
public class python_manager_editor : Editor
{
    python_manager targetManager;

    private void OnEnable() {
        targetManager = (python_manager)target;
    }

    public override void OnInspectorGUI() {
        if(GUILayout.Button("Launch Python Script", GUILayout.Height(35)))
        {
            Debug.Log("This is working!");
            string path = Application.dataPath + "/Python/temp_ai2text.py";
            Debug.Log(path);
            PythonRunner.RunFile(path);
        }
    }
}
