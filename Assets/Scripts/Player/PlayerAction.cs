using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    Gatherable tarGatherable;

    private void Update()
    {
        if (Input.GetMouseButton(0) && tarGatherable != null)
        {
            tarGatherable.Gather();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tarGatherable == null && other.gameObject.GetComponent<Gatherable>() != null)
        {
            tarGatherable = other.gameObject.GetComponent<Gatherable>();
            print("1");
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
