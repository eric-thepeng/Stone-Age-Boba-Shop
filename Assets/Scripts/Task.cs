using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class Task : MonoBehaviour
{ 
    public TaskInfo myTi;
    GameObject shadow;
    public void SetUp(TaskInfo ti)
    {
        myTi = ti;
        if(ti.category == TaskInfo.Category.Jessie)
        {
            GetComponent<SpriteResolver>().SetCategoryAndLabel("Task Heads","Jessie");
        }
        else if (ti.category == TaskInfo.Category.Adam)
        {
            GetComponent<SpriteResolver>().SetCategoryAndLabel("Task Heads", "Adam");
        }
        else if (ti.category == TaskInfo.Category.Charles)
        {
            GetComponent<SpriteResolver>().SetCategoryAndLabel("Task Heads", "Charles");
        }
        else if (ti.category == TaskInfo.Category.Julie)
        {
            GetComponent<SpriteResolver>().SetCategoryAndLabel("Task Heads", "Julie");
        }
        else if (ti.category == TaskInfo.Category.Smith)
        {
            GetComponent<SpriteResolver>().SetCategoryAndLabel("Task Heads", "Smith");
        }
    }

    public void Deselect()
    {
        //GetComponentInParent<Animator>().SetTrigger("Deselect");
        GetComponentInParent<Animator>().SetBool("Selected", false);

    }

    private void OnMouseDown()
    {
        print("mouse down");
        TaskManager.i.SelectTask(this);
        //GetComponentInParent<Animator>().SetTrigger("Select");
        GetComponentInParent<Animator>().SetBool("Selected", true);
    }

    public void SelectInScript()
    {
        print("selected on runtime down");
        TaskManager.i.SelectTask(this);
        //GetComponentInParent<Animator>().SetTrigger("Select");
        GetComponentInParent<Animator>().SetBool("Selected", true);
    }

    private void OnMouseOver()
    {
        
    }

    private void OnMouseEnter()
    {
        shadow = new GameObject("shadow of " + gameObject.name);
        //shadow.transform.position = gameObject.transform.position + shadowOffsetStandard;
        shadow.transform.parent = this.transform;
        shadow.transform.localPosition = new Vector3(0, 0.2f, 0.1f);
        shadow.transform.localScale = new Vector3(1.1f, 1.1f, 1); //gameObject.transform.localScale;
        shadow.AddComponent<SpriteRenderer>();
        shadow.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
        shadow.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
    }
    private void OnMouseExit()
    {
        Destroy(shadow);
    }
}
