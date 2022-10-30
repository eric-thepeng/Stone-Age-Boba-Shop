using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOListScriptableObject", menuName = "ScriptableObjects/SOListScriptableObject")]
public class SOListScriptableObject : SerializedScriptableObject
{
    public List<ScriptableObject> list = new List<ScriptableObject>();
}
