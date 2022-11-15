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

    /// <summary>
    /// Information to be displayed on each grid.
    /// </summary>
    public class ItemInfo
    {
        public RectTransform uiBlock;
        public ItemScriptableObject soForm;
        public Sprite objectSprite, tetrisSprite;
        public int amount;

        public ItemInfo(ItemScriptableObject inSO)
        {
            uiBlock = null;
            soForm = inSO;
            objectSprite = soForm.objectSprite;
            tetrisSprite = soForm.tetrisSprite;
            amount = 0;
        }

        public void AddOne()
        {
            amount += 1;
        }

        public void MinusOne()
        {
            amount -= 1;
        }
    }

    public List<ItemInfo> Backpack = new List<ItemInfo>();

    public void AddItem(ItemScriptableObject so)
    {
        foreach(ItemInfo ii in Backpack)
        {
            if(ii.soForm == so)
            {
                ii.AddOne();
                return;
            }
        }
        Backpack.Add(new ItemInfo(so));
    }

    public void DeleteItem(ScriptableObject so)
    {
        foreach (ItemInfo ii in Backpack)
        {
            if (ii.soForm == so)
            {
                ii.MinusOne();
                return;
            }
        }
        print("Backpack does not have this item: " + so);
    }
}
