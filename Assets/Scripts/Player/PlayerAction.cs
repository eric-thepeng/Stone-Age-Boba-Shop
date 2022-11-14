using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    Gatherable tarGatherable;
    CapsuleCollider myCollider;
    public enum tool {Hand, Scythe, Axe}
    tool toolNow = tool.Axe;

    private void Awake()
    {
        myCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        if (tarGatherable == null) return;
        if (Input.GetMouseButton(0))
        {
            tarGatherable.Gather(toolNow);
        }
        if (tarGatherable.CheckEmpty())
        {
            tarGatherable = null;
            refreshCollider();
        }

    }

    void refreshCollider()
    {
        myCollider.enabled = false;
        myCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tarGatherable == null && other.gameObject.GetComponent<Gatherable>() != null)
        {
            tarGatherable = other.gameObject.GetComponent<Gatherable>();
        }
    }

    private void OnTriggerExit(Collider other) //exit a area
    {
        if (other.gameObject.GetComponent<Gatherable>() == null) return; //ignore if not gatherable
        if(tarGatherable != null && tarGatherable == other.gameObject.GetComponent<Gatherable>()) //isGatherable and is tarGatherable, abandonGather and dislink
        {
            tarGatherable.AbandonGather();
            tarGatherable = null;
        }
    }
}
