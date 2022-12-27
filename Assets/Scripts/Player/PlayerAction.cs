using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    UpGroundObj tarObj;
    //Gatherable tarGatherable;
    CapsuleCollider myCollider;
    public enum tool {Hand, Scythe, Axe}
    tool toolNow = tool.Axe;

    private void Awake()
    {
        myCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        if (tarObj == null) return;
        if (tarObj.isGatherable() != null)
        {
            if (Input.GetMouseButton(0))
            {
                tarObj.isGatherable().Gather(toolNow);
            }
            if (tarObj.isGatherable().CheckEmpty())
            {
                tarObj = null;
                refreshCollider();
            }
        }else if (tarObj.isPickable() !=null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                tarObj.isPickable().PickUp();
            }
        }
    }

    void refreshCollider()
    {
        myCollider.enabled = false;
        myCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tarObj == null && other.gameObject.GetComponent<UpGroundObj>() != null)
        {
            tarObj = other.gameObject.GetComponent<UpGroundObj>();
        }
    }

    private void OnTriggerExit(Collider other) //exit a area
    {
        if (other.gameObject.GetComponent<UpGroundObj>() == null) return; //ignore if not UpGroundObj

        if(tarObj != null && tarObj.isGatherable()) //isGatherable and is tarGatherable, abandonGather and dislink
        {
            tarObj.isGatherable().AbandonGather();
        }

        tarObj = null;
    }
}
