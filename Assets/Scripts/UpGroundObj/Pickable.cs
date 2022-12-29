using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : UpGroundObj
{
    [SerializeField] GameObject objProduce;
    [SerializeField] int produceAmount = 1;
    [SerializeField] ItemScriptableObject isoToGet;
    // Start is called before the first frame update

    public void PickUp()
    {
        if (objProduce != null)
        {
            StartCoroutine(PickUpIE());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator PickUpIE()
    {
        int amount = 0;
        while(amount < produceAmount)
        {
            GameObject ngo = Instantiate(objProduce, transform.parent);
            ngo.transform.position = transform.position;
            ngo.GetComponent<mushroom>().SetUp(GetComponent<SpriteRenderer>().sprite, isoToGet);
            amount += 1;
            if(amount == produceAmount)
            {
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
}
