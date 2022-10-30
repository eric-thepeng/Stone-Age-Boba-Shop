using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    public enum facing {Up, Down, Left, Right}
    public facing myFacing;

    public Vector2 attachedCoordination;

    public Tetris myTetris;
    public Tetris oppositeTetris = null;

    public List<Edge> touchingEdges = new List<Edge>();

    void Start()
    {
        GetComponentInParent<Tetris>().allEdges.Add(this);
        myTetris = GetComponentInParent<Tetris>();
    }

    public void RefreshState() 
    {
        //print(transform.parent.name + " " + name + " oppositeTetris: " +oppositeTetris);
        if (touchingEdges.Count == 0) return;
        //print(transform.parent.name + " " + name + " has touching edge");
        if (oppositeTetris != null) return;
        //print(transform.parent.name + " " + name + "is refreshing");
    
        oppositeTetris = touchingEdges[0].getTetris();
        touchingEdges[0].RefreshState();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("in");
        if (collision.GetComponent<Edge>() != null && checkFacingMatch(myFacing, collision.GetComponent<Edge>().myFacing))
        {
            touchingEdges.Add(collision.GetComponent<Edge>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Edge>() != null)
        {
            touchingEdges.Remove(collision.GetComponent<Edge>());
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        //print("in");
        if (other.GetComponent<Edge>() != null)
        {
            touchingEdges.Add(other.GetComponent<Edge>());
            //print("add");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //print("stay");

    }

    private void OnTriggerExit(Collider other)
    {
        //print("out");
        if (other.GetComponent<Edge>() != null)
        {
            touchingEdges.Remove(other.GetComponent<Edge>());
        }
    }*/
    
    public Tetris getTetris() { return myTetris; }
    public bool isConnected() { return oppositeTetris != null; }
    public Tetris getOppositeTetris() { return oppositeTetris; }
    public Edge getOppositeEdge() { return touchingEdges[0]; }
    public Vector3 getOppositeEdgeDistance() {return (touchingEdges[0].transform.position - transform.position);}
    public Vector2 getAttachedCoord() { return attachedCoordination; }

    public Vector2 getAttachToDirection()
    {
        if (myFacing == facing.Left) return new Vector2(-1,0);
        else if (myFacing == facing.Right) return new Vector2(1, 0);
        else if (myFacing == facing.Down) return new Vector2(0, 1);
        else if (myFacing == facing.Up) return new Vector2(0, -1);

        print("error at getAttachToDirection()");
        return new Vector2(0, 0);
    }

    public bool checkFacingMatch(facing one, facing two)
    {
        if (one == facing.Up && two == facing.Down) return true;
        if (one == facing.Down && two == facing.Up) return true;
        if (one == facing.Left && two == facing.Right) return true;
        if (one == facing.Right && two == facing.Left) return true;
        return false;
    }
    
}
