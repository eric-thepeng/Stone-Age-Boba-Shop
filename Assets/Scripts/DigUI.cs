using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigUI : MonoBehaviour
{
    static DigUI instance;
    SpriteRenderer mySR, fillerSR;
    public static DigUI i
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<DigUI>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        mySR = GetComponent<SpriteRenderer>();
        fillerSR = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        Clear();
    }
    public void SetRatio(float setTo)
    {
        if (mySR.enabled == false) mySR.enabled = true;
        if (fillerSR.enabled == false) fillerSR.enabled = true;
        transform.GetChild(0).transform.localScale = new Vector3(setTo, setTo, 1);
    }

    public void Clear()
    {
        mySR.enabled = false;
        fillerSR.enabled = false;
    }
}
