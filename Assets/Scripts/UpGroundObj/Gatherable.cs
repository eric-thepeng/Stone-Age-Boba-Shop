using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class Gatherable : UpGroundObj
{
    [SerializeField] SpriteLibrary spriteLibrary;
    [SerializeField] SpriteResolver targetResolver;
    SpriteLibraryAsset LibraryAsset => spriteLibrary.spriteLibraryAsset;

    [SerializeField] string targetCategroy;
    [SerializeField] string fullSpriteLabel;
    [SerializeField] string emptySpriteLabel;
    public enum state {Gathering, Full, Empty}
    public enum toolMatchness {Normal, Stuck}
    public enum type {Berry, Grass, Tree}
    public type myType = type.Tree;
    state stateNow;
    public state bornState;

    [SerializeField] GameObject objProduce;

    Animator animator;

    float gatherTimeRequire = 1;
    float gatherTimeCount = 0;
    float lastGatherTimeCount = 0;

    private void Awake()
    {
        spriteLibrary = GetComponent<SpriteLibrary>();
        targetResolver = GetComponent<SpriteResolver>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
       BornAs(bornState);
    }
    void BornAs(state bornState)
    {
        if (bornState == state.Gathering)
        {
            print("error: born as gathering");
            return;
        }
        stateNow = bornState;
        SetSpriteToCurrentState();
    }
    void SetSpriteToCurrentState()
    {
        if(stateNow == state.Full)
        {
            targetResolver.SetCategoryAndLabel(targetCategroy, fullSpriteLabel);
        }
        else if(stateNow == state.Empty)
        {
            targetResolver.SetCategoryAndLabel(targetCategroy, emptySpriteLabel);
        }
    }
    void ChangeStateTo(state newState)
    {
        if (stateNow == newState) return;
        else
        {
            stateNow = newState;
            SetSpriteToCurrentState();
            if(stateNow == state.Gathering) animator.SetBool("Gathering", true);
            else animator.SetBool("Gathering", false);
        } 
    }
    public void Gather(PlayerAction.tool inTool) 
    {
        if(stateNow == state.Empty)
        {
            EmptyGather();
            return;
        }
        ChangeStateTo(state.Gathering);
        gatherTimeCount += Time.deltaTime;
        if(GetToolMatchness(inTool) == toolMatchness.Stuck)
        {
            gatherTimeCount = Mathf.Clamp(gatherTimeCount,0,gatherTimeRequire * 0.2f);
        }
        DigUI.i.SetRatio(gatherTimeCount/gatherTimeRequire);
        if(gatherTimeCount >= gatherTimeRequire)
        {
            Produce();
        }
    }
    private void Update()
    {
        if(stateNow == state.Gathering && gatherTimeCount == lastGatherTimeCount)
        {
            AbandonGather();
        }
        lastGatherTimeCount = gatherTimeCount;
    }
    public void AbandonGather() 
    {
        if (stateNow == state.Empty) return;
        ChangeStateTo(state.Full);
        gatherTimeCount = 0;
        DigUI.i.Clear();
    }
    public void EmptyGather() { }

    public void Grow()
    {
        ChangeStateTo(state.Full);
    }
    public void Produce()
    {
        DigUI.i.Clear();
        ChangeStateTo(state.Empty);
        if(objProduce != null)
        {
            GameObject ngo = Instantiate(objProduce, transform.parent);
            ngo.transform.position = transform.position;
        }
    }
    public bool CheckEmpty() { return stateNow == state.Empty; }

    public toolMatchness GetToolMatchness(PlayerAction.tool inTool)
    {
        if(myType == type.Berry)
        {
            return toolMatchness.Normal;
        }
        else if(myType == type.Grass)
        {
            if (inTool == PlayerAction.tool.Scythe) return toolMatchness.Normal;
        }
        else if(myType == type.Tree)
        {
            if (inTool == PlayerAction.tool.Axe) return toolMatchness.Normal;
        }
        return toolMatchness.Stuck;
    }

}
