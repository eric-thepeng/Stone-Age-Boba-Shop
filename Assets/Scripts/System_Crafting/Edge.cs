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
        if (touchingEdges[0] == null) return;
        if (oppositeTetris != null) return;
    
        oppositeTetris = touchingEdges[0].getTetris();
        oppositeTetris.RefreshEdges();
    }

    public void ResetState()
    {
        if(oppositeTetris != null)
        {
            foreach (Edge e in oppositeTetris.allEdges)
            {
                if (e.oppositeTetris == myTetris)
                {
                    e.oppositeTetris = null;
                }
            }

        }
        oppositeTetris = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Edge>() != null)
        {
            touchingEdges.Add(other.GetComponent<Edge>());
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
