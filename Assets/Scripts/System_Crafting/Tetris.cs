using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TetrisInfo
{
    /*
    public ScriptableObject type;
    public Vector2 position;
    public TetrisInfo(ScriptableObject t, Vector2 v)
    {
        this.type = t;
        this.position = v;
    }*/

    public string type;
    public Vector2 position;
    public TetrisInfo(string t, Vector2 v)
    {
        this.type = t;
        this.position = v;
    }

}

public class Tetris : MonoBehaviour
{
    TetrisInfo myInfo;

    //ScriptableObject myType;
    public string myType;

    List<TetrisInfo> recipe = new List<TetrisInfo>();

    public List<Edge> allEdges = new List<Edge>();

    public Vector2 dragDisplacement = new Vector2(0,0);
    public Vector3 mouseDownPos = new Vector2(0, 0);
    public Vector3 tetrisDownPos = new Vector3(0, 0, 0);



    private void OnMouseDown()
    {
        ResetEdges();
        mouseDownPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tetrisDownPos = transform.position;
    }

    private void OnMouseDrag()
    {
        transform.position = tetrisDownPos + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseDownPos); //TODO maintain Z
    }

    private void OnMouseUp()
    {
        RefreshEdges();
        recipe = new List<TetrisInfo>();
        Search(recipe);
        CheckRecipe();
    }

    public void RefreshEdges()
    {
        foreach (Edge e in allEdges)
        {
            e.RefreshState();
        }
    }

    public void ResetEdges()
    {
        foreach (Edge e in allEdges)
        {
            e.ResetState();
        }
    }

    void CheckRecipe()
    {
        int c = 1;
        foreach (TetrisInfo t in recipe)
        {
            print("" + c + t.type + " " + t.position);
            c++;
        }
    }

    void Search(List<TetrisInfo> Recipe)
    {
        List<Edge> toProcess = new List<Edge>(allEdges);
        foreach(Edge e in toProcess)
        {
            if (!e.isConnected()) return;
            if (Recipe.Contains(e.getOppositeTetris().GetTetrisInfo())) return;
            e.getOppositeTetris().Search(Recipe);
        }
    }

    public TetrisInfo GetTetrisInfo() {
        myInfo = new TetrisInfo(myType, transform.position);
        return myInfo;
    }
}
