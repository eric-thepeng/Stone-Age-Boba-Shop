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
    enum state {Wait, Animation, Merge}
    state stateNow = state.Wait;

    TetrisInfo myInfo;

    //ScriptableObject myType;
    public string myType;

    public List<TetrisInfo> recipe = new List<TetrisInfo>();

    public List<Edge> allEdges = new List<Edge>();

    public Vector2 dragDisplacement = new Vector2(0,0);
    public Vector3 mouseDownPos = new Vector2(0, 0);
    public Vector3 tetrisDownPos = new Vector3(0, 0, 0);

    private Vector3 GetMouseWorldPos()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1));
    }

    private void OnMouseDown()
    {
        if (stateNow != state.Wait) return;
        ResetEdges();
        mouseDownPos = GetMouseWorldPos();
        tetrisDownPos = transform.position;
    }

    private void OnMouseDrag()
    {
        if (stateNow != state.Wait) return;
        transform.position = tetrisDownPos + (GetMouseWorldPos() - mouseDownPos);
    }

    private void OnMouseUp()
    {
        if (stateNow != state.Wait) return;
        RefreshEdges();
        recipe = new List<TetrisInfo>();
        Search(recipe);
        CheckSnap();
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
        //PROCESS RECIPE
        //1. find left-top point
        Vector2 topLeft = new Vector3(0,0,0);
        foreach(TetrisInfo t in recipe)
        {
            if (t.position.x <= topLeft.x) topLeft.x = t.position.x;
            if (t.position.y >= topLeft.y) topLeft.y = t.position.y;
        }
        //2. edit all
        for(int i =0; i<recipe.Count; i++)
        {
            topLeft += recipe[i].position;//.position = recipe[i].position - topLeft;
        }


        //EXPORT
        string export = "";
        print("start checking recipe");
        int c = 1;
        foreach (TetrisInfo t in recipe)
        {
            export += "" + c + " " + t.type + " " + t.position + " || ";
            c++;
        }
        print(export);
    }

    void Search(List<TetrisInfo> Recipe)
    {
        print("at search of: " + GetTetrisInfo().type);
        if (!Recipe.Contains(GetTetrisInfo())) Recipe.Add(GetTetrisInfo());

        List<Edge> toProcess = new List<Edge>(allEdges);
        foreach(Edge e in toProcess)
        {
            if (!e.isConnected()) continue;
            if (Recipe.Contains(e.getOppositeTetris().GetTetrisInfo())) continue;
            e.getOppositeTetris().Search(Recipe);
        }
    }

    void CheckSnap()
    {
        if (recipe.Count == 0) return;
        print("In checksnap");
        foreach (Edge e in allEdges)
        {
            if (e.isConnected())
            {
                print(e.getOppositeEdgeDistance());
                StartCoroutine(SnapMovement(e.getOppositeEdgeDistance()));
                break;
            }
        }

    }

    IEnumerator SnapMovement(Vector3 delta)
    {
        print("in corou");
        stateNow = state.Animation;
        Vector3 orgPos = transform.position, tarPos = orgPos+delta;
        float timeCount = 0, timeRequire = 0.3f;

        while (timeCount < timeRequire)
        {
            timeCount += Time.deltaTime;
            transform.position = Vector3.Lerp(orgPos,tarPos,timeCount/timeRequire);
            yield return new WaitForSeconds(0);
        }

        stateNow = state.Wait;
    }

    public TetrisInfo GetTetrisInfo() {
        myInfo = new TetrisInfo(myType, transform.position);
        return myInfo;
    }
}
