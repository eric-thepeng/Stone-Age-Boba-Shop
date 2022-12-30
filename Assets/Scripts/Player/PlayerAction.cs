using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    UpGroundObj tarObj;
    //Gatherable tarGatherable;
    CapsuleCollider myCollider;
    public enum tool {Hand, Shovel, Axe}
    tool toolNow = tool.Hand;
    private void Awake()
    {
        myCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //Hand -> Shovel -> Axe -> Hand
            if (toolNow == tool.Hand)
            {
                if (UI_Tools.i.hasShovel())
                {
                    toolNow = tool.Shovel;
                    UI_Tools.i.switchTool(2);
                }
                else if (UI_Tools.i.hasAxe())
                {
                    toolNow = tool.Axe;
                    UI_Tools.i.switchTool(3);
                }
            }
            else if (toolNow == tool.Shovel)
            {
                if (UI_Tools.i.hasAxe())
                {
                    toolNow = tool.Axe;
                    UI_Tools.i.switchTool(3);
                }
                else
                {
                    toolNow = tool.Hand;
                    UI_Tools.i.switchTool(1);
                }
            }
            else if (toolNow == tool.Axe)
            {
                toolNow = tool.Hand;
                UI_Tools.i.switchTool(1);
            }
        }

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
                CanvasManager.i.CloseTargetInfo();
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
            /*
            tarHighlight = new GameObject("shadow of " + other.gameObject.name);
            tarHighlight.transform.parent = other.transform;
            tarHighlight.transform.localPosition = new Vector3(0, 0.0f, 0.1f);
            tarHighlight.transform.localScale = new Vector3(1.2f, 1.2f, 1); //gameObject.transform.localScale;
            tarHighlight.transform.localRotation = Quaternion.identity;
            tarHighlight.AddComponent<SpriteRenderer>();
            tarHighlight.GetComponent<SpriteRenderer>().sprite = other.GetComponent<SpriteRenderer>().sprite;
            tarHighlight.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);*/
            CanvasManager.i.SetUpTargetInfo(other.gameObject.name);
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

        //Destroy(tarHighlight);
        CanvasManager.i.CloseTargetInfo();
        tarObj = null;
    }
}
