using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    Tetris myTetris;
    Tetris oppositeTetris;

    List<Edge> touchingEdges = new List<Edge>();

    void Start()
    {
        GetComponentInParent<Tetris>().allEdges.Add(this);
        myTetris = GetComponentInParent<Tetris>();
    }

    void Update()
    {
        
    }

    public void RefreshState()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Edge>() != null)
        {
            touchingEdges.Add(other.GetComponent<Edge>());
            oppositeTetris = other.GetComponent<Edge>().getTetris();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Edge>() != null)
        {
            touchingEdges.Remove(other.GetComponent<Edge>());
        }
    }

    public Tetris getTetris() { return myTetris; }
    public bool isConnected() { return oppositeTetris != null; }
    public Tetris getOppositeTetris() { return oppositeTetris; }
    
}
