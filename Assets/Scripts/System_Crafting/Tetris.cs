using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TetrisInfo
{
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
    enum state {Wait, Drag, Animation, Merge}
    state stateNow = state.Wait;

    TetrisInfo myInfo;
    Vector2 recipeFormingDelta;

    public string myType;
    public ItemScriptableObject itemSO;
    public ItemSOListScriptableObject allItemListSO;

    public List<Edge> allEdges = new List<Edge>();

    public Vector2 dragDisplacement = new Vector2(0,0);
    public Vector3 mouseDownPos = new Vector2(0, 0);
    public Vector3 tetrisDownPos = new Vector3(0, 0, 0);

    public class RecipeCombiator
    {
        Tetris motherTetris;
        List<Tetris> pastTetris;
        List<KeyValuePair<Vector2, ScriptableObject>> recipeGrid;

        public RecipeCombiator(Tetris t)
        {
            motherTetris = t;
            pastTetris = new List<Tetris>();
            recipeGrid = new List<KeyValuePair<Vector2, ScriptableObject>>();
        }

        public void AddTetris(Tetris baseT, Tetris newT, Vector2 baseCor, Vector2 dir, Vector2 newCor)
        {
            //Avoid Repetition (extra prevention
            if (Searched(newT)) return;
            pastTetris.Add(newT);

            //first add
            if (baseT == newT)
            {
                newT.recipeFormingDelta = new Vector2(0, 0);
                List<KeyValuePair<Vector2, ScriptableObject>> toSearch = newT.itemSO.FormationRecipeCoord;//getFormationRecipeCoord();
                foreach (KeyValuePair<Vector2, ScriptableObject> kvp in toSearch)
                {
                    recipeGrid.Add(kvp);
                }
                return;
            }
            //not first add
            else
            {
                //Calculate and record Delta
                Vector2 delta = baseCor + dir - newCor + baseT.recipeFormingDelta;
                newT.recipeFormingDelta = delta;

                //Load into final receipe 
                foreach (KeyValuePair<Vector2, ScriptableObject> kvp in newT.itemSO.FormationRecipeCoord)//getFormationRecipeCoord())
                {
                    recipeGrid.Add(new KeyValuePair<Vector2, ScriptableObject>(kvp.Key + delta, kvp.Value));
                }
            }
        }

        public bool Searched(Tetris t)
        {
            return (pastTetris.Contains(t));
        }

        public void Organize()
        {
            Vector2 leftNTopBound = new Vector2(0, 0);
            foreach (KeyValuePair<Vector2, ScriptableObject> kvp in recipeGrid)
            {
                if (kvp.Key.x < leftNTopBound.x) leftNTopBound.x = kvp.Key.x;
                if (kvp.Key.y < leftNTopBound.y) leftNTopBound.y = kvp.Key.y;
            }
            List<KeyValuePair<Vector2, ScriptableObject>> editedRecipeGrid = new List<KeyValuePair<Vector2, ScriptableObject>>();
            foreach (KeyValuePair<Vector2, ScriptableObject> kvp in recipeGrid)
            {
                editedRecipeGrid.Add(new KeyValuePair<Vector2, ScriptableObject>(kvp.Key - leftNTopBound, kvp.Value));
            }
            recipeGrid = editedRecipeGrid;
        }

        public bool hasConnector()
        {
            return (pastTetris.Count != 1);
        }

        public void DebugPrint()
        {
            print("Debug Print Recipe");
            foreach (KeyValuePair<Vector2, ScriptableObject> kvp in recipeGrid)
            {
                print(kvp.Key + " " + kvp.Value.name);
            }
        }

        public List<KeyValuePair<Vector2, ScriptableObject>> getRecipeGrid() { return recipeGrid; }
    }

    private Vector3 GetMouseWorldPos()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1));
    }

    private void SetState(state newState)
    {
        stateNow = newState;
    }

    private void OnMouseDown()
    {
        if (stateNow != state.Wait) return;
        SetState(state.Drag);
        ResetEdges();
        mouseDownPos = GetMouseWorldPos();
        tetrisDownPos = transform.position;
    }

    private void OnMouseDrag()
    {
        if (stateNow != state.Drag) return;
        transform.position = tetrisDownPos + (GetMouseWorldPos() - mouseDownPos);
    }

    private void OnMouseUp()
    {
        if (stateNow != state.Drag) return;
        SetState(state.Wait);
        RefreshEdges();
        RecipeCombiator RC = new RecipeCombiator(this);
        Search(RC, this, new Vector2(0,0), new Vector2(0, 0), new Vector2(0, 0));
        CheckSnap(RC);
        RC.Organize();
        CheckRecipe(RC);
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

    void CheckRecipe(RecipeCombiator rc)
    {
        ItemScriptableObject product = null;
        rc.DebugPrint();
        foreach (ItemScriptableObject iso in allItemListSO.list)
        {
            if(iso == itemSO) { continue; }
            if (iso.CheckMatch(rc.getRecipeGrid()))
            {
                product = iso;
                break;
            }
        }

        if (product != null)
        {
            print("We got it! It is: " + product.name);
        }

        /*
        rc.DebugPrint();
        foreach(ItemScriptableObject iso in allItemListSO.list)
        {
            foreach(ItemScriptableObject.Recipe r in iso.allRecipes){
                if(r.CheckMatch(rc.getRecipeGrid()) && itemSO.)
            }
        }*/
    }

    /*
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
    }*/

    void Search(RecipeCombiator rc, Tetris baseTetris, Vector2 baseCor, Vector2 dir, Vector2 newCor)
    {
        //add Tetris to recipe (embedded repitition check
        rc.AddTetris(baseTetris, this, baseCor, dir, newCor);

        List<Edge> toProcess = new List<Edge>(allEdges);
        foreach (Edge e in toProcess)
        {
            if (!e.isConnected()) continue;
            if (rc.Searched(e.getOppositeTetris())) continue;
            e.getOppositeTetris().Search(rc, this,e.getAttachedCoord(),e.getAttachToDirection(),e.getOppositeEdge().getAttachedCoord());
        }
    }

    void CheckSnap(RecipeCombiator rc)
    {
        if (!rc.hasConnector()) return;
        foreach (Edge e in allEdges)
        {
            if (e.isConnected())
            {
                StartCoroutine(SnapMovement(e.getOppositeEdgeDistance()));
                break;
            }
        }

    }

    IEnumerator SnapMovement(Vector3 delta)
    {
        stateNow = state.Animation;
        Vector3 orgPos = transform.position, tarPos = orgPos+delta;
        float timeCount = 0, timeRequire = 0.2f;

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
