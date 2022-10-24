using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObjects/ItemSO")]
public class ItemScriptableObject : ScriptableObject
{

    [System.Serializable]
    public class Column
    {
        public ItemScriptableObject[] rows = new ItemScriptableObject[6];
    }

    public Column[] recipe = new Column[6];


}
