using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    Transform topLeft, bottomRight;
    [SerializeField] List<ItemScriptableObject> startingTetris;

    static CraftingManager instance;
    public static CraftingManager i
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<CraftingManager>();
            }
            return instance;
        }
    }

    private void Start()
    {
        topLeft = transform.Find("Crafting Background").Find("Top Left");
        bottomRight = transform.Find("Crafting Background").Find("Bottom Right");
        foreach(ItemScriptableObject iso in startingTetris)
        {
            AddToCrafting(iso.myPrefab);
        }
    }

    public void AddToCrafting(GameObject go)
    {

    }

    public void RemoveFromCrafting(GameObject go)
    {

    }
}
