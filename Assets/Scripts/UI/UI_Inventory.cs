using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    public bool showing = false;
    public Transform itemSlotContainer, itemSlotTemplate, itemDescriptionContainer, itemDescriptionImage;

    /// <summary>
    /// Information to be displayed on each grid.
    /// </summary>
    class ItemInfo
    {
        public Transform uiBlock;
        public string objectSprite, tetrisSprite;
        public int amount;
    }

    Dictionary<Vector2, ItemInfo> itemCoord = new Dictionary<Vector2, ItemInfo>();
    Vector2 selectedCoord;

    static UI_Inventory instance;
    public static UI_Inventory i
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<UI_Inventory>();
            return instance;
        }
    }

    
    private void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");

        itemDescriptionContainer = transform.Find("ItemDescriptionContainer");
        itemDescriptionImage = itemDescriptionContainer.Find("Image");

    }


    private void Start()
    {
        //GetComponent<UI_Displayer>().afterHide = AfterClose;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Backspace))
        {
            if (showing) CloseBackpack();
            else SetUpCharms();
        }

        if (!showing) return;

        if (Input.GetKeyDown(KeyCode.A)) ItemMoveSelectedCoord(new Vector2(-1,0));
        if (Input.GetKeyDown(KeyCode.D)) ItemMoveSelectedCoord(new Vector2(1, 0));
        if (Input.GetKeyDown(KeyCode.W)) ItemMoveSelectedCoord(new Vector2(0, 1));
        if (Input.GetKeyDown(KeyCode.S)) ItemMoveSelectedCoord(new Vector2(0,-1));
    }

    
    public void CloseBackpack()
    {
        //StartCoroutine(GetComponent<UI_Displayer>().HidePanel());
    }

    public void OpenBackpack()
    {

    }

    public void AfterClose()
    {
        /*
        foreach (KeyValuePair<Vector2, Inventory.Item> p in itemCoord)
        {
            //Destroy(p.Value.uiBlock.gameObject);
            //p.Value.uiBlock = null;
        }
        itemCoord = new Dictionary<Vector2, Inventory.Item>();
        showing = false;
        equipmentDescriptionContainer.gameObject.SetActive(false);
        charmDescriptionContainer.gameObject.SetActive(false);
        */
    }

    public void SetUpCharms()
    {
        /*
        CloseBackpack();
        showing = true;
        equipmentDescriptionContainer.gameObject.SetActive(true);
        charmDescriptionContainer.gameObject.SetActive(true);
        int x = 0;
        int y = 0;
        int itemSlotCellSize = 120;
        /*itemCoord = new Dictionary<Vector2, Item>();
        foreach (Item c in Inventory.i.MyCharms)
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            c.uiBlock = itemSlotRectTransform;
            c.uiBlock.Find("Image").GetComponent<Image>().sprite = c.sprite;
            c.uiBlock.gameObject.SetActive(true);
            c.uiBlock.anchoredPosition += new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            itemCoord.Add(new Vector2(x, y), c);
            x++;
            if (x >= 4)
            {
                x = 0;
                y--;
            }
        }
        selectedCoord = new Vector2(0, 0);
        ItemMoveSelectedCoord(new Vector2(0, 0));
        StartCoroutine(GetComponent<UI_Displayer>().ShowPanel());
        */
    }

    void ItemMoveSelectedCoord(Vector2 delta)
    {
      /*
        if (!itemCoord.ContainsKey(delta + selectedCoord)) return;
        itemCoord[selectedCoord].uiBlock.Find("WhenSelected").gameObject.SetActive(false);
        selectedCoord += delta;
        itemCoord[selectedCoord].uiBlock.Find("WhenSelected").gameObject.SetActive(true);
      */
        
    }
    
}
