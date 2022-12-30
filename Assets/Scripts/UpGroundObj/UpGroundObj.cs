using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGroundObj : MonoBehaviour
{
    bool setUpTypeComplete = false;
    Gatherable myGatherable = null;
    Pickable myPickable = null;
    public string displayName = "not set";
    public Gatherable isGatherable()
    {
        if (myGatherable != null) return myGatherable;

        SetUpType();

        if (myGatherable != null) return myGatherable;

        return null;
    }
    public Pickable isPickable()
    {
        if (myPickable != null) return myPickable;

        SetUpType();

        if (myPickable != null) return myPickable;

        return null;
    }

    public void SetUpType()
    {
        if (setUpTypeComplete) return;

        if (GetComponent<Gatherable>() != null)
        {
            myGatherable = GetComponent<Gatherable>();
        }

        if (GetComponent<Pickable>() != null)
        {
            myPickable = GetComponent<Pickable>();
        }

        setUpTypeComplete = true;
    }


}
