using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemScriptableObject", menuName = "ScriptableObjects/ItemScriptableObject")]
public class ItemScriptableObject : SerializedScriptableObject
{
    public string name;
    public List<Recipe> allRecipes = new List<Recipe>();

    public Recipe getFormationRecipe()
    {
        return allRecipes[0];
    }
    public class Recipe
    {
        //   [TableMatrix(HorizontalTitle = "Recipe Matrix", SquareCells = false)
        public ScriptableObject[,] recipe = new ScriptableObject[6, 6];
        public List<KeyValuePair<Vector2, ScriptableObject>> getCoordForm()
        {
            List<KeyValuePair<Vector2, ScriptableObject>> export = new List<KeyValuePair<Vector2, ScriptableObject>>();
            for (int x = 0; x < recipe.GetLength(0); x++)
            {
                for (int y = 0; y < recipe.GetLength(1); y++)
                {
                    if (recipe[x, y] == null) continue;
                    export.Add(new KeyValuePair<Vector2, ScriptableObject>(new Vector2(x, y), recipe[x,y]));
                }
            }
            return export;
        }
    }



}
