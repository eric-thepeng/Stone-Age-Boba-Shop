using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    Animator tarCollectable;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && tarCollectable != null)
        {
            tarCollectable.SetTrigger("Collect");
            tarCollectable = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collectable")
        {
            tarCollectable = other.GetComponent<Animator>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (tarCollectable != null && other.gameObject == tarCollectable.gameObject) tarCollectable = null;
    }
}
