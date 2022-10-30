using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemScriptableObject", menuName = "ScriptableObjects/ItemScriptableObject")]
public class ItemScriptableObject : SerializedScriptableObject
{
    //Variables

    public string name;
    public List<Recipe> allRecipes = new List<Recipe>();
    public List<KeyValuePair<Vector2, ScriptableObject>> FormationRecipeCoord
    {
        get
        {
            return allRecipes[0].getCoordForm();
        }
    }


    public bool CheckMatch(List<KeyValuePair<Vector2, ScriptableObject>> toCheck)
    {
        foreach (Recipe r in allRecipes)
        {
            if (r.CheckMatch(toCheck)) return true;
        }
        return false;
    }


    //Each Recipe class contains one recipe, can be accessed by methods.

    public class Recipe
    {
        //[TableMatrix(HorizontalTitle = "Recipe Matrix", SquareCells = false)

        public ScriptableObject[,] recipe = new ScriptableObject[6, 6];

        //Get CoordForm of Recipe, the only form accessible.
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

        public bool CheckMatch(List<KeyValuePair<Vector2, ScriptableObject>> toCheck)
        {
            List<KeyValuePair<Vector2, ScriptableObject>> toMatch = getCoordForm();
            if (toCheck.Count != toMatch.Count) return false;
            foreach(KeyValuePair<Vector2, ScriptableObject> kvp in toCheck)
            {
                if (!toMatch.Contains(kvp)) return false;
            }
            return true;
        }
    }
}
