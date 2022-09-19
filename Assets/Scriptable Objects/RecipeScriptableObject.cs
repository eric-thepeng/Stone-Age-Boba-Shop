using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeSO", menuName = "ScriptableObjects/RecipeSO")]
public class RecipeScriptableObject : ScriptableObject
{
    string name;
    List<ItemScriptableObject> ItemList;
    List<ActionScriptableObject> ActionList;
    ItemScriptableObject ItemProduce;
    ActionScriptableObject ActionProduce;
}
