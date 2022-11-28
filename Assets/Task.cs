using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{ 
    public TaskInfo myTi;
    public void SetUp(TaskInfo ti)
    {
        myTi = ti;
    }

    private void OnMouseDown()
    {
        print("mouse down");
        TaskManager.i.DisplayTask(myTi);
    }

    private void OnMouseOver()
    {
        
    }
}
