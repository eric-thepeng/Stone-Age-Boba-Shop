using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : UpGroundObj
{
    [SerializeField] GameObject objProduce;
    [SerializeField] ItemScriptableObject isoToGet;
    // Start is called before the first frame update

    public void PickUp()
    {
        if (objProduce != null)
        {
            GameObject ngo = Instantiate(objProduce, transform.parent);
            ngo.transform.position = transform.position;
            ngo.GetComponent<mushroom>().SetUp(GetComponent<SpriteRenderer>().sprite, isoToGet);
        }
        Destroy(gameObject);
    }
}