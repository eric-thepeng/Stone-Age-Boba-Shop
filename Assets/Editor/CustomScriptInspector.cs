using UnityEngine;
using UnityEditor;
using Unity.Collections;

[CustomEditor(typeof(ItemScriptableObject))]
public class CustomScriptInscpector : Editor
{

    ItemScriptableObject targetScript;

    void OnEnable()
    {
        targetScript = target as ItemScriptableObject;
    }

    public override void OnInspectorGUI()
    {

        EditorGUILayout.IntField(6);
        EditorGUILayout.IntField(6);

        EditorGUILayout.BeginHorizontal();
        for (int y = 0; y < 6; y++)
        {
            EditorGUILayout.BeginVertical();
            for (int x = 0; x < 6; x++)
            {
                //targetScript.recipe[x].rows[y] = EditorGUILayout.ObjectField(targetScript.recipe[x].rows[y], typeof(ScriptableObject), false);
            }
            EditorGUILayout.EndVertical();

        }
        EditorGUILayout.EndHorizontal();

    }
}
