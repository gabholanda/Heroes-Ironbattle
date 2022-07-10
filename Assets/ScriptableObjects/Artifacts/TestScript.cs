using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(InventoryItem<Artifact>))]
public class TestScript : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("Item"));
        //EditorGUILayout.LabelField("Level", myTarget.Level.ToString());
    }
}
