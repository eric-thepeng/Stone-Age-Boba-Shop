using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TetrisInfo
{
    public ScriptableObject type;
    public Vector2 position;
    public TetrisInfo(ScriptableObject t, Vector2 v)
    {
        this.type = t;
        this.position = v;
    }
}

public class Tetris : MonoBehaviour
{
    TetrisInfo myInfo;

    ScriptableObject myType;

    List<TetrisInfo> recipe = new List<TetrisInfo>();

    public List<Edge> allEdges = new List<Edge>();

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnMouseUp()
    {
        foreach(Edge e in allEdges)
        {
            e.RefreshState();
        }
        recipe = new List<TetrisInfo>();
        Search(recipe);
        CheckRecipe();
    }

    void CheckRecipe()
    {
        int c = 1;
        foreach (TetrisInfo t in recipe)
        {
            print("" + c + t.position);
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
