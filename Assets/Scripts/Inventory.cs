using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public class Item
    {

    }

    static Inventory instance;
    public static Inventory i
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<Inventory>();
            return instance;
        }

    }

    Dictionary<ScriptableObject, int> Backpack = new Dictionary<ScriptableObject, int>();

    public void AddItem(ScriptableObject so)
    {
        if (Backpack.ContainsKey(so))
        {
            Backpack[so] += 1;
        }
        else
        {
            Backpack.Add(so,1);
        }
    }

    public void DeleteItem(ScriptableObject so)
    {
        if (Backpack.ContainsKey(so))
        {
            Backpack[so] -= 1;
            if(Backpack[so] == 0)
            {
                Backpack.Remove(so);
            }
        }
        else
        {
            print("Backpack does not have item" + so);
        }
    }

    public void DisplayInventory()
    {

    }
}
