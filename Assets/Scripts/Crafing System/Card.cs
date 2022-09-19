using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    
    void OnMouseDown()
    {
        // Destroy the gameObject after clicking on it
        Destroy(gameObject);
    }
    private void OnMouseDrag()
    {
        
    }

    private void OnMouseUp()
    {

    }
}
