using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    static Inventory instance;
    public static Inventory i
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<Inventory>();
            return instance;
        }

    }

    List<ScriptableObject> Backpack;

    public void ObtainItem(ScriptableObject so)
    {
        Backpack.Add(so);
    }

    public void DeleteItem()
    {

    }
}
