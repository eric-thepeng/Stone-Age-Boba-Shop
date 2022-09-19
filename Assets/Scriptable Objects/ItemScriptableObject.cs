using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObjects/ItemSO")]
public class ItemScriptableObject : ScriptableObject
{
    public string name;
    public Sprite craftingSprite;
}
