using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor
{
    GameEvent _target;

    private void OnEnable()
    {
        _target = target as GameEvent;
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        if (_target.listeners.Count > 0)
            EditorGUILayout.LabelField("Listeners: ", EditorStyles.boldLabel);
        {
            _target.listeners.ForEach(listener =>
            {
                if (listener.transform.parent)
                {
                    EditorGUILayout.LabelField(listener.transform.parent.name);
                }
                else
                {
                    EditorGUILayout.LabelField(listener.name);
                }
            });
        }
        serializedObject.ApplyModifiedProperties();
    }
}
